using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;

public class SensePeople : MonoBehaviour
{
    private Thief thief;
    public float checkRadius;
    public float bothCheck;

    public LayerMask checkLayers;
    public LayerMask bothCheckLayer;

    public Vector3 leftHandColliderOffset;
    public Vector3 rightHandColliderOffset;
    public Vector3 bothHandColliderOffset;

    private void Awake()
    {
        thief = GetComponent<Thief>();
    }

    private void Update()
    {
        CheckPeopleLocation();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + leftHandColliderOffset, checkRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + rightHandColliderOffset, checkRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + bothHandColliderOffset, bothCheck);
    }

    private void CheckPeopleLocation()
    {
        Collider[] rightHandSideCollider = Physics.OverlapSphere(transform.position + rightHandColliderOffset, checkRadius, checkLayers);
        Collider[] leftHandSideCollider = Physics.OverlapSphere(transform.position + leftHandColliderOffset, checkRadius, checkLayers);
        Collider[] bothHandCollider = Physics.OverlapSphere(transform.position + bothHandColliderOffset, bothCheck, bothCheckLayer);

        //foreach (Collider collider in bothHandCollider)
        //{
        //    if (collider.CompareTag("People"))
        //    {
        //        if (collider.transform.position.x > transform.position.x)
        //        {
        //            thief.IsRight = true;
        //        }
        //        else if (collider.transform.position.x < transform.position.x)
        //        s
        //            thief.IsLeft = true;
        //        }
        //    }

        //    else if (collider.CompareTag("Tether"))
        //    {
        //        thief.IsStealing = true;
        //        foreach (Collider coll in bothHandCollider[0].gameObject.GetComponentsInChildren<Collider>())
        //        {
        //            coll.enabled = false;
        //        }
        //        People[] hitObj = bothHandCollider[0].GetComponentsInChildren<People>();
        //        if (hitObj != null)
        //        {
        //            foreach (People ppl in hitObj)
        //            {
        //                ppl.transform.parent = null;
        //                ppl.IsNervous = true;
        //                //ppl.Rotating = true;
        //                //ppl.IsChasing = true;
        //            }
        //        }
        //    }
        //}

        //if (bothHandCollider.Length > 0)
        //{
        //    thief.IsStealing = true;
        //    foreach (Collider coll in bothHandCollider[0].gameObject.GetComponentsInChildren<Collider>())
        //    {
        //        coll.enabled = false;
        //    }
        //    People[] hitObj = bothHandCollider[0].GetComponentsInChildren<People>();
        //    if (hitObj != null)
        //    {
        //        foreach (People ppl in hitObj)
        //        {
        //            ppl.transform.parent = null;
        //            ppl.IsNervous = true;
        //            //ppl.Rotating = true;
        //            //ppl.IsChasing = true;
        //        }
        //    }
        //}


        if (bothHandCollider.Length > 0)
        {
            foreach (Collider collider in bothHandCollider)
            {
                if (collider.CompareTag("People"))
                {
                    if (collider.transform.position.x > transform.position.x)
                    {
                        thief.IsRight = true;
                        People hitObj = collider.GetComponent<People>();
                        hitObj.transform.parent = null;
                        hitObj.IsNervous = true;
                    }

                    else if (collider.transform.position.x < transform.position.x)
                    {
                        thief.IsLeft = true;
                        People hitObj = collider.GetComponent<People>();
                        hitObj.transform.parent = null;
                        hitObj.IsNervous = true;
                    }
                }

                else if (collider.CompareTag("Tether"))
                {
                    collider.enabled = false;
                    thief.IsStealing = true;
                }
            }
        }

        else if (bothHandCollider.Length > 1)
        {
            foreach (Collider collider in bothHandCollider)
            {
                Debug.Log(collider.gameObject.name);
            }
        }

        else if (bothHandCollider.Length == 0)
        {
            thief.IsStealing = false;
        }

        if (leftHandSideCollider.Length > 0)
        {
            thief.IsLeft = true;
            People hitObj = leftHandSideCollider[0].GetComponent<People>();
            hitObj.transform.parent = null;
            hitObj.IsNervous = true;
            //hitObj.IsChasing = true;
        }
        else if (leftHandSideCollider.Length == 0 && bothHandCollider.Length == 0)
        {
            thief.IsLeft = false;
        }
        if (rightHandSideCollider.Length > 0)
        {
            thief.IsRight = true;
            People hitObj = rightHandSideCollider[0].GetComponent<People>();
            hitObj.transform.parent = null;
            hitObj.IsNervous = true;
            //hitObj.IsChasing = true;
        }
        else if (rightHandSideCollider.Length == 0 && bothHandCollider.Length == 0)
        {
            thief.IsRight = false;
        }
    }
}
