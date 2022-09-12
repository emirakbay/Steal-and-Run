using UnityEngine;
using System;

public class SensePeople : MonoBehaviour
{
    private People people;
    public float checkRadius;
    public LayerMask checkLayers;
    public Vector3 leftHandColliderOffset;
    public Vector3 rightHandColliderOffset;

    //public Transform rightArm;
    //public Transform leftArm;

    private void Update()
    {
        CheckPeopleLocation();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - leftHandColliderOffset, checkRadius);
        Gizmos.DrawWireSphere(transform.position - rightHandColliderOffset, checkRadius);
    }

    private void CheckPeopleLocation()
    {
        Collider[] rightHandSideColliders = Physics.OverlapSphere(transform.position - leftHandColliderOffset, checkRadius, checkLayers);
        Collider[] leftHandSideColliders = Physics.OverlapSphere(transform.position - rightHandColliderOffset, checkRadius, checkLayers);

        Array.Sort(leftHandSideColliders, new DistanceComparer(transform));
        Array.Sort(rightHandSideColliders, new DistanceComparer(transform));

        if (leftHandSideColliders.Length > 0 && rightHandSideColliders.Length > 0)
        {
            foreach (Collider coll in leftHandSideColliders)
            {
                People hitObj = coll.GetComponent<People>();
                hitObj.IsNervous = true;
            }
        }

        else if (rightHandSideColliders.Length > 0 && leftHandSideColliders.Length == 0)
        {
            foreach (Collider coll in rightHandSideColliders)
            {
                People hitObj = coll.GetComponent<People>();
                hitObj.IsNervous = true;
            }
        }

        else if (leftHandSideColliders.Length > 0 && rightHandSideColliders.Length == 0)
        {
            foreach (Collider coll in leftHandSideColliders)
            {
                People hitObj = coll.GetComponent<People>();
                hitObj.IsNervous = true;
            }
        }
    }
}
