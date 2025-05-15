using System.Collections;
using UnityEngine;

public class PowerupInvencibilidad : MonoBehaviour
{
    [SerializeField] private float invincibilityDuration = 5f; // Duración del power-up
    [SerializeField] private float speedY = -3f; // Velocidad en el eje Y (puede ser negativa para bajar)
    [SerializeField] private float lifetime = 10f; // Tiempo para destruir el power-up automáticamente

    private SimplePlayerMovement activeInve;

    private void Start()
    {
        // Destruir el objeto automáticamente tras 'lifetime' segundos
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Movimiento constante en el eje Y
        transform.Translate(Vector3.up * speedY * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que colisiona tiene el tag "Player"
        if (other.CompareTag("Player"))
        {

            // Obtener referencia al script SimplePlayerMovement para activar invencibilidad
            activeInve = other.GetComponent<SimplePlayerMovement>();

            if (activeInve != null && !activeInve.isInvincible)
            {
                // Activar invencibilidad y comunicar al sistema de salud
                activeInve.ActivateInvencibili();
            }

            // Obtener referencia al script PlayerHealth para activar invencibilidad
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null && !playerHealth.isInvulnerable)
            {
                // Activar invencibilidad en el sistema de salud
                playerHealth.ActivateInvincibility(invincibilityDuration);

            }
            else
            {
                Debug.LogWarning("PlayerHealth no encontrado en Player o ya está invulnerable.");
            }

            // Destruir el power-up después de activarlo
            Destroy(gameObject);
        }
    }
}