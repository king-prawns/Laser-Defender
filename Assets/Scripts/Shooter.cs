using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;

    public bool isFiring;

    Coroutine firingCouroutine;

    void Start()
    {
        
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCouroutine == null)
        {
            firingCouroutine = StartCoroutine(FireContinuosly());
        }
        else if (!isFiring && firingCouroutine != null)
        {
            StopCoroutine(firingCouroutine);
            firingCouroutine = null;
        }
    }

    IEnumerator FireContinuosly()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab,
                                transform.position,
                                Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifetime);
            yield return new WaitForSeconds(firingRate);
        }
    }
}
