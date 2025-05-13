using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerHealth playerHealth;

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
    }
}

