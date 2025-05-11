using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScore1 : MonoBehaviour
{
    [SerializeField] private int points = 1000; // Punts a afegir quan aquest enemic mor

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            // Suma punts si existeix un ScoreManager a escena
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddPoints(points);
            }

            // Destrueix la bala
            Destroy(other.gameObject);

            // Destrueix aquest enemic
            Destroy(gameObject);
        }
    }
}


