using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHealro : MonoBehaviour
{
    public enum AnimatorState { normal, happy, angry, dead}
    
    [SerializeField]
    private AnimatorState animatorState;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.SetTrigger(animatorState.ToString());
    }
}
