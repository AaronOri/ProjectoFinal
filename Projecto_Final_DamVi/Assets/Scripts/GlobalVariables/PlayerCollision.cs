using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerHealth playerHealth;

    [SerializeField] private AudioClip powerUpSound;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet") || other.CompareTag("Boss"))
        {
            playerHealth.RebDany();

            if (other.CompareTag("EnemyBullet"))
            {
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("PowerUp"))
        {
            if (audioSource != null && powerUpSound != null)
            {
                audioSource.PlayOneShot(powerUpSound);
            }
        }
    }
}
