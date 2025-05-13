using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    [Tooltip("Nombre de la imagen UI que se usará para el fade (buscada por nombre en escena)")]
    public string fadeImageName = "FadeImage"; // Nombre del objeto Image a buscar

    public float fadeDuration = 1f; // Duración del fade

    public static SceneTransitionManager Instance; // Instancia singleton

    private Image fadeImage; // Imagen UI usada para el fade, se obtiene buscando por nombre

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Buscar todas las imágenes en la escena
        Image[] allImages = Object.FindObjectsOfType<Image>();

        // Buscar la que tenga el nombre fadeImageName
        foreach (Image img in allImages)
        {
            if (img.gameObject.name == fadeImageName)
            {
                fadeImage = img;
                break;
            }
        }

        if (fadeImage == null)
        {
            Debug.LogWarning($"No se encontró ninguna imagen UI llamada '{fadeImageName}' en la escena.");
        }
    }

    void Start()
    {
        StartCoroutine(FadeFromBlack());
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    IEnumerator FadeAndLoadScene(string sceneName)
    {
        yield return StartCoroutine(FadeToBlack());
        SceneManager.LoadScene(sceneName);
        yield return null;
        yield return StartCoroutine(FadeFromBlack());
    }

    public IEnumerator FadeToBlack()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(Mathf.Lerp(0f, 1f, t / fadeDuration));
            yield return null;
        }
        SetAlpha(1f);
    }

    IEnumerator FadeFromBlack()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(Mathf.Lerp(1f, 0f, t / fadeDuration));
            yield return null;
        }
        SetAlpha(0f);
    }

    void SetAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
        }
    }
}
