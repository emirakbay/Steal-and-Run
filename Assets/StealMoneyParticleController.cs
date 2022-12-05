using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealMoneyParticleController : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void StealMoneyParticle()
    {
        _particleSystem.Play();
    }
}
