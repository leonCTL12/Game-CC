using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    private Text targetText;
    [SerializeField]
    private float typeSpeed;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip typingSFX;
    [SerializeField]
    private bool forceUseTypingSFX;

    private void Awake()
    {
        targetText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Type(string stringToType, AudioClip clip = null)
    {
        if (!forceUseTypingSFX)
        {
            audioSource.clip = clip;
        } else
        {
            audioSource.loop = true;
        }
        StartCoroutine(TypeCoroutine(stringToType));
    }

    private IEnumerator TypeCoroutine(string stringToType)
    {
        float t = 0;
        int charIndex = 0;
        audioSource.Play();
        while(charIndex < stringToType.Length)
        {
            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, stringToType.Length);

            targetText.text = stringToType.Substring(0, charIndex);
            yield return null;
        }
        audioSource.Stop();
        targetText.text = stringToType;
    }
}
