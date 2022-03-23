using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraButton : MonoBehaviour
{
    [SerializeField]
    private DietQuizUI dietQuiz;

    [SerializeField]
    private GameObject cameraPanel;

    [SerializeField]
    private Sprite cameraIcon, quizIcon;

    [SerializeField]
    private Image iconImg;

    private bool inQuiz = true;
    


    public void OnCameraButtonClick()
    {
        inQuiz = !inQuiz;
        dietQuiz.ShowCamera(!inQuiz);
        Sprite icon = inQuiz ? cameraIcon : quizIcon;
        cameraPanel.SetActive(!inQuiz);
        iconImg.sprite = icon;
    }

}
