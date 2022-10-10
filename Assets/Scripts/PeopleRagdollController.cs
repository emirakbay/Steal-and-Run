using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleRagdollController : MonoBehaviour
{
    void Start()
    {
        //foreach (Collider coll in GetComponentsInChildren<Collider>())
        //{
        //    if (coll.CompareTag("Ragdoll"))
        //    {
        //        if (coll.GetComponent<BoxCollider>() != null)
        //        {
        //            coll.GetComponent<BoxCollider>().isTrigger = true;
        //        }

        //        if (coll.GetComponent<CapsuleCollider>() != null)
        //        {
        //            coll.GetComponent<CapsuleCollider>().isTrigger = true;
        //        }
        //    }

        //}

        setRigidbodyState(true);
        setColliderState(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            OnDie();
        }
    }

    private void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = !state;
        }
    }


    private void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }
        GetComponent<Collider>().enabled = !state;
    }

    private void OnDie()
    {
        GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
    }
}
