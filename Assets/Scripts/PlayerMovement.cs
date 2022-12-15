using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float slidingFactor;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float sprintSpeed;

    [SerializeField] private float speedySpeed;

    private Vector3 moveDirection;

    private CharacterController controller;

    private bool rotateFlag = false;

    private float rotateSpeed = 0.5f;
    private float timeCount = 0.0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (GameManager.Instance.HasGameStarted == true)
        {
            GetComponent<Thief>().IsRunning = true;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.HasGameStarted == true && !GameManager.Instance.IsGameOver && !GetComponent<Thief>().IsLast)
        {
            Move();
        }

        else if (GetComponent<Thief>().IsLast)
        {
            GameObject lastPerson = GameObject.FindGameObjectWithTag("LastPerson");

            MoveTowardsTarget(lastPerson.transform.position);
        }

        if (rotateFlag)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 60f, 0), timeCount * rotateSpeed);
            timeCount += Time.deltaTime;
            //rotateFlag = false;
        }

        //if (GameManager.Instance.HasGameStarted == true && !GameManager.Instance.IsGameOver)
        //{
        //    Move();
        //}
    }

    private void Move()
    {
        float pointer_x = Input.GetAxis("Mouse X");

        if (GetComponent<Thief>().IsSprinting)
        {
            MoveDirection = new Vector3(pointer_x * slidingFactor, 0, sprintSpeed * Time.deltaTime);
        }

        if (GetComponent<Thief>().IsSuperFast)
        {
            MoveDirection = new Vector3(pointer_x * slidingFactor, 0, speedySpeed * Time.deltaTime);
        }

        else
        {
            MoveDirection = new Vector3(pointer_x * slidingFactor, 0, VerticalSpeed * Time.deltaTime);
        }

        controller.Move(MoveDirection);

        if (MoveDirection != Vector3.zero)
        {
            transform.forward = MoveDirection;

            Quaternion toRotation = Quaternion.LookRotation(MoveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void MoveTowardsTarget(Vector3 target)
    {
        var offset = target - transform.position;
        if (offset.magnitude > 1.0f)
        {
            offset = offset.normalized * verticalSpeed;
            controller.Move(offset * Time.deltaTime);
        }
    }

    void Rotate()
    {
        rotateFlag = true;
    }

    public Vector3 MoveDirection { get => moveDirection; set => moveDirection = value; }
    public float VerticalSpeed { get => verticalSpeed; set => verticalSpeed = value; }
}
