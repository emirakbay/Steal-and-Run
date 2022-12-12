using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnPeopleCollide : MonoBehaviour
{
    private float maceForce = 1000.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("People"))
        {
            other.gameObject.GetComponent<People>().IsDead = true;
            other.GetComponent<PeopleRagdollController>().OnDie(transform, maceForce);
        }
    }
}