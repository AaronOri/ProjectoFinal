using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] Transform firePoint;
    [SerializeField] bool isPlayer = true;
    [SerializeField] float shootCooldown = 0.25f;

    float cooldownTimer = 0f;

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
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction.normalized * bulletSpeed;
        }
    }
}
