using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCallback : MonoBehaviour
{
    ParticleSystem main;
    void Awake()
    {
        main = GetComponent<ParticleSystem>();
    }
    void OnEnable()
    {
        main.Play();
    }
    public void OnParticleSystemStopped()
    {
        
        Managers.Projectile.Enqueue(this.gameObject,"Fx");
        gameObject.SetActive(false);
    } 

}
