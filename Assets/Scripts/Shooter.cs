using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    float projectileSpeed = 20f;
    float projectileLifetime = 5f;
    float baseFiringRate = 0.15f;
    [SerializeField] bool useAI;
    float firingRateVariance = 0f;
    float minimumFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
    
    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if(useAI)
        {
            isFiring = true;
            projectileSpeed = 7f;
            baseFiringRate = 0.4f;
            firingRateVariance = 0.1f;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isFiring && firingCoroutine == null){
            firingCoroutine = StartCoroutine(FireContinuously());
        } else if (!isFiring && firingCoroutine != null) {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while(true) {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

            if(rb != null) { rb.velocity = transform.up * projectileSpeed;}
            Destroy(instance, projectileLifetime);

            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            audioPlayer.PlayerShootingClip();

            yield return new WaitForSecondsRealtime(timeToNextProjectile);
        }
    }
}
