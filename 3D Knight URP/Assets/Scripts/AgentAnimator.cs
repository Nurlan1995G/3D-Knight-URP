using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AgentAnimator : MonoBehaviour
{
    private Animator animator;
    public enum AnimStates { Idle, Walk, Attack, Replenishment, Death};  //enum это перечисление

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void SetAnimState(AnimStates animState)
    {
        animator.SetInteger("State", (int)animState);
    }
}
