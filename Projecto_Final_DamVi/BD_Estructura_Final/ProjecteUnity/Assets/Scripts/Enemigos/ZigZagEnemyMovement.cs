using UnityEngine;

public class ZigZagEnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;             // Velocidad general del movimiento
    [SerializeField] float amplitude = 2f;         // Amplitud del zigzag horizontal
    [SerializeField] float frequency = 2f;         // Frecuencia del zigzag
    [SerializeField] float verticalSpeed = 1f;     // Velocidad constante hacia abajo en el eje Y

    private Vector3 startPosition;                   // Posición inicial del enemigo
    private float previousZigzag = 0f;               // Valor anterior para detectar cambio de dirección
    private bool goingRight = true;                   // Dirección actual del zigzag

    // Ángulo actual de rotación para girar el sprite al cambiar de dirección
    private float currentRotationAngle = -90f;

    void Start()
    {
        startPosition = transform.position;
        previousZigzag = Mathf.Sin(Time.time * frequency);
    }

    void Update()
    {
        // Valor actual del zigzag horizontal
        float currentZigzag = Mathf.Sin(Time.time * frequency);
        float zigzag = currentZigzag * amplitude;

        // Detectar cambio de dirección en el zigzag
        if ((goingRight && currentZigzag < 0) || (!goingRight && currentZigzag > 0))
        {
            // Girar el sprite al cambiar dirección
            transform.Rotate(0f, 0f, currentRotationAngle);
            goingRight = !goingRight;

            // Invertir ángulo para próximo giro
            currentRotationAngle = -currentRotationAngle;
        }

        // Calculamos el vector de movimiento combinando zigzag horizontal y descenso vertical constante
        Vector3 movement = new Vector3(zigzag, -verticalSpeed, 0f).normalized * speed * Time.deltaTime;

        // Aplicamos el movimiento en el espacio mundial
        transform.Translate(movement, Space.World);

        previousZigzag = currentZigzag;
    }
}
