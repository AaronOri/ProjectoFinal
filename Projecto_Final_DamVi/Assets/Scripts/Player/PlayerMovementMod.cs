
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMod : MonoBehaviour
{
    [SerializeField] float horizontalSpeed = 5f;          // Velocidad horizontal
    [SerializeField] float verticalSpeed = 2f;            // Velocidad vertical (por encima del desplazamiento de la cámara)
    [SerializeField] float verticalControlAmount = 0.5f;  // Cantidad de control vertical
    [SerializeField] float leanAngle = 15f;               // Ángulo de inclinación visual

    private Rigidbody2D rb;  // Referencia al Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Obtener el Rigidbody2D del jugador
    }

    void Update()
    {
        // Obtiene las entradas del jugador para el movimiento horizontal y vertical
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical") * verticalControlAmount;

        // Crear un vector de movimiento basado en las entradas
        Vector2 movement = new Vector2(moveX * horizontalSpeed, verticalSpeed + moveY);

        // Asigna la velocidad del Rigidbody para mover al jugador
        rb.velocity = movement;

        // Rotación visual (inclinación de la nave) basada en la entrada horizontal
        float targetZRotation = -moveX * leanAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, targetZRotation);
    }
}
