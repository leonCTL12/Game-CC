using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHealroManager : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
    }

    public void PlayRobotsIntro()
    {
        animator.SetTrigger("intro");
        foreach (Transform child in transform)
        {
            child.GetComponent<Animator>().SetTrigger("fall");
        }
        StartCoroutine(Delay_SetRobotBackToNormal());
    }

    public void PlayRobotsOutro()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Animator>().SetTrigger("jump");
        }
    }

    public IEnumerator Delay_SetRobotBackToNormal()
    {
        yield return new WaitForSeconds(0.2f);

        foreach (Transform child in transform)
        {
            child.GetComponent<MenuHealro>().ResetAnimatorState();
        }
    }
}
