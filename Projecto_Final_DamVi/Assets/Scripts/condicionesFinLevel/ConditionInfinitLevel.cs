using UnityEngine;
using UnityEngine.SceneManagement;

public class ConditionInfinitLevel : MonoBehaviour
{
    [SerializeField]
    public string noLivesSceneName; // Escena para cuando no quedan vidas

    [SerializeField]
    public PlayerHealth playerHealth; // Referencia directa al script que maneja las vidas

    private bool sceneLoaded = false;

    void Start()
    {
        if (playerHealth == null)
        {
            Debug.LogWarning("PlayerHealth no está asignado en ConditionInfinitLevel.");
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
                LoadSceneNoLives();
            }
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

