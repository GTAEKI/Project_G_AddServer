using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;


public class ParticleColorChange : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem hitPS;
    private ParticleSystem[] hitPSArray = new ParticleSystem[3];
    private ParticleSystem.MainModule[] hitmain = new ParticleSystem.MainModule[3];

    void Awake()
    {
        hitPS = GetComponent<ParticleSystem>();
        hitPSArray = hitPS.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < hitPSArray.Length; i++)
        {
            hitmain[i] = hitPSArray[i].main;
        }
    }
    public void ChangeParticleColor(EColorType type = EColorType.White)
    {
        switch (type)
        {
            case EColorType.Red:
                for (int i = 0; i < hitmain.Length; i++)
                {
                    hitmain[i].startColor = Color.red;
                }
                break;
            case EColorType.Yellow:
                for (int i = 0; i < hitmain.Length; i++)
                {
                    hitmain[i].startColor = Color.yellow;
                }
                break;
            default:
                for (int i = 0; i < hitmain.Length; i++)
                {
                    hitmain[i].startColor = Color.white;
                }
                break;
        }
    }
}
