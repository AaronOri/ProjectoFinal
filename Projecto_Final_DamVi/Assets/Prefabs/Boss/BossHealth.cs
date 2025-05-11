using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private GameObject explosionEffect;

    private int currentHealth;
    private bool isPhase2 = false;
    private BossShooter shooter;

    void Start()
    {
        currentHealth = maxHealth;
        shooter = GetComponent<BossShooter>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBarFill.fillAmount = (float)currentHealth / maxHealth;

        if (!isPhase2 && currentHealth <= maxHealth / 2)
        {
            isPhase2 = true;
            shooter.EnterPhase2();
            InvokeRepeating(nameof(RandomExplosion), 0f, 0.5f);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void RandomExplosion()
    {
        Vector3 offset = new Vector3(
            Random.Range(-2f, 2f),
            Random.Range(-1f, 1f),
            0f
        );
        Instantiate(explosionEffect, transform.position + offset, Quaternion.identity);
    }

    void Die()
    {
        // Lógica de muerte final, animación, efectos, etc.
        Destroy(gameObject);
    }
}

