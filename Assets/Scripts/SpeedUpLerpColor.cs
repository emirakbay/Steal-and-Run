using UnityEngine;

public class SpeedUpLerpColor : MonoBehaviour
{
    private MeshRenderer speedUpMeshRenderer;

    [SerializeField] private Color[] myColor;

    [SerializeField] private int colorIndex;

    [SerializeField] private float lerpTime;

    [SerializeField] private float t;

    void Start()
    {
        speedUpMeshRenderer = GetComponent<MeshRenderer>();

        speedUpMeshRenderer.material.color = Color.green;
    }

    void Update()
    {
        speedUpMeshRenderer.material.color = Color.Lerp(speedUpMeshRenderer.material.color, myColor[colorIndex], lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);

        if (t > 0.9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= myColor.Length) ? 0 : colorIndex;
        }
    }
}
