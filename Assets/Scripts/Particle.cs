using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    public bool _isStopped = false;
    private void Awake()
    {
        _particleSystem = gameObject.GetComponent<ParticleSystem>();
    }
    public void PlayAnimation()
    {
        _particleSystem.Play();


    }

}
