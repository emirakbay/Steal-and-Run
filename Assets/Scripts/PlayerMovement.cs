using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float slidingFactor;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float sprintSpeed;

    private Vector3 moveDirection;

    private CharacterController controller;

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
        if (GameManager.Instance.HasGameStarted == true && !GameManager.Instance.IsGameOver)
        {
            Move();
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");

        if (GetComponent<Thief>().IsSprinting)
        {
            MoveDirection = new Vector3(moveX * slidingFactor, 0, sprintSpeed * Time.deltaTime);
        }

        else
        {
            MoveDirection = new Vector3(moveX * slidingFactor, 0, VerticalSpeed * Time.deltaTime);
        }

        controller.Move(MoveDirection);

        if (MoveDirection != Vector3.zero)
        {
            transform.forward = MoveDirection;

            Quaternion toRotation = Quaternion.LookRotation(MoveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public Vector3 MoveDirection { get => moveDirection; set => moveDirection = value; }
    public float VerticalSpeed { get => verticalSpeed; set => verticalSpeed = value; }
}
