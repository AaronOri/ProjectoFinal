using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialog_TimerToScene : MonoBehaviour
{
    [SerializeField]
    public float countdownTime = 30f; // Tiempo para la cuenta regresiva

    [SerializeField]
    public string nextSceneName; // Escena tras temporizador 

    [SerializeField]
    public string noLivesSceneName; // Escena para cuando no quedan vidas 

    [SerializeField]
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
        
    }

    private void LoadSceneNoLives()
    {
        if (sceneLoaded) return;

        sceneLoaded = true;

        if (!string.IsNullOrEmpty(noLivesSceneName))
        {
            SceneManager.LoadScene(noLivesSceneName);
        }
       
    }
}
