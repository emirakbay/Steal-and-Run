using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAnimationStateController : MonoBehaviour
{
    private Thief Thief;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Thief = GetComponent<Thief>();
    }

    // Update is called once per frame
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
    }
}
