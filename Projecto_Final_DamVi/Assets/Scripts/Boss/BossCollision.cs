using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollision : MonoBehaviour
{
    private BossHealth bossHealth;

    private void Start()
    {
        bossHealth = GetComponent<BossHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            bossHealth.TakeDamage(2); // Pots ajustar el valor
            Destroy(other.gameObject);
        }
    }
}

