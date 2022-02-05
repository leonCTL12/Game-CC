﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class AmmoInventoryEntry
{
    [AmmoType]
    public int ammoType;
    public int amount = 0;
}

public class Controller : MonoBehaviour
{
    //Urg that's ugly, maybe find a better way
    public static Controller Instance { get; protected set; }

    public Camera MainCamera;
    public Camera WeaponCamera;
    
    public Transform CameraPosition;
    public Transform WeaponPosition;
    
    public Weapon[] startingWeapons;

    //this is only use at start, allow to grant ammo in the inspector. m_AmmoInventory is used during gameplay
    public AmmoInventoryEntry[] startingAmmo;

    [Header("Control Settings")]
    public float MouseSensitivity = 100.0f;
    public float PlayerSpeed = 5.0f;
    public float RunningSpeed = 7.0f;
    public float JumpSpeed = 5.0f;

    [Header("Audio")]
    public RandomPlayer FootstepPlayer;
    public AudioClip JumpingAudioCLip;
    public AudioClip LandingAudioClip;
    
    float m_VerticalSpeed = 0.0f;
    bool m_IsPaused = false;
    int m_CurrentWeapon;
    
    float m_VerticalAngle, m_HorizontalAngle;
    public float Speed { get; private set; } = 0.0f;

    public bool LockControl { get; set; }
    public bool CanPause { get; set; } = true;

    public bool Grounded => m_Grounded;

    CharacterController m_CharacterController;

    bool m_Grounded;
    float m_GroundedTimer;
    float m_SpeedAtJump = 0.0f;

    List<Weapon> m_Weapons = new List<Weapon>();
    Dictionary<int, int> m_AmmoInventory = new Dictionary<int, int>();

    #region Newly added code
    //input param
    private Vector2 walkInput;
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform cameraTransform;
    private Vector2 lookInput;
    [SerializeField]
    private float LookSensitivity;
    private float xRotation = 0f;

