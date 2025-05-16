using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] Transform firePoint;
    [SerializeField] bool isPlayer = true;
    [SerializeField] float shootCooldown = 0.25f;
    [SerializeField] float initialCooldown = 2f; //  Ahora configurable en el editor

    float cooldownTimer;

    void Start()
    {
        cooldownTimer = initialCooldown; //  Usamos el valor del Inspector
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (isPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer <= 0f)
            {
                Shoot(Vector2.up);
                cooldownTimer = shootCooldown;
            }
        }
        else
        {
            if (cooldownTimer <= 0f)
            {
                Shoot(Vector2.down);
                cooldownTimer = shootCooldown;
            }
        }
    }

    void Shoot(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Hacer que la bala sea hija de la cámara principal
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            bullet.transform.SetParent(mainCam.transform);
        }

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction.normalized * bulletSpeed;
        }
    }

}

