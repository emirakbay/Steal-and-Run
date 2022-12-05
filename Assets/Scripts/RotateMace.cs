using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMace : MonoBehaviour
{
    public GameObject pressable;

    void Update()
    {
        if (pressable.GetComponent<PressButton>().IsPressed)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 25.0f), 5.0f * Time.deltaTime);
        }
    }
}
