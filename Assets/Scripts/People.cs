using System;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class People : MonoBehaviour
{
    private bool isChasing = false;
    private bool isNervous = false;
    private bool isDead = false;

    private bool rotating = false;

    private float speed = 50.0f;
    private float rotationTime;

    CharacterController _controller;
    Transform targetTransform;
    GameObject Player;

    float rotateSpeed = 0.1f;
    float timeCount = 0.0f;

    private Vector3 relativePos;
    private Quaternion targetRotation;

    public Transform target;
    private AnimationClip[] clips;
    private Animator animator;

    private StartAnimation startAnimation;

    private int cash = 0;

    public Transform player;
    protected NavMeshAgent mesh;


    private void Start()
    {
        //Player = GameObject.FindWithTag("Player");
        //target = Player.transform;
        //_controller = GetComponent<CharacterController>();
        //_controller.enabled = true;

        mesh = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector3 moveDir = FindObjectOfType<PlayerMovement>().MoveDirection;
        if (IsNervous)
            Rotate();

        if (IsChasing)
        {
            //Vector3 direction = target.position - transform.position;

            //direction = direction.normalized;

            //Vector3 velocity = direction * speed;

            //_controller.Move(velocity * Time.deltaTime);


            mesh.destination = GameObject.FindWithTag("Player").transform.position;

            //if (moveDir != Vector3.zero)
            //{
            //    transform.position += moveDir * speed * Time.deltaTime;

            //    transform.forward = moveDir;

            //    Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);

            //    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speed * Time.deltaTime);

            //}
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other.gameObject);
    }

    private void Awake()
    {
        AssignRandomStartAnim();
    }

    public void Rotate()
    {
        //relativePos = target.position + transform.position;
        //targetRotation = Quaternion.LookRotation(-relativePos);
        //rotating = true;

        //if (rotating)
        //{
        //    rotationTime += Time.deltaTime * 5.0f;
        //    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationTime * Time.deltaTime);
        //}
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, timeCount * rotateSpeed);
        timeCount = timeCount + Time.deltaTime;
    }

    private void SurprisedAnimStopEvent()
    {
        IsChasing = true;
        GetComponent<Collider>().isTrigger = false;
        //GetComponent<CharacterController>().enabled = true;
    }

    private void AssignRandomStartAnim()
    {
        startAnimation = (StartAnimation)Random.Range(0, (float)Enum.GetValues(typeof(StartAnimation)).Cast<StartAnimation>().Max());

    }

    public bool IsChasing { get => isChasing; set => isChasing = value; }
    public bool IsNervous { get => isNervous; set => isNervous = value; }
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