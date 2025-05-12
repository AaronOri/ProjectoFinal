using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Asigna los prefabs de enemigos a generar")]
    public GameObject[] enemyPrefabs;

    [Tooltip("Tiempo en segundos entre cada generación")]
    public float spawnInterval = 2f;

    [Tooltip("Colisionador de la zona de generación")]
    public BoxCollider2D spawnZoneCollider;

    private float timer;

    void Start()
    {
        // Verifica si el colisionador está asignado
        if (spawnZoneCollider == null)
        {
            Debug.LogError("EnemySpawner requiere un componente BoxCollider2D en el mismo GameObject.");
            return;
        }

        // Verifica si hay prefabs de enemigos asignados
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogError("No se han asignado prefabs de enemigos.");
            return;
        }

        // Inicia la corrutina para generar enemigos
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        // Bucle infinito para generar enemigos en intervalos
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval); // Espera el intervalo de generación
            SpawnEnemy(); // Genera un enemigo
        }
    }

    void SpawnEnemy()
    {
        // Elige un prefab de enemigo aleatorio
        int index = Random.Range(0, enemyPrefabs.Length);

        // Obtiene una posición aleatoria dentro de los límites del colisionador
        Vector2 spawnPosition = GetRandomPointInBounds(spawnZoneCollider.bounds);

        // Instancia el enemigo y establece su padre al GameObject que tiene este script
        GameObject enemy = Instantiate(enemyPrefabs[index], spawnPosition, Quaternion.identity);
        enemy.transform.SetParent(transform); // Establece el padre al spawner
    }

    Vector2 GetRandomPointInBounds(Bounds bounds)
    {
        // Genera un punto aleatorio dentro de los límites, evitando los bordes
        float x = Random.Range(bounds.min.x + 0.1f, bounds.max.x - 0.1f);
        float y = Random.Range(bounds.min.y + 0.1f, bounds.max.y - 0.1f);
        return new Vector2(x, y);
    }
}
