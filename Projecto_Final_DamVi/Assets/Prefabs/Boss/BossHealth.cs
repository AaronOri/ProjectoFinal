using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Slider healthSlider;                 // slider en comptes d’Image
    [SerializeField] private GameObject explosionEffect;

    private int currentHealth;
    private bool isPhase2 = false;

    private BossShooter shooter;
    private BossMovement movement;
    private BossScore bossScore;  // si el fas servir

    void Start()
    {
        currentHealth = maxHealth;

        shooter = GetComponent<BossShooter>();
        movement = GetComponent<BossMovement>();
        bossScore = GetComponent<BossScore>();

        if (healthSlider != null)
        {
            healthSlider.minValue = 0;               // ✅ Correcció afegida aquí
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (healthSlider != null)
            healthSlider.value = currentHealth;

        if (!isPhase2 && currentHealth <= maxHealth / 4)
        {
            isPhase2 = true;

            shooter?.EnterPhase2();
            movement?.EnterPhase2();

            // explosions cada 0.5 s i lligades al boss
            InvokeRepeating(nameof(RandomExplosion), 0f, 0.5f);
        }

        if (currentHealth <= 0) Die();
    }

    void RandomExplosion()
    {
        Vector3 offset = new Vector3(Random.Range(-2f, 2f), Random.Range(-1f, 1f), 0f);

        // parent = transform  -> l’explosió es mou amb el boss
        Instantiate(explosionEffect, transform.position + offset, Quaternion.identity, transform);
    }

    void Die()
    {
        CancelInvoke();                       // atura explosions de fase 2
        Instantiate(explosionEffect, transform.position, Quaternion.identity, null);

        bossScore?.GiveScore();               // punts (si uses BossScore.cs)

        Destroy(gameObject);
    }
}

