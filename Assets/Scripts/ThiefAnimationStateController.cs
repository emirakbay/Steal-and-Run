using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAnimationStateController : MonoBehaviour
{
    private Thief Thief;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        Thief = GetComponent<Thief>();
    }

    void Update()
    {
        CheckandSetThiefAnimation();
    }

    private void CheckandSetThiefAnimation()
    {
        if (Thief.IsRunning == true)
        {
            animator.SetBool("isRunning", true);
        }

        if (Thief.IsStealing == true)
        {
            animator.SetBool("isStealing", true);
        }
        else
        {
            animator.SetBool("isStealing", false);
        }

        if (Thief.IsLeft == true)
        {
            animator.SetBool("isLeft", true);
        }
        else
        {
            animator.SetBool("isLeft", false);
        }

        if (Thief.IsRight == true)
        {
            animator.SetBool("isRight", true);
        }
        else
        {
            animator.SetBool("isRight", false);
        }

        if (Thief.IsSprinting == true)
        {
            animator.SetBool("isSprint", true);
        }
        else
        {
            animator.SetBool("isSprint", false);
        }
    }
}
