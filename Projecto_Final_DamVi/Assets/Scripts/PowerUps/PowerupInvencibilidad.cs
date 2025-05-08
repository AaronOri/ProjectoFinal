using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupInvencibilidad : MonoBehaviour
{
    [SerializeField] private float invincibilityDuration = 5f; // Duración del power-up
    [SerializeField] private SpriteRenderer playerSprite; // El sprite del jugador
    [SerializeField] private float blinkInterval = 0.2f; // Intervalo de parpadeo

    private bool isInvincible = false;
    private float invincibilityTimer = 0f;

    void Start()
    {
        if (playerSprite == null)
        {
            playerSprite = GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            if (invincibilityTimer <= 0f)
            {
                EndInvincibility();
            }
        }
    }

    public void ActivateInvincibility()
    {
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
        StartCoroutine(BlinkEffect());
    }

    private IEnumerator BlinkEffect()
    {
        while (isInvincible)
        {
            playerSprite.enabled = !playerSprite.enabled; // Alterna la visibilidad del sprite
            yield return new WaitForSeconds(blinkInterval);
        }

        playerSprite.enabled = true; // Asegura que el sprite esté visible al terminar
    }

    private void EndInvincibility()
    {
        isInvincible = false;
        StopCoroutine(BlinkEffect());
        playerSprite.enabled = true; // Asegura que el sprite esté visible al final
    }

    // Método para activar la invencibilidad (puede llamarlo un power-up)
    public void OnPickup()
    {
        ActivateInvincibility();
    }
}

