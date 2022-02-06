using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private DialogueItem[] dialogueItems;

    [SerializeField]
    private CharacterSkinController robot;

    [SerializeField]
    private TypingEffect contentText;

    [SerializeField]
    private GameObject videoRawImage;

    [SerializeField]
    private AudioSource audioSource;

    private int currentDialogueIndex = 0;

    private void Start()
    {
        StartCoroutine(PlayTVNoSignal());
    }

    private IEnumerator PlayTVNoSignal()
    {
        audioSource.Stop();
        videoRawImage.SetActive(true);
        yield return new WaitForSeconds(3.8f);
        PlayNextLine();
        audioSource.Play();
        videoRawImage.SetActive(false);
    }

    public void PlayNextLine()
    {
        if(currentDialogueIndex >= dialogueItems.Length) 
        {
            StartCoroutine(EndingCoroutine());
            return; 
        }
        DialogueItem dialogue = dialogueItems[currentDialogueIndex];
        contentText.Type(dialogue.line);
        robot.ChangeAnimatorIdle(dialogue.trigger.ToString());
        robot.ChangeEyeOffset(dialogue.eye);

        currentDialogueIndex++;
    }

    public IEnumerator EndingCoroutine()
    {
        StartCoroutine(PlayTVNoSignal());
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("MainUI");
    }
}
