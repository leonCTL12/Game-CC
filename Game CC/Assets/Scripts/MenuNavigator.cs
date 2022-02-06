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
    private GameObject dietQuiz;
    [SerializeField]
    private GameObject battleMenu;
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

    public void ShowDietQuiz()
    {
        StartCoroutine(ShowDietQuizCoroutine());
    }

    private IEnumerator ShowDietQuizCoroutine()
    {
        dietQuiz.SetActive(true);
        animator.SetBool("diet_intro", true);
        animator.SetTrigger("main_outro");
        bottomRobots.PlayRobotsOutro();
        yield return new WaitForSeconds(0.3f);
        subMenuHealro.PlayRobotsIntro(SubMenuHealroManager.robot.diet);
        mainMenu.SetActive(false);
    }

    public void ShowBattleMenu()
    {
        StartCoroutine(ShowBattleMenuCoroutine());
    }

    private IEnumerator ShowBattleMenuCoroutine()
    {
        battleMenu.SetActive(true);
        animator.SetBool("battle_intro", true);
        animator.SetTrigger("main_outro");
        bottomRobots.PlayRobotsOutro();
        yield return new WaitForSeconds(0.3f);
        subMenuHealro.PlayRobotsIntro(SubMenuHealroManager.robot.battle);
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
        dietQuiz.SetActive(false);
        battleMenu.SetActive(false);

        mainMenu.SetActive(true);

        animator.SetBool("workout_intro", false);
        animator.SetBool("diet_intro", false);
        animator.SetBool("battle_intro", false);
    }
}
