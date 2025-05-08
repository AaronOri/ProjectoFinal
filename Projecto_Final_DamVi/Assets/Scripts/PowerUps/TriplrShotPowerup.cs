using UnityEngine;

public class TripleShotPowerUp : MonoBehaviour
{
    public float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShooting shooting = other.GetComponent<PlayerShooting>();
            if (shooting != null)
            {
                shooting.ActivateTripleShot(duration);
            }

            Destroy(gameObject); // Elimina el power-up un cop recollit
        }
    }
}
