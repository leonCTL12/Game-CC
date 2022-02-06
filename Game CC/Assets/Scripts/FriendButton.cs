using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendButton : MonoBehaviour
{
    [SerializeField]
    private GameObject friendPanel;

    private bool active = false;
    public void ToggleFriendPanel()
    {
        active = !active;
        friendPanel.SetActive(active);
    }
}
