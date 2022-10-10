using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
    private bool isPressed = false;

    //Vector3(-17.3999996,3.8599999,7.69999981)

    [SerializeField] private Transform moveableWall;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = new Vector3(17.4f, moveableWall.transform.position.y, moveableWall.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPressed = true;
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y - 0.8f, transform.localPosition.z), 0.7f);
        }
    }

    private void Update()
    {
        if (IsPressed)
            MoveWall();
    }

    private void MoveWall()
    {
        moveableWall.transform.position = Vector3.Lerp(moveableWall.position, targetPosition, 2.0f * Time.deltaTime);
    }

    public bool IsPressed { get => isPressed; set => isPressed = value; }
}
