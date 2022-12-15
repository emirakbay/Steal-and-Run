using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Thief : MonoBehaviour
{
    private int currentCash;

    private bool isRunning = false;
    private bool isSprinting = false;
    private bool isSuperFast = false;
    private bool isStealing = false;
    private bool isDead = false;
    private bool isLeft = false;
    private bool isRight = false;
    private bool isLast = false;


    // The target marker.
    public Transform target;

    // Angular speed in radians per sec.
    public float speed = 2.0f;

    public int CurrentCash { get => currentCash; set => currentCash = value; }
    public bool IsRunning { get => isRunning; set => isRunning = value; }
    public bool IsStealing { get => isStealing; set => isStealing = value; }
    public bool IsLeft { get => isLeft; set => isLeft = value; }
    public bool IsRight { get => isRight; set => isRight = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public bool IsSprinting { get => isSprinting; set => isSprinting = value; }
    public bool IsSuperFast { get => isSuperFast; set => isSuperFast = value; }
    public bool IsLast { get => isLast; set => isLast = value; }

    private void Start()
    {
        setRigidbodyState(true);
        setColliderState(false);
    }

    private void Update()
    {
        if (IsLast)
        {
            // Determine which direction to rotate towards
            Vector3 targetDirection = target.position - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = speed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private void Die()
    {
        GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
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
            if (collider.name == "Thief")
                continue;
            collider.enabled = state;
        }
        GetComponent<Collider>().enabled = !state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grinder"))
        {
            IsDead = true;
            Die();
            GameManager.Instance.GameOver(true);
        }

        if (other.CompareTag("Finish"))
        {
            GameManager.Instance.HasPassedFinishLine = true;

            GameObject[] peoples = GameObject.FindGameObjectsWithTag("People");

            foreach (GameObject people in peoples)
            {
                people.GetComponent<People>().IsActive = false;
            }
        }

        if (other.CompareTag("SpeedUp"))
        {
            StartCoroutine(SpeedUp(5.0f));
        }

        //if (other.CompareTag("LastPerson"))
        //{
        //    print(GameManager.Instance.PowerUpScore);
        //    IsLast = true;
        //    other.gameObject.GetComponent<People>().IsDead = true;
        //    other.GetComponent<PeopleRagdollController>().OnDie(transform, 50.0f);
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SpeedUp"))
        {
            StartCoroutine(SpeedUp(5.0f));
        }
    }

    public IEnumerator SpeedUp(float boostTime)
    {
        IsSprinting = true;
        GetComponent<ThiefParticleController>().SpeedUpParticleController(true);
        yield return new WaitForSeconds(boostTime);
        GetComponent<ThiefParticleController>().SpeedUpParticleController(false);
        IsSprinting = false;
    }


    public IEnumerator SpeedUpExplosionBoost()
    {
        IsSuperFast = true;
        GetComponent<ThiefParticleController>().ExplosionSparksController(true);
        GetComponent<ThiefParticleController>().SpeedUpParticleController(true);
        GetComponent<ThiefParticleController>().ExplosionParticleController(true);
        yield return new WaitForSeconds(5.0f);
        GetComponent<ThiefParticleController>().ExplosionSparksController(false);
        GetComponent<ThiefParticleController>().SpeedUpParticleController(false);
        GetComponent<ThiefParticleController>().ExplosionParticleController(false);
        IsSuperFast = false;
    }
}
