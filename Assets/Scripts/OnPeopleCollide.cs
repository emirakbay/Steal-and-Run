using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPeopleCollide : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
    }
}
