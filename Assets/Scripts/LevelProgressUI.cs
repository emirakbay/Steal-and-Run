using UnityEngine;
using UnityEngine.UI;

public class LevelProgressUI : MonoBehaviour
{
    [SerializeField] private Image uiFillImage;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform endLineTransform;

    private Vector3 endLinePosition;
    private float fullDistance;

    private void Start()
    {
        endLinePosition = endLineTransform.position;
        fullDistance = GetDistance();
    }

    private float GetDistance()
    {
        // Slow
        //return Vector3.Distance(playerTransform.position, endLinePosition);

        //Fast
        return (endLinePosition - playerTransform.position).sqrMagnitude;
    }


    private void UpdateProgressFill(float value)
    {
        uiFillImage.fillAmount = value;
    }


    private void Update()
    {
        // check if the player doesn't pass the End Line
        if (playerTransform.position.z <= endLinePosition.z)
        {
            float newDistance = GetDistance();
            float progressValue = Mathf.InverseLerp(fullDistance, 0f, newDistance);

            UpdateProgressFill(progressValue);
        }
    }
}