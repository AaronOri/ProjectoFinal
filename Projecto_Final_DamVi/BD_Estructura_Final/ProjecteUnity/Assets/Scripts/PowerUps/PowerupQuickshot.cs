using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupQuickshot : MonoBehaviour
{
    [SerializeField] private float effectDuration = 5f; // Duración del power-up
    [SerializeField] private float lifetime = 10f; // Tiempo para destruir el power-up automáticamente
    [SerializeField] private float speedY = -3f; // Velocidad en el eje Y (puede ser negativa para bajar)

    private SimplePlayerMovement playerMovement;

    void Start()
    {
        // Asegúrate de que el componente SimplePlayerMovement esté presente
        playerMovement = FindObjectOfType<SimplePlayerMovement>();  // O usa GetComponent si el powerup está en el mismo GameObject
        if (playerMovement == null)
        {
            Debug.LogError("No se encontró el componente SimplePlayerMovement en el GameObject del jugador.");
        }

        // Que el power-up cuelgue de la Main Camera
        Transform mainCameraTransform = Camera.main?.transform;
        if (mainCameraTransform != null)
        {
            transform.SetParent(mainCameraTransform);
        }

        // Destruir el objeto automáticamente tras 'lifetime' segundos
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Movimiento constante en el eje Y
        transform.Translate(Vector3.up * speedY * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aplica el efecto de Quickshot
            if (playerMovement != null)
            {
                playerMovement.ActivateQuickshot();  // Asegúrate de que ActivateQuickshot esté implementado correctamente
                Destroy(gameObject);  // Destruir el powerup después de que se haya recogido
            }
        }
    }
}
