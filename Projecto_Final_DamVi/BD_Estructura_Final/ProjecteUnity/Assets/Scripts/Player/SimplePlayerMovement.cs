using System.Collections;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    [SerializeField] float horizontalSpeed = 5f;
    [SerializeField] float verticalScrollSpeed = 2f;
    [SerializeField] float verticalControlAmount = 0.5f; // Control vertical
    [SerializeField] float leanAngle = 15f;

    public bool isInvincible = false;  // Para la invencibilidad
    private float invincibilityDuration = 7f;  // Duración de la invencibilidad
    private bool quickshotActive = false;  // Para el power-up de quickshot
    private float quickshotDuration = 5f;  // Duración de quickshot
    private bool tripleshotActive = false;  // Para el power-up de tripleshot

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMovement();
        HandleInvincibility();
        HandleQuickshot();
        HandleTripleshot();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical") * verticalControlAmount;
        Vector3 movement = new Vector3(moveX * horizontalSpeed, verticalScrollSpeed + moveY, 0f) * Time.deltaTime;
        transform.Translate(movement);

        // Inclinación de la nave (efecto estético)
        float targetZRotation = -moveX * leanAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, targetZRotation);
    }

    private void HandleInvincibility()
    {
        if (isInvincible)
        {
            // Hacer que la nave parpadee
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }
    }

    public void ActivateInvencibili()
    {
        if (!isInvincible)
        {
            isInvincible = true;
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        float timer = invincibilityDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null; // Esperar un frame
        }
        isInvincible = false;
        spriteRenderer.enabled = true;  // Asegurarse de que la nave sea visible
    }

    private void HandleQuickshot()
    {
        if (quickshotActive)
        {
            quickshotDuration -= Time.deltaTime;
            if (quickshotDuration <= 0)
            {
                quickshotActive = false;
            }
        }
    }

    private void HandleTripleshot()
    {
        // Puedes manejar la mecánica de tripleshot aquí o en el script de disparo
    }

    // Métodos para activar power-ups
    public void ActivateQuickshot()
    {
        quickshotActive = true;
        quickshotDuration = 5f;  // Reseteamos el tiempo de quickshot
    }

    public void ActivateTripleshot()
    {
        tripleshotActive = true;
    }
}
