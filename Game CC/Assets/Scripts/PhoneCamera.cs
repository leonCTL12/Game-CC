using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;

    [SerializeField]
    private Animator animator;


    public RawImage background;
    private AudioSource audioSource;

    [SerializeField]
    private CommentType commentType;

    private enum CommentType
    {
        balanced,
        fastFood,
        trash
    }

    [SerializeField]
    private Text dietComment;

    private string balancedComment = "Rate: 10/10 \n Comment: This is a balanced diet! GJ!";

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if(devices.Length == 0)
        {
            Debug.Log("No camera detected");
            camAvailable = false;
            return;
        }

        for(int i=0; i<devices.Length; i++)
        {
            backCam = new WebCamTexture(devices[i].name);
        }

        if(backCam == null)
        {
            Debug.Log("Unable to find back camera");
            return;
        }

        backCam.Play();
        background.texture = backCam;
        camAvailable = true;

        SetComment();
    }

    private void SetComment()
    {
        string comment = null;
        switch (commentType)
        {
            case CommentType.balanced:
                comment = balancedComment;
                break;
            default:
                comment = balancedComment;
                break;

        }

        dietComment.text = comment;
    }

    private void Update()
    {
        if (!camAvailable)
            return;

        float scaleY = backCam.videoVerticallyMirrored ? -1f: 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }

    public void TakePhoto()
    {
        StartCoroutine(PhotoCoroutine());
    }

    private IEnumerator PhotoCoroutine()
    {
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
        backCam.Pause();
        animator.SetTrigger("ShowComment");
        camAvailable = false;

    }
}
