using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Slider healthSlider;                
    [SerializeField] private GameObject explosionEffect;

    [SerializeField] private AudioClip phase2LoopSound;
    [SerializeField] private AudioSource phase2AudioSource;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioSource audioSource;

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
            healthSlider.minValue = 0;               
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"Boss recibió daño. Vida actual: {currentHealth}");

        if (healthSlider != null)
            healthSlider.value = currentHealth;

        if (!isPhase2 && currentHealth <= maxHealth / 4)
        {
            isPhase2 = true;
            shooter?.EnterPhase2();
            movement?.EnterPhase2();

            if (phase2AudioSource != null && phase2LoopSound != null)
            {
                phase2AudioSource.clip = phase2LoopSound;
                phase2AudioSource.loop = true;
                phase2AudioSource.Play();
            }

            InvokeRepeating(nameof(RandomExplosion), 0f, 0.5f);
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Boss debería morir ahora.");
            Die();
        }
    }


    void RandomExplosion()
    {
        Vector3 offset = new Vector3(Random.Range(-2f, 2f), Random.Range(-1f, 1f), 0f);

        // parent = transform  -> l’explosió es mou amb el boss
        Instantiate(explosionEffect, transform.position + offset, Quaternion.identity, transform);
    }

    void Die()
    {
        CancelInvoke();

        if (audioSource != null && deathSound != null)
            audioSource.PlayOneShot(deathSound);

        Instantiate(explosionEffect, transform.position, Quaternion.identity, null);

        bossScore?.GiveScore();

        Destroy(gameObject);
    }
}

