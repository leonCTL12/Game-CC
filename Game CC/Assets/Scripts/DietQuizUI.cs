using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DietQuizUI : MonoBehaviour
{
    [SerializeField]
    private int correctIndex;
    [SerializeField]
    private Text titleText;
    [SerializeReference]
    private Text bottomText;

    private string correctTitle = "Correct!";
    private string wrongTitle = "Wrong, the correct ans is A!";
    private string correctBottomText = "Good Job! Award is given, Please Check your inventory:)";
    private string wrongBottomText = "Don't give up! Come back and try again tomorrow:)";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChooseOption(int index)
    {
        bool correct = index == correctIndex;
        string chosenTitle = correct ? correctTitle : wrongTitle;
        string chosenBottomText = correct ? correctBottomText : wrongBottomText;

        bottomText.text = chosenBottomText;
        titleText.text = chosenTitle;

        animator.SetTrigger("ShowResult");
    }

    public void ShowCamera(bool show)
    {
        animator.SetBool("ShowCamera",show);
    }

}
