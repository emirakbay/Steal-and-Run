using System.Collections.Generic;
using UnityEngine;

public class ThiefParticleController : MonoBehaviour
{
    private ParticleSystem[] particleSystems;
    private Thief thief;
    private float boostTime = 5.0f;

    [SerializeField] private List<GameObject> particles = new List<GameObject>();

    void Start()
    {
        thief = FindObjectOfType<Thief>();
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        SpeedUpParticleController(false);
        ExplosionParticleController(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedUp"))
        {
            StartCoroutine(thief.SpeedUp(boostTime));
        }
    }

    public void SpeedUpParticleController(bool flag)
    {
        for (int i = 0; i <= 2; i++)
        {
            if (flag)
                particleSystems[i].Play();
            else
                particleSystems[i].Stop();
        }
    }

    public void ExplosionParticleController(bool flag)
    {
        for (int i = particleSystems.Length - 1; i >= particleSystems.Length - 8; i--)
        {
            if (flag)
            {
                particleSystems[i].Play();
            }
            else
                particleSystems[i].Stop();
        }
    }

    public void CreateFlames()
    {
        Vector3 center = transform.position;
        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = RandomCircle(center, 5.0f);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            int index = Random.Range(0, particles.Count);
            Instantiate(particles[index], pos, rot);
        }
    }

    public Vector3 RandomCircle(Vector3 center, float radius)
    {
        float angleX = Random.value * 360;
        float angleZ = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angleX * Mathf.Deg2Rad);
        pos.y = 0.7f;
        pos.z = center.z + radius * Mathf.Sin(angleZ * Mathf.Deg2Rad);
        return pos;
    }
}
