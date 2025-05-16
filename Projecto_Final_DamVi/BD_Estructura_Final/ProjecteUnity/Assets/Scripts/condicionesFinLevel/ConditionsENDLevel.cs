using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Dialog_TimerToSceneWithFade : MonoBehaviour
{
    [SerializeField]
    public float countdownTime = 30f; // Tiempo para la cuenta regresiva

    [SerializeField]
    public string nextSceneName; // Escena tras temporizador 

    [SerializeField]
    public string noLivesSceneName; // Escena para cuando no quedan vidas 

    [SerializeField]
    public PlayerHealth playerHealth; // Referencia directa al script que maneja las vidas

    [Header("Fade UI")]
    [Tooltip("Imagen UI transparente que se oscurecerá al cargar la siguiente escena")]
    public Image fadeImage; // Imagen para el fade (debe estar inicialmente transparente)

    [Tooltip("Duración del efecto de fade")]
    public float fadeDuration = 1f;

    private float countdownTimer;
    private bool sceneLoaded = false;

    void Start()
    {
        countdownTimer = countdownTime;

        if (playerHealth == null)
        {
            Debug.LogWarning("PlayerHealth no está asignado en Dialog_TimerToSceneWithFade.");
        }

        // Inicializar imagen de fade transparente
        if (fadeImage != null)
        {
            SetAlpha(0f);
        }
        else
        {
            Debug.LogWarning("fadeImage no está asignada en Dialog_TimerToSceneWithFade.");
        }
    }

    void Update()
    {
        if (sceneLoaded) return;

        countdownTimer -= Time.deltaTime;

        // Verificar vidas si playerHealth está asignado
        if (playerHealth != null)
        {
            int currentLives = playerHealth.GetCurrentLives();
            if (currentLives <= 0)
            {
                sceneLoaded = true;
                StartCoroutine(FadeAndLoadScene(noLivesSceneName));
                return; // Evitar seguir con countdown
            }
        }

        if (countdownTimer <= 0f)
        {
            sceneLoaded = true;
            StartCoroutine(FadeAndLoadScene(nextSceneName));
        }
    }

    IEnumerator FadeAndLoadScene(string sceneName)
    {
        yield return StartCoroutine(FadeToBlack());

        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Nombre de escena vacío. No se puede cargar la escena.");
        }
    }

    IEnumerator FadeToBlack()
    {
        if (fadeImage == null)
            yield break;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(Mathf.Lerp(0f, 1f, t / fadeDuration));
            yield return null;
        }
        SetAlpha(1f);
    }

    void SetAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = alpha;
            fadeImage.color = c;
        }
    }
}

