using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxVides = 3; // Máximo de vidas
    [SerializeField] private GameObject[] vidaIcons; // Iconos de vida en el HUD
    [SerializeField] private float invincibilityDuration = 5f; // Duración de la invulnerabilidad
    [SerializeField] public bool isInvulnerable = false; // Estado de invulnerabilidad

    private int videsActuals;
    private float invincibilityTimer = 0f;

    void Start()
    {
        videsActuals = maxVides;
        ActualitzarHUD();
    }

    void Update()
    {
        // Manejo del temporizador de invulnerabilidad
        if (isInvulnerable)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f)
            {
                EndInvincibility();
            }
        }
    }

    public void RebDany()
    {
        if (isInvulnerable) return; // Ignorar daño si es invulnerable

        videsActuals--;

        if (videsActuals <= 0)
        {
            videsActuals = 0;
            ActualitzarHUD();
            Morir();
        }
        else
        {
            ActualitzarHUD();
        }
    }

    private void ActualitzarHUD()
    {
        for (int i = 0; i < vidaIcons.Length; i++)
        {
            vidaIcons[i].SetActive(i < videsActuals);
        }
    }

    private void Morir()
    {
        Debug.Log("Jugador ha mort!");
        // Aquí puedes añadir explosión, desactivar jugador, pantalla de Game Over, etc.
        gameObject.SetActive(false);
    }

    public int GetCurrentLives()
    {
        return videsActuals;
    }

    public void ActivateInvincibility(float duration)
    {
        isInvulnerable = true;
        invincibilityTimer = duration;
    }

    public void EndInvincibility()
    {
        isInvulnerable = false;
    }
}
