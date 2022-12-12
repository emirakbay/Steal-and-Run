using System;
using System.Collections;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class People : MonoBehaviour
{
    private bool isChasing = false;
    private bool isNervous = false;
    private bool isDead = false;
    private bool isActive = true;
    [SerializeField]
    private bool isWalking = false;
    [SerializeField]
    private bool onBonusGround = false;
    [SerializeField]
    private bool isLastPerson = false;

    private float walkSpeed = 2.0f;
    private float rotateSpeed = 0.1f;
    private float timeCount = 0.0f;

    public Transform target;

    private StartAnimation startAnimation;

    public Transform player;

    protected NavMeshAgent mesh;

    private void Start()
    {
        mesh = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Vector3 moveDir = FindObjectOfType<PlayerMovement>().MoveDirection;
        if (IsNervous)
            Rotate();

        if (IsChasing && !IsDead && !OnBonusGround)
        {
            if (GetComponent<NavMeshAgent>().enabled)
            {
                mesh.destination = GameObject.FindWithTag("Player").transform.position;
            }
        }

        if (IsDead)
        {
            if (GetComponent<NavMeshAgent>())
                GetComponent<NavMeshAgent>().enabled = false;
        }

        if (!IsActive)
        {
            IsChasing = false;
            GetComponent<NavMeshAgent>().enabled = false;
        }

        if (IsWalking && GameManager.Instance.HasGameStarted)
        {
            Move();
        }

        CheckDistance();

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    GetComponent<PeopleRagdollController>().OnDie(transform);
        //}
    }

    private void Awake()
    {
        AssignRandomStartAnim();
    }

    public void Rotate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, timeCount * rotateSpeed);
        timeCount += Time.deltaTime;
    }

    private void SurprisedAnimStopEvent()
    {
        if (!OnBonusGround)
            IsChasing = true;
        //GetComponent<Collider>().isTrigger = false;
    }

    private void AssignRandomStartAnim()
    {
        startAnimation = (StartAnimation)Random.Range(0, (float)Enum.GetValues(typeof(StartAnimation)).Cast<StartAnimation>().Max());
    }

    public IEnumerator DeactivateCollider(int seconds)
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(seconds);
        GetComponent<Collider>().enabled = true;
    }

    private void CheckDistance()
    {
        if (IsChasing)
        {
            float distance = Vector3.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);

            if (distance < 5.0f)
            {
                GameObject.FindWithTag("Player").GetComponent<PeopleRagdollController>().ActivateThiefRagdoll();
                GameObject[] peoples = GameObject.FindGameObjectsWithTag("People");
                foreach (GameObject people in peoples)
                {
                    people.GetComponent<People>().IsActive = false;
                }
            }
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
    }

    public bool IsChasing { get => isChasing; set => isChasing = value; }
    public bool IsNervous { get => isNervous; set => isNervous = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public bool IsActive { get => isActive; set => isActive = value; }
    public bool IsWalking { get => isWalking; set => isWalking = value; }
    public bool OnBonusGround { get => onBonusGround; set => onBonusGround = value; }
    public StartAnimation StartAnimation { get => startAnimation; set => startAnimation = value; }
}

public enum StartAnimation
{
    idle1,
    idle2,
    idle3,
    idle4,
    talk1,
    talk2
}