using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleRagdollController : MonoBehaviour
{
    void Start()
    {
        setRigidbodyState(true);
        setColliderState(false);
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

    private void ApplyForceOnDeath(float forceValue)
    {
        Rigidbody[] bones = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody bone in bones)
        {
            bone.AddForce(transform.right * forceValue);
        }
    }

    private void ApplyForceToLastPerson(float forceValue)
    {
        Rigidbody[] bones = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody bone in bones)
        {
            bone.AddForce(0, 0, forceValue, ForceMode.Impulse);
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

    public void OnDie(Transform t, float forceValue)
    {
        GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
        //GetComponent<CharacterController>().enabled = false;

        if (t.CompareTag("Mace"))
        {
            ApplyForceOnDeath(forceValue);
        }

        if (t.CompareTag("Player"))
        {
            ApplyForceToLastPerson(forceValue);
        }
    }
}