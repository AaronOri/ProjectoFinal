using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ConditionFinalLevel : MonoBehaviour
{
    [SerializeField]
    private string noLivesSceneName; // Escena para cuando no quedan vidas

    [SerializeField]
    private PlayerHealth playerHealth; // Referencia al script que maneja las vidas

    [SerializeField]
    private GameObject objectToWatch; // Objeto a monitorear para ver si fue destruido

    [SerializeField]
    private string objectDestroyedSceneName; // Nombre de la escena para objeto destruido

    [SerializeField]
    private Image fadeOverlayImage; // Imagen transparente que se oscurecerá al máximo

    [SerializeField]
    private float fadeDuration = 1.5f; // Duración del oscurecimiento en segundos

    private bool sceneLoaded = false;

    void Start()
    {
        if (playerHealth == null)
        {
            Debug.LogWarning("PlayerHealth no está asignado en ConditionFinalLevel.");
        }

        if (objectToWatch == null)
        {
            Debug.LogWarning("objectToWatch no está asignado en ConditionFinalLevel.");
        }

        if (fadeOverlayImage == null)
        {
            Debug.LogWarning("fadeOverlayImage no está asignada en ConditionFinalLevel.");
        }
        else
        {
            // Asegurarse que la imagen empiece completamente transparente
            Color c = fadeOverlayImage.color;
            c.a = 0f;
            fadeOverlayImage.color = c;
            fadeOverlayImage.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (sceneLoaded) return;

        // Verificar vidas si playerHealth está asignado
        if (playerHealth != null)
        {
            int currentLives = playerHealth.GetCurrentLives();
            if (currentLives <= 0)
            {
                StartCoroutine(FadeAndLoadScene(noLivesSceneName));
                return;
            }
        }

        // Verificar si el objeto monitoreado fue destruido
        if (objectToWatch == null)
        {
            StartCoroutine(FadeAndLoadScene(objectDestroyedSceneName));
            return;
        }
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        if (sceneLoaded) yield break;

        sceneLoaded = true;

        if (fadeOverlayImage == null)
        {
            Debug.LogError("No se puede oscurecer la pantalla porque fadeOverlayImage no está asignada.");
            // Cargar la escena inmediatamente si no hay imagen para oscurecer
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError("El nombre de la escena está vacío o es nulo.");
            }
            yield break;
        }

        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("El nombre de la escena está vacío o es nulo.");
            yield break;
        }

        // Oscurecer la imagen aumentando su alfa de 0 a 1 en fadeDuration segundos
        float elapsed = 0f;
        Color c = fadeOverlayImage.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / fadeDuration);
            fadeOverlayImage.color = c;
            yield return null;
        }
        c.a = 1f;
        fadeOverlayImage.color = c;

        // Cargar la escena luego de completar el oscurecimiento
        SceneManager.LoadScene(sceneName);
    }
}

