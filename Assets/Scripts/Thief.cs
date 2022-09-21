using UnityEngine;

public class Thief : MonoBehaviour
{
    private int currentGold;

    private bool isRunning = false;
    private bool isStealing = false;
    private bool isDead = false;
    private bool isLeft = false;
    private bool isRight = false;

    private float stealStreak;

    public bool IsRunning { get => isRunning; set => isRunning = value; }
    public int CurrentGold { get => currentGold; set => currentGold = value; }
    public float StealStreak { get => stealStreak; set => stealStreak = value; }
    public bool IsStealing { get => isStealing; set => isStealing = value; }
    public bool IsLeft { get => isLeft; set => isLeft = value; }
    public bool IsRight { get => isRight; set => isRight = value; }
    public bool IsDead { get => isDead; set => isDead = value; }

    private void Start()
    {
        //setRigidbodyState(true);
        //setColliderState(false);
    }

    private void die()
    {
    }

    private void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
    }

    private void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            //collider.enabled = state;
        }
    }
}
