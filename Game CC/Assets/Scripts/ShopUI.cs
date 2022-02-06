using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField]
    private GameObject vItemCover, rItemCover;
    [SerializeField]
    private GameObject vitualItemPanel;
    [SerializeField]
    private GameObject realItemPanel;
    [SerializeField]
    private Image vitualItemImage;
    [SerializeField]
    private Image realItemImage;
    [SerializeField]
    private Button virutalItemButton;
    [SerializeField]
    private Button realItemButton;

    private bool virtualMenu = true;

    public void ToggleMenu()
    {
        virtualMenu = !virtualMenu;
        SetUp();
    }

    private void OnEnable()
    {
        Debug.Log("On Enable");
        virtualMenu = true;
        SetUp();
    }

    private void SetUp()
    {
        vitualItemPanel.SetActive(virtualMenu);
        virutalItemButton.interactable = !virtualMenu;
        vItemCover.SetActive(virtualMenu);

        realItemPanel.SetActive(!virtualMenu);
        realItemButton.interactable = virtualMenu;
        rItemCover.SetActive(!virtualMenu);
    }
}
