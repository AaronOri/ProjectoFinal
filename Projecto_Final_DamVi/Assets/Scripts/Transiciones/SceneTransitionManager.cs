using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public Image fadeImage; // Imagen UI que se usará para el fade
    public float fadeDuration = 1f; // Duración del fade

    public static SceneTransitionManager Instance; // Instancia singleton

    void Awake()
    {
        // Singleton para mantenerlo entre escenas
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Esto hace que el SceneTransitionManager persista entre escenas
        }
        else
        {
            Destroy(gameObject); // Si hay otro SceneTransitionManager, lo destruimos
        }
    }

    void Start()
    {
        // Inicia con el fadeFromBlack al cargar la primera escena
        StartCoroutine(FadeFromBlack());
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    IEnumerator FadeAndLoadScene(string sceneName)
    {
        yield return StartCoroutine(FadeToBlack()); // Fade out
        SceneManager.LoadScene(sceneName); // Cargar la nueva escena
        yield return null; // Espera un frame para que la nueva escena cargue
        yield return StartCoroutine(FadeFromBlack()); // Fade in
    }

     public IEnumerator FadeToBlack()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(Mathf.Lerp(0f, 1f, t / fadeDuration)); // Fade out
            yield return null;
        }
        SetAlpha(1f); // Asegura que esté completamente negro al final
    }

    IEnumerator FadeFromBlack()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(Mathf.Lerp(1f, 0f, t / fadeDuration)); // Fade in
            yield return null;
        }
        SetAlpha(0f); // Asegura que se haya vuelto transparente al final
    }

    void SetAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = alpha; // Cambia la opacidad de la imagen
            fadeImage.color = color;
        }
    }
}