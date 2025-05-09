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
            Shoot(Vector2.up);
            cooldownTimer = shootCooldown;
        }
    }

    void Shoot(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction.normalized * bulletSpeed;
        }
    }
}
