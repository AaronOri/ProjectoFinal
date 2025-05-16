using UnityEngine;
using System.Collections;

public class EnemyScore1 : MonoBehaviour
{
    [SerializeField] private int points = 1000; // Points to add when this enemy dies
    [SerializeField] private GameObject explosionPrefab; // Explosion prefab to instantiate

    [SerializeField] private GameObject[] spawnPrefabs; // Array of prefabs to spawn with probability

    [SerializeField, Range(0f, 1f)] private float spawnProbability = 0.2f; // Spawn probability (20%)

    [SerializeField] private AudioClip deathSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            HandleBulletCollision(other);
        }
    }

    private void HandleBulletCollision(Collider2D bullet)
    {
        // Add points if a ScoreManager exists in the scene
        ScoreManager.Instance?.AddPoints(points);

        // Destroy the bullet
        Destroy(bullet.gameObject);

        // Create explosion and destroy this enemy
        CreateExplosion();

        // Try to spawn a random prefab from the array with probability
        TrySpawnPrefab();

        // Use coroutine to delay enemy destruction for effects
        StartCoroutine(DestroyEnemy());
    }

    private void CreateExplosion()
    {
        if (explosionPrefab != null)
        {
            GameObject explosionInstance = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosionInstance, 1f);
        }

        // Reproducir sonido de destrucción
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
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
                    Instantiate(chosenPrefab, transform.position, Quaternion.identity);
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

    private IEnumerator DestroyEnemy()
    {
        // Wait for a short duration to allow effects to play
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

}

