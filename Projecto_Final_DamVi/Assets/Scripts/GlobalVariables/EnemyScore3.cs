using UnityEngine;
using TMPro;

public class EnemyScore3 : MonoBehaviour
{
    
    [SerializeField] private int points = 1000; // Puntos a añadir cuando este enemigo muere

    [SerializeField] private GameObject explosionPrefab; // Prefab de explosión a instanciar

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            // Sumar puntos si existe un ScoreManager en la escena
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddPoints(points);
            }

            // Destruir la bala
            Destroy(other.gameObject);

            // Crear explosión y destruir este enemigo
            CreateExplosion();
            Destroy(gameObject);
        }
    }

    private void CreateExplosion()
    {
    
        GameObject explosionInstance = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosionInstance, 1f); // Destruir la explosión tras 1 segundo

    }
}
