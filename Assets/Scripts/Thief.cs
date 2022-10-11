using System.Collections;
using UnityEngine;

public class Thief : MonoBehaviour
{
    private int currentCash;

    private bool isRunning = false;
    private bool isSprinting = false;
    private bool isStealing = false;
    private bool isDead = false;
    private bool isLeft = false;
    private bool isRight = false;

    private bool stealStreak;


    public bool StealStreak { get => stealStreak; set => stealStreak = value; }
    public int CurrentCash { get => currentCash; set => currentCash = value; }
    public bool IsRunning { get => isRunning; set => isRunning = value; }
    public bool IsStealing { get => isStealing; set => isStealing = value; }
    public bool IsLeft { get => isLeft; set => isLeft = value; }
    public bool IsRight { get => isRight; set => isRight = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public bool IsSprinting { get => isSprinting; set => isSprinting = value; }

    private void Start()
    {
        setRigidbodyState(true);
        setColliderState(false);
    }

    private void Update()
    {
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

        if (other.CompareTag("Obstacle"))
        {
            IsDead = true;
            Die();
            GameManager.Instance.GameOver(true);
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
}
