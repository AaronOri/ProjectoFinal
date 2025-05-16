using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxVides = 3;
    [SerializeField] private GameObject[] vidaIcons;
    [SerializeField] private float invincibilityDuration = 5f;
    [SerializeField] public bool isInvulnerable = false;

    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private SpriteRenderer spriteRenderer; // Asignar en Inspector

    private int videsActuals;
    private float invincibilityTimer = 0f;

    private float parpadeoTimer = 0f;
    private float parpadeoInterval = 0.1f;

    void Start()
    {
        videsActuals = maxVides;
        ActualitzarHUD();
    }

    void Update()
    {
        if (isInvulnerable)
        {
            invincibilityTimer -= Time.deltaTime;

            // Manejo del parpadeo
            parpadeoTimer -= Time.deltaTime;
            if (parpadeoTimer <= 0f)
            {
                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = !spriteRenderer.enabled;
                }
                parpadeoTimer = parpadeoInterval;
            }

            if (invincibilityTimer <= 0f)
            {
                EndInvincibility();
            }
        }
    }

    public void RebDany()
    {
        if (isInvulnerable) return;

        videsActuals--;

        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        if (videsActuals <= 0)
        {
            videsActuals = 0;
            ActualitzarHUD();
            Morir();
        }
        else
        {
            ActualitzarHUD();
            ActivateInvincibility(invincibilityDuration);
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
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        Debug.Log("Jugador ha mort!");
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
        parpadeoTimer = 0f;

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }
    }

    public void EndInvincibility()
    {
        isInvulnerable = false;

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true; // Asegura que quede visible
        }
    }
}
