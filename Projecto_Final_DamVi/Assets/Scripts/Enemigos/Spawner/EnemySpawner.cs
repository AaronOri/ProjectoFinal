using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Assign enemy prefabs to spawn")]
    public GameObject[] enemyPrefabs;

    [Tooltip("Time in seconds between spawns")]
    public float spawnInterval = 2f;

    private BoxCollider2D spawnZoneCollider;
    private float timer;

    void Start()
    {
        // Get the BoxCollider2D component attached to this GameObject
        spawnZoneCollider = GetComponent<BoxCollider2D>();

        if (spawnZoneCollider == null)
        {
            Debug.LogError("EnemySpawner requires a BoxCollider2D component on the same GameObject.");
        }

        timer = spawnInterval;
    }

    void Update()
    {
        if (enemyPrefabs.Length == 0 || spawnZoneCollider == null)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnInterval; // Reset timer
        }
    }

    void SpawnEnemy()
    {
        // Choose a random enemy prefab
        int index = Random.Range(0, enemyPrefabs.Length);

        // Get random position inside the box collider bounds
        Vector2 spawnPosition = GetRandomPointInBounds(spawnZoneCollider.bounds);

        Instantiate(enemyPrefabs[index], spawnPosition, Quaternion.identity);
    }

    Vector2 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }
}
