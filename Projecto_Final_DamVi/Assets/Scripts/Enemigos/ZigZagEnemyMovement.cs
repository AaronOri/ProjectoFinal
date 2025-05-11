using UnityEngine;

public class ZigZagEnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float amplitude = 2f;
    [SerializeField] float frequency = 2f;

    private Vector3 startPosition;
    private float previousZigzag = 0f;
    private bool goingRight = true;

    // Variable per controlar l'angle de gir
    private float currentRotationAngle = -90f;

    void Start()
    {
        startPosition = transform.position;
        previousZigzag = Mathf.Sin(Time.time * frequency);
    }

    void Update()
    {
        float currentZigzag = Mathf.Sin(Time.time * frequency);
        float zigzag = currentZigzag * amplitude;

        // Detecta canvi de direcció
        if ((goingRight && currentZigzag < 0) || (!goingRight && currentZigzag > 0))
        {
            // Canvi de direcció: gira el sprite amb l'angle actual
            transform.Rotate(0f, 0f, currentRotationAngle);
            goingRight = !goingRight;

            // Alterna l'angle de gir per la propera vegada
            currentRotationAngle = -currentRotationAngle;
        }

        Vector3 movement = new Vector3(zigzag, -1f, 0f).normalized * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        previousZigzag = currentZigzag;
    }
}

