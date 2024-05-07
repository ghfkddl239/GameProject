using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEffects : MonoBehaviour
{
    ParticleSystem fireEffect;

    private void OnEnable()
    {
        if (fireEffect is null)
        {
            fireEffect = transform.Find("FireEffect").GetComponent<ParticleSystem>();
        }
        fireEffect.Play();
    }

    private void OnDisable()
    {
        if (fireEffect is null)
        {
            fireEffect = transform.Find("FireEffect").GetComponent<ParticleSystem>();
        }
        fireEffect.Stop();
    }
}
