using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    [SerializeField] float horizontalSpeed = 5f;
    [SerializeField] float verticalScrollSpeed = 2f;
    [SerializeField] float verticalControlAmount = 0.5f; // Control vertical
    [SerializeField] float leanAngle = 15f;

    public bool isInvincible = false;  // Para la invencibilidad
    public float invincibilityDuration = 5f;  // Duraci�n de la invencibilidad
    public bool quickshotActive = false;  // Para el power-up de quickshot
    public float quickshotDuration = 5f;  // Duraci�n de quickshot
    public bool tripleshotActive = false;  // Para el power-up de tripleshot

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Movimiento del jugador
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical") * verticalControlAmount;
        Vector3 movement = new Vector3(moveX * horizontalSpeed, verticalScrollSpeed + moveY, 0f) * Time.deltaTime;
        transform.Translate(movement);

        // Inclinaci�n de la nave (efecto est�tico)
        float targetZRotation = -moveX * leanAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, targetZRotation);

        // L�gica para invencibilidad
        if (isInvincible)
        {
            // Hacer que la nave parpadee
            spriteRenderer.enabled = !spriteRenderer.enabled;

            // Temporizador de invencibilidad
            invincibilityDuration -= Time.deltaTime;
            if (invincibilityDuration <= 0)
            {
                isInvincible = false;
                spriteRenderer.enabled = true;  // Asegurarse de que la nave sea visible
            }
        }

        // L�gica para quickshot
        if (quickshotActive)
        {
            quickshotDuration -= Time.deltaTime;
            if (quickshotDuration <= 0)
            {
                quickshotActive = false;
            }
        }

        // L�gica para tripleshot
        if (tripleshotActive)
        {
            // Puedes manejar la mec�nica de tripleshot aqu� o en el script de disparo
        }
    }

    // M�todos para activar power-ups
    public void ActivateInvincibility()
    {
        isInvincible = true;
        invincibilityDuration = 5f;  // Reseteamos el tiempo de invencibilidad
    }

    public void ActivateQuickshot()
    {
        quickshotActive = true;
        quickshotDuration = 5f;  // Reseteamos el tiempo de quickshot
    }

    public void ActivateTripleshot()
    {
        tripleshotActive = true;
    }
}
