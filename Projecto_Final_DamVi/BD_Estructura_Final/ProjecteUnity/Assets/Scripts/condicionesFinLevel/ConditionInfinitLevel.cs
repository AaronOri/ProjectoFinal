using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ConditionInfinitLevel : MonoBehaviour
{
    [SerializeField]
    public string noLivesSceneName; // Escena para cuando no quedan vidas

    [SerializeField]
    public PlayerHealth playerHealth; // Referencia directa al script que maneja las vidas

 
    public Image fadeImage; // Imagen para el fade (debe estar inicialmente transparente)

    public float fadeDuration = 1f;

    private bool sceneLoaded = false;

    void Start()
    {
        if (playerHealth == null)
        {
            Debug.LogWarning("PlayerHealth no está asignado en ConditionInfinitLevel.");
        }

        if (fadeImage != null)
        {
            SetAlpha(0f); // Inicializa transparente
        }
        else
        {
            Debug.LogWarning("fadeImage no está asignada en ConditionInfinitLevel.");
        }
    }

    void Update()
    {
        if (sceneLoaded) return;

        if (playerHealth != null)
        {
            int currentLives = playerHealth.GetCurrentLives();
            if (currentLives <= 0)
            {
                sceneLoaded = true;
                StartCoroutine(FadeAndLoadScene(noLivesSceneName));
            }
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
            Debug.LogWarning("noLivesSceneName no está especificada. No se puede cargar la escena.");
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
