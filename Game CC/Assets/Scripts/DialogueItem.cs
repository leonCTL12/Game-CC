using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueItem
{
    public enum robotTrigger
    {
        normal,
        happy,
        angry,
        dead //it means scared
    }
    public CharacterSkinController.EyePosition eye;
    public robotTrigger trigger;
    public string line;
}
