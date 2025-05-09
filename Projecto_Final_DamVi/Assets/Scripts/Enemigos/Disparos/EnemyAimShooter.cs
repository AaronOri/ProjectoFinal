using UnityEngine;

public class EnemyAimShooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] Transform firePoint;
    [SerializeField] float shootCooldown = 1f;
    [SerializeField] float stopShootingAfter = 2f; //  Tiempo máximo disparando (ajustable)

    float cooldownTimer = 0f;
    float lifeTimer = 0f; // Cuenta cuánto tiempo ha pasado desde que el enemigo apareció
    Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        cooldownTimer = shootCooldown;
    }

    void Update()
    {
        if (player == null) return;

        cooldownTimer -= Time.deltaTime;
        lifeTimer += Time.deltaTime;

        //  Si ya pasó el tiempo, dejar de disparar
        if (lifeTimer >= stopShootingAfter) return;

        if (cooldownTimer <= 0f)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;
            Shoot(direction);
            cooldownTimer = shootCooldown;
        }
    }

    void Shoot(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.Euler(0f, 0f, angle + 90f) // Ajuste porque la bala apunta hacia abajo
        );

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }
    }
}
