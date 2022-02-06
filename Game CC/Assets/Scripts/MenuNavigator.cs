using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigator : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject workOutMenu;
    [SerializeField]
    private MenuHealroManager bottomRobots;
    [SerializeField]
    private SubMenuHealroManager subMenuHealro;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowWorkOutMenu()
    {
        StartCoroutine(ShowWorkOutMenuCoroutine());
    }

    private IEnumerator ShowWorkOutMenuCoroutine()
    {
        workOutMenu.SetActive(true);
        animator.SetBool("workout_intro", true);
        animator.SetTrigger("main_outro");
        bottomRobots.PlayRobotsOutro();
        yield return new WaitForSeconds(0.3f);
        subMenuHealro.PlayRobotsIntro(SubMenuHealroManager.robot.workout);
        mainMenu.SetActive(false);
    }

    public void BackToMainMenu()
    {
        StartCoroutine(BackToMainMenuCoroutine());
    }

    private IEnumerator BackToMainMenuCoroutine()
    {
        animator.SetTrigger("submenu_outro");
        subMenuHealro.PlayRobotsOutro();
        yield return new WaitForSeconds(0.4f);
        bottomRobots.PlayRobotsIntro();
        workOutMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
