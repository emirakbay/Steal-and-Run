using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public Transform start, end;

    public float speed;

    private Transform obstacle;
    private Vector3 lerpStart, lerpEnd;

    private void Awake()
    {
        obstacle = GetComponent<Transform>();
        lerpStart = new Vector3(start.transform.position.x, transform.position.y, transform.position.z);
        lerpEnd = new Vector3(end.transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        LerpBetweenPoints();

        if (GameManager.Instance.IsGameOver)
            GetComponent<MeshCollider>().isTrigger = false;
    }

    private void LerpBetweenPoints()
    {
        transform.position = Vector3.Lerp(lerpStart, lerpEnd, Mathf.PingPong(Time.time * speed, 1.0f));
    }

    //private void Rotate()
    //{
    //    obstacle.transform.Rotate(zAngle * rotationSpeed, obstacle.transform.rotation.y, obstacle.transform.rotation.z, Space.Self);
    //}
}
