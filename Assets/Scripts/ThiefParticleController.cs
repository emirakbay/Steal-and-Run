using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefParticleController : MonoBehaviour
{
    private ParticleSystem[] particleSystems;
    private Thief thief;
    private float boostTime = 5.0f;

    void Start()
    {
        thief = FindObjectOfType<Thief>();
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        StopSpeedUpParticle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedUp"))
        {
            StartCoroutine(thief.SpeedUp(boostTime));
        }
    }

    public void PlaySpeedUpParticle()
    {
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            if (particleSystem.name == "ExplosionParticle" || particleSystem.name == "Debris")
                particleSystem.Stop();
            else
                particleSystem.Play();
        }
    }
    public void StopSpeedUpParticle()
    {
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Stop();
        }
    }
}
