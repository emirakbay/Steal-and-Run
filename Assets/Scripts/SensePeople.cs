using UnityEngine;
using System;
using UnityEngine.UIElements;


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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position + leftHandColliderOffset, checkRadius);

    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position + rightHandColliderOffset, checkRadius);

    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position + bothHandColliderOffset, bothCheck);
    //}

    private void CheckPeopleLocation()
    {
        Collider[] rightHandSideCollider = Physics.OverlapSphere(transform.position + rightHandColliderOffset, checkRadius, checkLayers);
        Collider[] leftHandSideCollider = Physics.OverlapSphere(transform.position + leftHandColliderOffset, checkRadius, checkLayers);
        Collider[] bothHandCollider = Physics.OverlapSphere(transform.position + bothHandColliderOffset, bothCheck, bothCheckLayer);

        if (bothHandCollider.Length > 0)
        {
            foreach (Collider collider in bothHandCollider)
            {
                if (collider.CompareTag("People"))
                {
                    if (collider.GetComponentInParent<Collider>())
                    {
                        People hitObj = collider.GetComponent<People>();
                        hitObj.transform.parent = null;
                        hitObj.IsNervous = true;
                        thief.StealStreak = true;
                        GameManager.Instance.PowerUpScore += 10.0f;
                        StartCoroutine(thief.SpeedUp(1.0f));
                        if (collider.transform.position.x > transform.position.x)
                        {
                            thief.IsRight = true;
                        }

                        else if (collider.transform.position.x < transform.position.x)
                        {
                            thief.IsLeft = true;
                        }
                    }
                }

                else if (collider.CompareTag("Tether"))
                {
                    foreach (People people in collider.gameObject.GetComponentsInChildren<People>())
                    {
                        people.IsNervous = true;
                    }
                    StartCoroutine(thief.SpeedUp(2.0f));
                    collider.transform.DetachChildren();
                    Destroy(collider.gameObject);
                    thief.IsStealing = true;
                    thief.StealStreak = true;
                    GameManager.Instance.PowerUpScore += 20.0f;
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
            thief.StealStreak = false;
        }

        if (leftHandSideCollider.Length > 0)
        {
            if (leftHandSideCollider[0].gameObject.transform.parent != null)
            {
                GameManager.Instance.PowerUpScore += 10.0f;
                StartCoroutine(thief.SpeedUp(1.0f));
                thief.IsLeft = true;
                thief.StealStreak = true;
                People hitObj = leftHandSideCollider[0].GetComponent<People>();
                hitObj.transform.parent = null;
                hitObj.IsNervous = true;

            }
        }
        else if (leftHandSideCollider.Length == 0 && bothHandCollider.Length == 0)
        {
            thief.IsLeft = false;
            thief.StealStreak = false;

        }
        if (rightHandSideCollider.Length > 0)
        {
            if (rightHandSideCollider[0].gameObject.transform.parent != null)
            {
                GameManager.Instance.PowerUpScore += 10.0f;
                StartCoroutine(thief.SpeedUp(1.0f));
                thief.IsRight = true;
                thief.StealStreak = true;
                People hitObj = rightHandSideCollider[0].GetComponent<People>();
                hitObj.transform.parent = null;
                hitObj.IsNervous = true;
            }
        }
        else if (rightHandSideCollider.Length == 0 && bothHandCollider.Length == 0)
        {
            thief.IsRight = false;
            thief.StealStreak = false;
        }
    }
}
