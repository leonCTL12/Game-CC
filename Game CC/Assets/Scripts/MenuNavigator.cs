using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private GameObject shopMenu;
    [SerializeField]
    private MenuHealroManager bottomRobots;
    [SerializeField]
    private SubMenuHealroManager subMenuHealro;
    [SerializeField]
    private GameObject statusBar;
    [SerializeField]
    private GameObject coverPage;

    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ShowMainPanel()
    {
        animator.SetTrigger("cover_outro");
        StartCoroutine(CoverOutroCoroutine());
    }

    private IEnumerator CoverOutroCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        statusBar.SetActive(true);
        mainMenu.SetActive(true);
        coverPage.SetActive(false); 
    }
    public void ShowWorkOutMenu()
    {
        //audioSource.Play();
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
        //audioSource.Play();
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
        //audioSource.Play();
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

    public void ShowShopMenu()
    {
        //audioSource.Play();
        StartCoroutine(ShowShopMenuCoroutine());
    }

    private IEnumerator ShowShopMenuCoroutine()
    {
        shopMenu.SetActive(true);
        animator.SetBool("shop_intro", true);
        animator.SetTrigger("main_outro");
        bottomRobots.PlayRobotsOutro();
        yield return new WaitForSeconds(0.3f);
        subMenuHealro.PlayRobotsIntro(SubMenuHealroManager.robot.shop);
        mainMenu.SetActive(false);
    }

    public void BackToMainMenu()
    {
        //audioSource.Play();
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
        shopMenu.SetActive(false);

        mainMenu.SetActive(true);
        Debug.Log("Main Menu Active!");

        animator.SetBool("workout_intro", false);
        animator.SetBool("diet_intro", false);
        animator.SetBool("battle_intro", false);
        animator.SetBool("shop_intro", false);
    }

    public void GoToFightScene()
    {
        SceneManager.LoadScene("ExampleScene");
    }
}
