using UnityEngine;
using UnityEngine.XR;

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
            if (forceValue < 20.0f)
            {
                bone.AddForce(0, forceValue, 30.0f, ForceMode.Impulse);
            }

            else if (forceValue > 110f)
            {
                //print("normal value: " + forceValue);
                //print("normal value * 1.15f : " + forceValue * 1.15f);
                bone.AddForce(0, 90f, 90f, ForceMode.Impulse);
            }

            else
            {
                bone.AddForce(0, forceValue / 1.5f, forceValue * 1.15f, ForceMode.Impulse);
            }
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

    public void ActivateThiefRagdoll()
    {
        GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
    }
}