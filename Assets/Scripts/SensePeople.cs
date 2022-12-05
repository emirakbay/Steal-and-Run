using UnityEngine;
using System;

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

    public GameObject moneyParticle;

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

        if (bothHandCollider.Length > 0)
        {
            foreach (Collider collider in bothHandCollider)
            {
                if (collider.CompareTag("People"))
                {
                    if (collider.GetComponentInParent<Collider>())
                    {
                        People hitObj = collider.GetComponent<People>();
                        hitObj.GetComponent<StealMoneyParticleController>().StealMoneyParticle();
                        hitObj.IsWalking = false;
                        if (!thief.IsSprinting)
                        {
                            StartCoroutine(thief.SpeedUp(3.0f));
                        }
                        hitObj.transform.parent = null;
                        hitObj.IsNervous = true;
                        StartCoroutine(thief.SpeedUp(3.0f));
                        GameManager.Instance.PowerUpScore += .5f;
                        GameManager.Instance.MoneyScore += UnityEngine.Random.Range(1, 2);
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

                if (collider.CompareTag("Tether"))
                {
                    foreach (People people in collider.gameObject.GetComponentsInChildren<People>())
                    {
                        people.IsNervous = true;
                        people.GetComponent<StealMoneyParticleController>().StealMoneyParticle();
                        StartCoroutine(people.DeactivateCollider(2));
                    }
                    if (!thief.IsSprinting)
                    {
                        StartCoroutine(thief.SpeedUp(6.0f));
                    }
                    collider.transform.DetachChildren();
                    Destroy(collider.gameObject);
                    thief.IsStealing = true;
                    GameManager.Instance.PowerUpScore += 11.0f;
                    GameManager.Instance.MoneyScore += UnityEngine.Random.Range(5, 10);

                }

                // level end
                if (collider.CompareTag("KickAreaCollider"))
                {
                    GetComponent<Thief>().IsLast = true;
                }

                if (collider.CompareTag("LastPerson"))
                {
                    print(GameManager.Instance.PowerUpScore);
                    collider.gameObject.GetComponent<People>().IsDead = true;

                    if (GameManager.Instance.MoneyScore < 0)
                    {
                        collider.GetComponent<PeopleRagdollController>().OnDie(transform, 1.0f);
                    }

                    else
                    {
                        collider.GetComponent<PeopleRagdollController>().OnDie(transform, GameManager.Instance.PowerUpScore * 1.5f);
                    }
                }
            }
        }

        else if (bothHandCollider.Length == 0)
        {
            thief.IsStealing = false;
        }

        if (leftHandSideCollider.Length > 0)
        {
            People hitObj = leftHandSideCollider[0].GetComponent<People>();
            if (hitObj != null)
            {
                hitObj.GetComponent<StealMoneyParticleController>().StealMoneyParticle();
                hitObj.IsWalking = false;
                hitObj.transform.parent = null;
                hitObj.IsNervous = true;
                GameManager.Instance.PowerUpScore += .5f;
                GameManager.Instance.MoneyScore += UnityEngine.Random.Range(1, 2);
                thief.IsLeft = true;
            }

            else
            {
                GetComponent<Thief>().IsLast = true;
            }

            if (!thief.IsSprinting)
            {
                StartCoroutine(thief.SpeedUp(3.0f));
            }
        }

        else if (leftHandSideCollider.Length == 0 && bothHandCollider.Length == 0)
        {
            thief.IsLeft = false;
        }

        if (rightHandSideCollider.Length > 0)
        {
            People hitObj = rightHandSideCollider[0].GetComponent<People>();
            if (hitObj != null)
            {
                hitObj.GetComponent<StealMoneyParticleController>().StealMoneyParticle();
                hitObj.IsWalking = false;
                hitObj.transform.parent = null;
                hitObj.IsNervous = true;
                GameManager.Instance.PowerUpScore += .5f;
                GameManager.Instance.MoneyScore += UnityEngine.Random.Range(1, 2);
                thief.IsRight = true;
            }

            else
            {
                GetComponent<Thief>().IsLast = true;
            }

            if (!thief.IsSprinting)
            {
                StartCoroutine(thief.SpeedUp(3.0f));
            }
        }

        else if (rightHandSideCollider.Length == 0 && bothHandCollider.Length == 0)
        {
            thief.IsRight = false;
        }
    }
}
