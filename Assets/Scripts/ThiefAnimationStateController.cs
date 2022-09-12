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
        if (Thief.IsRunning == true)
        {
            animator.SetBool("isRunning", true);
        }
    }
}