    //new variable
    private CharacterController characterController;
    #endregion

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            walkInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            walkInput = Vector2.zero;
        }
    }

    public void Look(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            lookInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            lookInput = Vector2.zero;
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            m_Weapons[m_CurrentWeapon].triggerDown = true;
        }
        else if (context.canceled)
        {
            m_Weapons[m_CurrentWeapon].triggerDown = false;
        }
    }




    public void OpenMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (CanPause)
            {
                PauseMenu.Instance.Display();
            }
        }

    }

    private void MovementHandler()
    {
        Vector3 move = transform.right * walkInput.x + transform.forward * walkInput.y; //create direction to move base on where player is facing
        if (move != Vector3.zero)
        {
            characterController.Move(move * speed * Time.deltaTime);
        }
    }

    void Awake()
    {
        Instance = this;
        characterController = GetComponent<CharacterController>();
    }
    
    void Start()
    {
        m_IsPaused = false;
        m_Grounded = true;
        
        MainCamera.transform.SetParent(CameraPosition, false);
        MainCamera.transform.localPosition = Vector3.zero;
        MainCamera.transform.localRotation = Quaternion.identity;
        m_CharacterController = GetComponent<CharacterController>();

        for (int i = 0; i < startingWeapons.Length; ++i)
        {
            PickupWeapon(startingWeapons[i]);
        }

        for (int i = 0; i < startingAmmo.Length; ++i)
        {
            ChangeAmmo(startingAmmo[i].ammoType, startingAmmo[i].amount);
        }
        
        m_CurrentWeapon = -1;
        ChangeWeapon(0);

        for (int i = 0; i < startingAmmo.Length; ++i)
        {
            m_AmmoInventory[startingAmmo[i].ammoType] = startingAmmo[i].amount;
        }

        m_VerticalAngle = 0.0f;
        m_HorizontalAngle = transform.localEulerAngles.y;
    }

    void Update()
    {
        MovementHandler();
        LookHandler();
    }

    private void LookHandler()
    {
        float sensitivity = LookSensitivity;
        float tunedInputX = lookInput.x * sensitivity * Time.deltaTime;
        float tunedInputY = lookInput.y * sensitivity * Time.deltaTime;

        xRotation -= tunedInputY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * tunedInputX);
    }

    //keep as reference
    private void OriginalUpdate()
    {
        //if (CanPause && Input.GetButtonDown("Menu"))
        //{
        //    PauseMenu.Instance.Display();
        //}

        //FullscreenMap.Instance.gameObject.SetActive(Input.GetButton("Map"));

        bool wasGrounded = m_Grounded;
        bool loosedGrounding = false;

        //we define our own grounded and not use the Character controller one as the character controller can flicker
        //between grounded/not grounded on small step and the like. So we actually make the controller "not grounded" only
        //if the character controller reported not being grounded for at least .5 second;
        //if (!m_CharacterController.isGrounded)
        //{
        //    if (m_Grounded)
        //    {
        //        m_GroundedTimer += Time.deltaTime;
        //        if (m_GroundedTimer >= 0.5f)
        //        {
        //            loosedGrounding = true;
        //            m_Grounded = false;
        //        }
        //    }
        //}
        //else
        //{
        //    m_GroundedTimer = 0.0f;
        //    m_Grounded = true;
        //}

        //Speed = 0;
        //Vector3 move = Vector3.zero;
        //if (!m_IsPaused && !LockControl)
        //{
        //    // Jump 
        //    if (m_Grounded && Input.GetButtonDown("Jump"))
        //    {
        //        m_VerticalSpeed = JumpSpeed;
        //        m_Grounded = false;
        //        loosedGrounding = true;
        //        FootstepPlayer.PlayClip(JumpingAudioCLip, 0.8f, 1.1f);
        //    }

        //    bool running = m_Weapons[m_CurrentWeapon].CurrentState == Weapon.WeaponState.Idle && Input.GetButton("Run");
        //    float actualSpeed = running ? RunningSpeed : PlayerSpeed;

        //    if (loosedGrounding)
        //    {
        //        m_SpeedAtJump = actualSpeed;
        //    }

        //    // Move around with WASD
        //    move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //    if (move.sqrMagnitude > 1.0f)
        //        move.Normalize();

        //    float usedSpeed = m_Grounded ? actualSpeed : m_SpeedAtJump;

        //    move = move * usedSpeed * Time.deltaTime;

        //    move = transform.TransformDirection(move);
        //    m_CharacterController.Move(move);




        //    // Turn player
        //    float turnPlayer = Input.GetAxis("Mouse X") * MouseSensitivity;
        //    m_HorizontalAngle = m_HorizontalAngle + turnPlayer;

        //    if (m_HorizontalAngle > 360) m_HorizontalAngle -= 360.0f;
        //    if (m_HorizontalAngle < 0) m_HorizontalAngle += 360.0f;

        //    Vector3 currentAngles = transform.localEulerAngles;
        //    currentAngles.y = m_HorizontalAngle;
        //    transform.localEulerAngles = currentAngles;

        //    // Camera look up/down
        //    var turnCam = -Input.GetAxis("Mouse Y");
        //    turnCam = turnCam * MouseSensitivity;
        //    m_VerticalAngle = Mathf.Clamp(turnCam + m_VerticalAngle, -89.0f, 89.0f);
        //    currentAngles = CameraPosition.transform.localEulerAngles;
        //    currentAngles.x = m_VerticalAngle;
        //    CameraPosition.transform.localEulerAngles = currentAngles;

            m_Weapons[m_CurrentWeapon].triggerDown = Input.GetMouseButton(0);

            //Speed = move.magnitude / (PlayerSpeed * Time.deltaTime);

            //if (Input.GetButton("Reload"))
            //    m_Weapons[m_CurrentWeapon].Reload();

            //if (Input.GetAxis("Mouse ScrollWheel") < 0)
            //{
            //    ChangeWeapon(m_CurrentWeapon - 1);
            //}
            //else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            //{
            //    ChangeWeapon(m_CurrentWeapon + 1);
            //}

            //Key input to change weapon

            //for (int i = 0; i < 10; ++i)
            //{
            //    if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            //    {
            //        int num = 0;
            //        if (i == 0)
            //            num = 10;
            //        else
            //            num = i - 1;

            //        if (num < m_Weapons.Count)
            //        {
            //            ChangeWeapon(num);
            //        }
            //    }
            //}
        //}

        // Fall down / gravity
        //m_VerticalSpeed = m_VerticalSpeed - 10.0f * Time.deltaTime;
        //if (m_VerticalSpeed < -10.0f)
        //    m_VerticalSpeed = -10.0f; // max fall speed
        //var verticalMove = new Vector3(0, m_VerticalSpeed * Time.deltaTime, 0);
        //var flag = m_CharacterController.Move(verticalMove);
        //if ((flag & CollisionFlags.Below) != 0)
        //    m_VerticalSpeed = 0;

        //if (!wasGrounded && m_Grounded)
        //{
        //    FootstepPlayer.PlayClip(LandingAudioClip, 0.8f, 1.1f);
        //}
    }

    void PickupWeapon(Weapon prefab)
    {
        //TODO : maybe find a better way than comparing name...
        if (m_Weapons.Exists(weapon => weapon.name == prefab.name))
        {//if we already have that weapon, grant a clip size of the ammo type instead
            ChangeAmmo(prefab.ammoType, prefab.clipSize);
        }
        else
        {
            var w = Instantiate(prefab, WeaponPosition, false);
            w.name = prefab.name;
            w.transform.localPosition = Vector3.zero;
            w.transform.localRotation = Quaternion.identity;
            w.gameObject.SetActive(false);
            
            w.PickedUp(this);
            
            m_Weapons.Add(w);
        }
    }

    void ChangeWeapon(int number)
    {
        if (m_CurrentWeapon != -1)
        {
            m_Weapons[m_CurrentWeapon].PutAway();
            m_Weapons[m_CurrentWeapon].gameObject.SetActive(false);
        }

        m_CurrentWeapon = number;

        if (m_CurrentWeapon < 0)
            m_CurrentWeapon = m_Weapons.Count - 1;
        else if (m_CurrentWeapon >= m_Weapons.Count)
            m_CurrentWeapon = 0;
        
        m_Weapons[m_CurrentWeapon].gameObject.SetActive(true);
        m_Weapons[m_CurrentWeapon].Selected();
    }

    public int GetAmmo(int ammoType)
    {
        int value = 0;
        m_AmmoInventory.TryGetValue(ammoType, out value);

        return value;
    }

    public void ChangeAmmo(int ammoType, int amount)
    {
        if (!m_AmmoInventory.ContainsKey(ammoType))
            m_AmmoInventory[ammoType] = 0;

        var previous = m_AmmoInventory[ammoType];
        m_AmmoInventory[ammoType] = Mathf.Clamp(m_AmmoInventory[ammoType] + amount, 0, 999);

        if (m_Weapons[m_CurrentWeapon].ammoType == ammoType)
        {
            if (previous == 0 && amount > 0)
            {//we just grabbed ammo for a weapon that add non left, so it's disabled right now. Reselect it.
                m_Weapons[m_CurrentWeapon].Selected();
            }
            
            WeaponInfoUI.Instance.UpdateAmmoAmount(GetAmmo(ammoType));
        }
    }

    public void PlayFootstep()
    {
        FootstepPlayer.PlayRandom();
    }
}
