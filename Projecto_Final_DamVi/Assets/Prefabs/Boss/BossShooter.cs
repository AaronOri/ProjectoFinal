using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 1.5f;
    [SerializeField] private float bulletSpeed = 5f;

    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Fire();
            timer = fireRate;
        }
    }

    void Fire()
    {
        if (firePoints.Length == 0) return;

        // Tria un fire point aleatori
        Transform randomPoint = firePoints[Random.Range(0, firePoints.Length)];

        // Instancia la bala i aplica velocitat cap avall
        GameObject bullet = Instantiate(bulletPrefab, randomPoint.position, randomPoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = -randomPoint.up * bulletSpeed;
        }
    }

    public void EnterPhase2()
    {
        fireRate *= 0.5f; // Doble de velocitat de dispar
    }
}

