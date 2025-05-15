using UnityEngine;

public class EnemyScore1 : MonoBehaviour
{
    [SerializeField] private int points = 1000; // Puntos a añadir cuando este enemigo muere
    [SerializeField] private GameObject explosionPrefab; // Prefab de explosión a instanciar
    [SerializeField] private GameObject spawnPrefab; // Prefab a spawnear con probabilidad

    [SerializeField, Range(0f, 1f)] private float spawnProbability = 0.1f; // Probabilidad de spawn (10%)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            HandleBulletCollision(other);
        }
    }

    private void HandleBulletCollision(Collider2D bullet)
    {
        // Sumar puntos si existe un ScoreManager en la escena
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddPoints(points);
        }

        // Destruir la bala
        Destroy(bullet.gameObject);

        // Crear explosión y destruir este enemigo
        CreateExplosion();

        // Intentar spawnear prefab con probabilidad
        TrySpawnPrefab();

        Destroy(gameObject);
    }

    private void CreateExplosion()
    {
        if (explosionPrefab != null) // Validar que el prefab esté asignado
        {
            GameObject explosionInstance = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosionInstance, 1f); // Destruir la explosión tras 1 segundo
        }
        else
        {
            Debug.LogWarning("Explosion prefab is not assigned in the inspector.");
        }
    }

    private void TrySpawnPrefab()
    {
        if (spawnPrefab != null)
        {
            float roll = Random.Range(0f, 1f);
            if (roll <= spawnProbability)
            {
                Instantiate(spawnPrefab, transform.position, Quaternion.identity);
            }
        }
        else
        {
            Debug.LogWarning("Spawn prefab is not assigned in the inspector.");
        }
    }
}

