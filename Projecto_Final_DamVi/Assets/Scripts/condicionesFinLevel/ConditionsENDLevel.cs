using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialog_TimerToScene : MonoBehaviour
{
    [Tooltip("Tiempo en segundos tras el cual se cargará la escena automáticamente.")]
    public float countdownTime = 30f; // Tiempo para la cuenta regresiva

    [Tooltip("Nombre de la escena a cargar después de que termine el temporizador.")]
    public string nextSceneName; // Escena tras temporizador (opcional)

    [Tooltip("Nombre de la escena a cargar si se acaban las vidas.")]
    public string noLivesSceneName; // Escena para cuando no quedan vidas (opcional)

    [Tooltip("Referencia al script PlayerHealth para obtener las vidas.")]
    public PlayerHealth playerHealth; // Referencia directa al script que maneja las vidas

    private float countdownTimer;
    private bool sceneLoaded = false;

    void Start()
    {
        countdownTimer = countdownTime;

        if (playerHealth == null)
        {
            Debug.LogWarning("PlayerHealth no está asignado en Dialog_TimerToScene.");
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
                LoadSceneNoLives();
                return; // Evitar seguir con countdown
            }
        }

        if (countdownTimer <= 0f)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        if (sceneLoaded) return;

        sceneLoaded = true;

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("No se cargará la siguiente escena porque nextSceneName no está especificada.");
        }
    }

    private void LoadSceneNoLives()
    {
        if (sceneLoaded) return;

        sceneLoaded = true;

        if (!string.IsNullOrEmpty(noLivesSceneName))
        {
            SceneManager.LoadScene(noLivesSceneName);
        }
        else
        {
            Debug.Log("No se cargará la escena de 'sin vidas' porque noLivesSceneName no está especificada.");
        }
    }
}
