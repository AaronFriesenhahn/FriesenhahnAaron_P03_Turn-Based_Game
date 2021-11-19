using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifetimeScript : MonoBehaviour
{
    [SerializeField] float _ParticleLifetime;

    bool StartCountdown = false;

    // Update is called once per frame
    void Update()
    {
        if (StartCountdown == false)
        {
            StartCountdown = true;
            StartCoroutine(LifetimeCountdown(_ParticleLifetime));
        }
    }

    IEnumerator LifetimeCountdown(float value)
    {
        yield return new WaitForSeconds(value);
        Destroy(gameObject);
    }
}
