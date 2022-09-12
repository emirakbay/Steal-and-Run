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

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (GameManager.Instance.HasGameStarted == true)
        {
            Move();
        }
    }
    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX * slidingFactor, 0, verticalSpeed * Time.deltaTime);

        controller.Move(moveDirection);

        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;

            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
