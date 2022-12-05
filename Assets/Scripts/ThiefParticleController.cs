using System.Collections.Generic;
using UnityEngine;

public class ThiefParticleController : MonoBehaviour
{
    private ParticleSystem[] particleSystems;

    void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
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
        for (int i = particleSystems.Length - 7; i < particleSystems.Length - 4; i++)
        {
            if (flag)
                particleSystems[i].Play();
            else
                particleSystems[i].Stop();
        }
    }

    public void ExplosionSparksController(bool flag)
    {
        for (int i = particleSystems.Length - 3; i < particleSystems.Length; i++)
        {
            if (flag)
                particleSystems[i].Play();
            else
                particleSystems[i].Stop();
        }
    }
}
