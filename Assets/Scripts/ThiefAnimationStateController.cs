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
        else if (Thief.IsSprinting == false)
        {
            animator.SetBool("isSprint", false);
        }

        if (Thief.IsSuperFast == true)
        {
            animator.SetBool("stealBoost", true);
        }
        else if (Thief.IsSuperFast == false)
        {
            animator.SetBool("stealBoost", false);
        }

        if (Thief.IsLast == true)
        {
            animator.SetBool("isLast", true);
            //print("is last animation controoler");
            //StartCoroutine(WaitAndKick());
        }
    }

    public IEnumerator WaitAndKick()
    {
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("isLast", true);
    }
}
