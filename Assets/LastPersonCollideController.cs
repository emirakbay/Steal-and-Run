using System.Collections.Generic;
using UnityEngine;

public class LastPersonCollideController : MonoBehaviour
{
    private List<GameObject> collidedCubes = new List<GameObject>();

    public Transform spineTransform;

    private ParticleSystem[] particleSystems;

    private Transform[] sideCubeTransforms;

    private Vector3 targetScale;

    private float speed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BonusCube"))
        {
            collidedCubes.Add(other.gameObject);
        }
    }

    private void Start()
    {
        speed = 4.0f;
    }

    private void Update()
    {
        if (collidedCubes.Count > 0)
        {
            if (spineTransform.GetComponent<Rigidbody>().velocity.z <= 0f)
            {
                GameManager.Instance.LevelComplete();

                BonusCubeScaleUp();
                ActivateBonusCubeParticles();
            }
        }
    }

    private void BonusCubeScaleUp()
    {
        sideCubeTransforms = collidedCubes[collidedCubes.Count - 1].GetComponentsInChildren<Transform>();
        foreach (Transform item in sideCubeTransforms)
        {
            if (item.CompareTag("BonusCubeSides"))
            {
                targetScale = new Vector3(item.transform.localScale.x, 5.0f, item.transform.localScale.z);
                item.transform.localScale = Vector3.Lerp(item.transform.localScale, targetScale, speed * Time.deltaTime);
            }
        }
    }

    private void ActivateBonusCubeParticles()
    {
        particleSystems = collidedCubes[collidedCubes.Count - 1].GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem item in particleSystems)
        {
            item.Play();
        }
    }
}
