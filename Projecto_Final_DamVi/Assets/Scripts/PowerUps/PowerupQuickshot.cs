using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupQuickshot : MonoBehaviour
{
    [SerializeField] private float effectDuration = 5f; // Duraci�n del power-up
    private SimplePlayerMovement playerMovement;

    void Start()
    {
        // Aseg�rate de que el componente SimplePlayerMovement est� presente
        playerMovement = FindObjectOfType<SimplePlayerMovement>();  // O usa GetComponent si el powerup est� en el mismo GameObject
        if (playerMovement == null)
        {
            Debug.LogError("No se encontr� el componente SimplePlayerMovement en el GameObject del jugador.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aplica el efecto de Quickshot
            if (playerMovement != null)
            {
                playerMovement.ActivateQuickshot();  // Aseg�rate de que ActivateQuickshot est� implementado correctamente
                Destroy(gameObject);  // Destruir el powerup despu�s de que se haya recogido
            }
        }
    }
}


