using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;     // Prefab del proyectil
    [SerializeField] float bulletSpeed = 10f;     // Velocidad del disparo
    [SerializeField] Transform firePoint;         // Punto desde donde se dispara
    [SerializeField] float shootCooldown = 0.25f; // Tiempo mínimo entre disparos

    float cooldownTimer = 0f;
    SimplePlayerMovement playerMovement; // Declaramos la variable

    void Start()
    {
        // Si el SimplePlayerMovement está en otro GameObject, lo buscamos
        playerMovement = GameObject.Find("Player").GetComponent<SimplePlayerMovement>();

        if (playerMovement == null)
        {
            Debug.LogError("No se encontró el componente SimplePlayerMovement en el GameObject 'Player'.");
        }
    }

    void Update()
    {
        if (playerMovement == null) return;  // Si no hay un componente SimplePlayerMovement, no hacemos nada.

        cooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer <= 0f)
        {
            Shoot();
            cooldownTimer = shootCooldown;
        }

        // Si el power-up Quickshot está activo, reducir el tiempo de espera entre disparos
        if (playerMovement.quickshotActive)
        {
            shootCooldown = 0.1f;  // Disparo más rápido
        }
        else
        {
            shootCooldown = 0.25f;  // Cadencia normal
        }
    }

    void Shoot()
    {
        // Si Tripleshot está activo, disparar tres proyectiles
        if (playerMovement.tripleshotActive)
        {
            // Disparo central
            InstantiateBullet(Vector3.zero);

            // Disparos diagonales
            InstantiateBullet(new Vector3(-0.5f, 0.5f, 0));  // Diagonal izquierda
            InstantiateBullet(new Vector3(0.5f, 0.5f, 0));   // Diagonal derecha
        }
        else
        {
            // Si Tripleshot no está activo, disparar solo uno
            InstantiateBullet(Vector3.zero);
        }
    }

    // Método para instanciar un proyectil
    void InstantiateBullet(Vector3 offset)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position + offset, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.up * bulletSpeed;
        }
    }
}

