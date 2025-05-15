using UnityEngine;

public class TripleShotPowerUp : MonoBehaviour
{
    public float duration = 5f;
    public float speedY = -3f; // Velocidad en el eje Y (hacia abajo)
    public float lifetime = 10f; // Tiempo para destruir el power-up automáticamente

    private void Start()
    {
        // Que el power-up cuelgue de la Main Camera
        Transform mainCameraTransform = Camera.main?.transform;
        if (mainCameraTransform != null)
        {
            transform.SetParent(mainCameraTransform);
        }

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
        if (other.CompareTag("Player"))
        {
            PlayerShooting shooting = other.GetComponent<PlayerShooting>();
            if (shooting != null)
            {
                shooting.ActivateTripleShot(duration);
            }

            Destroy(gameObject); // Elimina el power-up una vez recogido
        }
    }
}

