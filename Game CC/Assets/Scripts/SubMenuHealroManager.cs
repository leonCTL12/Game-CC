using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuHealroManager : MonoBehaviour
{

    public enum robot
    {
        battle,
        workout,
        diet,
        shop
    }

    [SerializeField]
    private GameObject[] robots;

    private GameObject currentRobot = null;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayRobotsIntro(robot type)
    {
        animator.SetTrigger("intro");
        int targetIndex = (int)type;
        for (int i = 0; i<robots.Length; i++)
        {
            robots[i].SetActive(targetIndex == i);
            if (targetIndex == i)
            {
                currentRobot = robots[i];
            }
        }
        currentRobot.GetComponent<Animator>().SetTrigger("fall");

        StartCoroutine(Delay_SetRobotBackToNormal());
    }

    public void PlayRobotsOutro()
    {
        currentRobot.GetComponent<Animator>().SetTrigger("jump");
    }

    public IEnumerator Delay_SetRobotBackToNormal()
    {
        yield return new WaitForSeconds(0.2f);

        currentRobot.GetComponent<MenuHealro>().ResetAnimatorState();
    }


}
