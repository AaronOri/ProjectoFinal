using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFadeIn : MonoBehaviour
{
    public Image fadeImage; // Imagen UI que se usará para el fade
    public float fadeDuration = 1f; // Duración del fade

    void Start()
    {
        // Asegúrate de que la imagen de fade sea completamente opaca (negra) al inicio
        SetAlpha(1f);

        // Iniciar el fade in después de cargar la escena
        StartCoroutine(FadeInAfterSceneLoad());
    }

    // Método para hacer el fade in después de cargar la escena
    IEnumerator FadeInAfterSceneLoad()
    {
        // Esperar hasta que la escena esté completamente cargada
        yield return new WaitForSeconds(0.1f); // Un pequeño retraso por si es necesario

        // Realizar el fade in (de negro a transparente)
        yield return StartCoroutine(FadeFromBlack());
    }

    // Corutina para hacer el fade in (de negro a transparente)
    IEnumerator FadeFromBlack()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(Mathf.Lerp(1f, 0f, t / fadeDuration)); // Fade in (de negro a transparente)
            yield return null;
        }
        SetAlpha(0f); // Asegura que se haya vuelto completamente transparente al final
    }

    // Método para cambiar el alfa de la imagen de fade
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
