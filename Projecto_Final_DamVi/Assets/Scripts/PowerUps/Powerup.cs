using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Tipo de power-up
    public PowerUpType powerUpType;  // Enum para gestionar los tipos de power-ups

    // Método que detecta la colisión con el jugador
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificamos si el objeto que ha colisionado tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Intentamos obtener el componente SimplePlayerMovement del objeto que ha colisionado
            SimplePlayerMovement player = other.GetComponent<SimplePlayerMovement>();

            // Si el componente SimplePlayerMovement no es null
            if (player != null)
            {
                // Activar el power-up según el tipo
                switch (powerUpType)
                {
                    case PowerUpType.Invencibilidad:
                        player.ActivateInvincibility();
                        break;
                    case PowerUpType.Quickshot:
                        player.ActivateQuickshot();
                        break;
                    case PowerUpType.Tripleshot:
                        player.ActivateTripleshot();
                        break;
                }

                // Destruir el power-up después de que haya sido recogido
                Destroy(gameObject);
            }
            else
            {
                // Si no tiene el componente SimplePlayerMovement, mostramos un mensaje de advertencia
                Debug.LogWarning("El objeto que ha colisionado no tiene el componente SimplePlayerMovement.");
            }
        }
    }
}

// Enum para gestionar los tipos de power-ups
public enum PowerUpType
{
    Invencibilidad,
    Quickshot,
    Tripleshot
}
