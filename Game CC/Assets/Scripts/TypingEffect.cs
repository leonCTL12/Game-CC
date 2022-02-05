using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    private Text targetText;
    [SerializeField]
    private float typeSpeed;

    private void Awake()
    {
        targetText = GetComponent<Text>();
    }

    public void Type(string stringToType)
    {
        StartCoroutine(TypeCoroutine(stringToType));
    }

    private IEnumerator TypeCoroutine(string stringToType)
    {
        float t = 0;
        int charIndex = 0;

        while(charIndex < stringToType.Length)
        {
            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, stringToType.Length);

            targetText.text = stringToType.Substring(0, charIndex);
            yield return null;
        }

        targetText.text = stringToType;
    }
}
