using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public Transform start, end;
    
    public float speed;
    public float rotationSpeed;
    public float xAngle, yAngle, zAngle;

    private Transform obstacle;
    private Vector3 lerpStart, lerpEnd;

    private void Awake()
    {
        obstacle = GetComponent<Transform>();
    }

    void Start()
    {
        transform.parent = null;
        lerpStart = start.transform.position;
        lerpEnd = end.transform.position;
    }

    void Update()
    {
        Rotate();
        LerpBetweenPoints();

        if (GameManager.Instance.IsGameOver)
            GetComponent<MeshCollider>().isTrigger = false;
    }

    private void LerpBetweenPoints()
    {
        transform.position = Vector3.Lerp(lerpStart, lerpEnd, Mathf.PingPong(Time.time * speed, 1.0f));
    }

    private void Rotate()
    {
        obstacle.transform.Rotate(obstacle.transform.rotation.x, obstacle.transform.rotation.y, zAngle * rotationSpeed, Space.Self);
    }
}
