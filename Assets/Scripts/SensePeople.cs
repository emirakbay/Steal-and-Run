using UnityEngine;
using System;

public class SensePeople : MonoBehaviour
{
    private People people;
    public float checkRadius;
    public LayerMask checkLayers;

    //public Transform rightArm;
    //public Transform leftArm;

    private void Update()
    {
        CheckPeopleLocation();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    private void CheckPeopleLocation()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);

        Array.Sort(colliders, new DistanceComparer(transform));

        if (colliders.Length > 0)
        {
            foreach (Collider coll in colliders)
            {
                People hitObj = coll.GetComponent<People>();
                hitObj.IsNervous = true;
            }

            // Remove collider
            if (colliders.Length == 1)
            {
                // left side sense
                if (colliders[0].transform.position.x < transform.position.x)
                {
                    Debug.Log("people on your left");
                }
                // right side sense
                if (colliders[0].transform.position.x > transform.position.x)
                {
                    Debug.Log("people on your right");
                }
            }

            else if (colliders.Length > 1)
            {
                Debug.Log("both");
            }
        }
    }
}
