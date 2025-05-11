using UnityEngine;

public class EnemyHomingShooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float shootCooldown = 1f;
    [SerializeField] float stopShootingAfter = 2f;
    [SerializeField] float bulletSpeed = 10f;

    float cooldownTimer = 0f;
    float lifeTimer = 0f;
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

        if (lifeTimer >= stopShootingAfter) return;

        if (cooldownTimer <= 0f)
        {
            Vector2 directionToPlayer = player.position - firePoint.position;
            Shoot(directionToPlayer);
            cooldownTimer = shootCooldown;
        }
    }

    void Shoot(Vector2 direction)
    {
        // Calcula el ángulo hacia el jugador
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // Instancia la bala con rotación correcta
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction.normalized * bulletSpeed;
        }
    }
}
