using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseWeaponManager : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private Image weaponField;

    [SerializeField]
    private Sprite[] weapons;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TogglePanel(bool show)
    {
        animator.SetBool("expand",show);
    }

    public void ShowWeapon(int weaponIndex)
    {
        TogglePanel(false);
        weaponField.sprite = weapons[weaponIndex];
    }


}
