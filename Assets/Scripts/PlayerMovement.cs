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


    [HideInInspector] public float horizontalSwipeConstant;
    [HideInInspector] public float verticalSwipeConstant;

    [SerializeField] private float m_VerticalInputMultiplier, m_HorizontalInputMultiplier;
    [SerializeField] private float m_ReferenceWidth, m_ReferenceHeight;

    private int screenWidth;
    private int screenHeight;

    private Vector3? currentFrameMousePos;
    private Vector3? lastFrameMousePos;

    #region SmoothDampValues
    private readonly float smoothDampTime = 0.075f;
    private readonly float smoothDampMaxVelocity = 360f;
    private float horizontalSmoothDampReference, verticalSmoothDampReference;
    #endregion


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    private void Update()
    {
        if (GameManager.Instance.HasGameStarted == true)
        {
            GetComponent<Thief>().IsRunning = true;
        }

        GetInputs();
        CalculateSwipeConstants();
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
        //float pointer_x = Input.GetAxis("Mouse X");

        if (GetComponent<Thief>().IsSprinting)
        {
            MoveDirection = new Vector3(horizontalSwipeConstant * slidingFactor * Time.deltaTime, 0, sprintSpeed * Time.deltaTime);
        }

        if (GetComponent<Thief>().IsSuperFast)
        {
            MoveDirection = new Vector3(horizontalSwipeConstant * slidingFactor * Time.deltaTime, 0, speedySpeed * Time.deltaTime);
        }

        else
        {
            MoveDirection = new Vector3(horizontalSwipeConstant * slidingFactor * Time.deltaTime, 0, VerticalSpeed * Time.deltaTime);
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

    private void CalculateSwipeConstants()
    {
        if (lastFrameMousePos.HasValue)
        {
            CalculateSwipeConstant();
        }
        else
        {
            lastFrameMousePos = currentFrameMousePos;
            return;
        }

        lastFrameMousePos = currentFrameMousePos;
    }

    private void GetInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentFrameMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            currentFrameMousePos = Input.mousePosition;
        }
        else
        {
            currentFrameMousePos = lastFrameMousePos = null;
            horizontalSwipeConstant = 0;
            verticalSwipeConstant = 0;
        }

    }

    private void CalculateSwipeConstant()
    {
        var mousePosDifferenceByPixel = currentFrameMousePos - lastFrameMousePos;
        horizontalSwipeConstant = Mathf.SmoothDamp(horizontalSwipeConstant, mousePosDifferenceByPixel.Value.x * (m_ReferenceWidth / screenWidth) * m_HorizontalInputMultiplier, ref horizontalSmoothDampReference, smoothDampTime, smoothDampMaxVelocity);
        verticalSwipeConstant = Mathf.SmoothDamp(verticalSwipeConstant, mousePosDifferenceByPixel.Value.y * (m_ReferenceHeight / screenHeight) * m_VerticalInputMultiplier, ref verticalSmoothDampReference, smoothDampTime, smoothDampMaxVelocity);
    }

    public Vector3 MoveDirection { get => moveDirection; set => moveDirection = value; }
    public float VerticalSpeed { get => verticalSpeed; set => verticalSpeed = value; }
}
