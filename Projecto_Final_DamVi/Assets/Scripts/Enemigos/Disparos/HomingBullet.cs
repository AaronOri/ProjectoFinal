using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float homingDuration = 1.5f;
    [SerializeField] float rotationSpeed = 200f;
    [SerializeField] float lifetime = 5f;
    [SerializeField] float startTrackingDelay = 1f;

    Transform player;
    Rigidbody2D rb;
    float homingTimer = 0f;
    float trackingTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player")?.transform;
        homingTimer = homingDuration;
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            trackingTimer += Time.fixedDeltaTime;

            if (trackingTimer >= startTrackingDelay)
            {
                if (homingTimer > 0f)
                {
                    Vector2 direction = ((Vector2)player.position - rb.position).normalized;
                    float rotateAmount = Vector3.Cross(direction, transform.up).z;
                    rb.angularVelocity = -rotateAmount * rotationSpeed;
                }
                else
                {
                    rb.angularVelocity = 0f;
                }

                rb.velocity = transform.up * speed;
                homingTimer -= Time.fixedDeltaTime;
            }
            else
            {
                rb.velocity = transform.up * speed;
            }
        }
    }
}
