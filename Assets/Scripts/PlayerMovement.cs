using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float slidingFactor;
    [SerializeField] private float rotationSpeed;

    private Vector3 moveDirection;

    private CharacterController controller;

    public Vector3 MoveDirection { get => moveDirection; set => moveDirection = value; }

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
        if (GameManager.Instance.HasGameStarted == true)
        {
            Move();
        }
    }

    private void Move()
    {

        float moveX = Input.GetAxis("Horizontal");

        MoveDirection = new Vector3(moveX * slidingFactor, 0, verticalSpeed * Time.deltaTime);

        controller.Move(MoveDirection);

        if (MoveDirection != Vector3.zero)
        {
            transform.forward = MoveDirection;

            Quaternion toRotation = Quaternion.LookRotation(MoveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
