using UnityEngine;

public class EnemyScore3 : MonoBehaviour
{
    [SerializeField] private int points = 3000; // Points to add when this enemy dies
    [SerializeField] private GameObject explosionPrefab; // Explosion prefab to instantiate

    [SerializeField] private GameObject[] spawnPrefabs; // Array of prefabs to spawn with probability
    [SerializeField, Range(0f, 1f)] private float spawnProbability = 0.2f; // Spawn probability (20%)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            // Add points if a ScoreManager exists in the scene
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddPoints(points);
            }

            // Destroy the bullet
            Destroy(other.gameObject);

            // Create explosion and try to spawn a prefab
            CreateExplosion();
            TrySpawnPrefab();

            // Destroy this enemy
            Destroy(gameObject);
        }
    }

    private void CreateExplosion()
    {
        if (explosionPrefab != null) // Validate that the prefab is assigned
        {
            GameObject explosionInstance = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosionInstance, 1f); // Destroy the explosion after 1 second
        }
        else
        {
            Debug.LogWarning("Explosion prefab is not assigned in the inspector.");
        }
    }

    private void TrySpawnPrefab()
    {
        if (spawnPrefabs != null && spawnPrefabs.Length > 0)
        {
            float roll = Random.Range(0f, 1f);
            if (roll <= spawnProbability)
            {
                // Pick one prefab randomly from the array
                int index = Random.Range(0, spawnPrefabs.Length);
                GameObject chosenPrefab = spawnPrefabs[index];

                if (chosenPrefab != null)
                {
                    // Instantiate the prefab as a child of the main camera
                    GameObject spawnedPrefab = Instantiate(chosenPrefab, Camera.main.transform);
                    spawnedPrefab.transform.position = transform.position; // Set position to the enemy's position
                }
                else
                {
                    Debug.LogWarning($"Spawn prefab at index {index} is not assigned.");
                }
            }
        }
        else
        {
            Debug.LogWarning("Spawn prefabs array is empty or not assigned in the inspector.");
        }
    }
}
