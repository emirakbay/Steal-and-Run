using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    private Transform obstacle;
    public float xAngle, yAngle, zAngle;

    public Transform start, end;
    private Vector3 lerpStart, lerpEnd;


    private void Awake()
    {
        obstacle = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        lerpStart = start.transform.position;
        lerpEnd = end.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        LerpBetweenPoints();

        if (GameManager.Instance.IsGameOver)
            GetComponent<BoxCollider>().isTrigger = false;
    }

    private void LerpBetweenPoints()
    {
        transform.position = Vector3.Lerp(lerpStart, lerpEnd, Mathf.PingPong(Time.time * speed, 1.0f));
    }

    private void Rotate()
    {
        obstacle.transform.Rotate(obstacle.transform.rotation.x, obstacle.transform.rotation.y, zAngle * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
