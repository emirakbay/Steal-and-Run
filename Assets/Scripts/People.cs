using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class People : MonoBehaviour
{
    private bool _isChasing = false;
    private bool _isNervous = false;

    private float speed = 2f;

    Vector3 relativePos;
    Quaternion targetRotation;
    public Transform target;
    bool rotating = false;
    float rotationTime;

    private void Awake()
    {
    }

    private void Update()
    {
        Vector3 moveDir = FindObjectOfType<PlayerMovement>().MoveDirection;


        if (IsNervous)
            Rotate();

        if (IsChasing)
        {
            transform.position += moveDir * speed * Time.deltaTime;

            if (moveDir != Vector3.zero)
            {
                transform.forward = moveDir;

                Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speed * Time.deltaTime);

            }
        }
    }

    public bool IsChasing { get => _isChasing; set => _isChasing = value; }
    public bool IsNervous { get => _isNervous; set => _isNervous = value; }

    public void Rotate()
    {
        relativePos = target.position + transform.position;
        targetRotation = Quaternion.LookRotation(relativePos);
        rotating = true;

        if (rotating)
        {
            Debug.Log("started");
            rotationTime += Time.deltaTime * 1.0f;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationTime * Time.deltaTime);
        }

        if (rotationTime > 0.2f)
        {
            Debug.Log("finished");
            rotating = false;
        }
    }
}

