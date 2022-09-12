using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleAnimationStateController : MonoBehaviour
{
    private People People;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        People = GetComponent<People>();
    }

    // Update is called once per frame
    void Update()
    {
        if (People.IsNervous == true)
        {
            animator.SetBool("isNervous", true);
        }
    }
}
