using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleAnimationStateController : MonoBehaviour
{
    private People people;

    private Animator animator;

    private string[] animParamaters = { "idle1", "idle2", "idle3", "idle4", "talk1", "talk2" };
    void Start()
    {
        animator = GetComponent<Animator>();
        people = GetComponent<People>();
        SetStartAnimations();
    }

    void Update()
    {
        if (people.IsNervous == true)
        {
            animator.SetBool("isNervous", true);
        }

        if (people.IsChasing == true)
        {
            animator.SetBool("isChasing", true);
        }

        else if (people.IsChasing == false)
        {
            animator.SetBool("isChasing", false);
        }

        if (people.IsActive == false)
        {
            animator.SetBool("isFinished", true);
        }
    }

    private void SetStartAnimations()
    {
        if (people.IsWalking)
        {
            animator.SetBool("isWalking", true);
        }

        else
        {
            for (int i = 0; i < animParamaters.Length; i++)
            {
                if ((int)people.StartAnimation == i)
                {
                    animator.SetBool(animParamaters[i], true);
                }
            }
        }
    }
}