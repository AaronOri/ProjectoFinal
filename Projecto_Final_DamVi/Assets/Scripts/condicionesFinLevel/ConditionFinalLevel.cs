using UnityEngine;
using UnityEngine.SceneManagement;

public class ConditionFinalLevel : MonoBehaviour
{
    [SerializeField]
    public string noLivesSceneName; // Escena para cuando no quedan vidas

    [SerializeField]
    public PlayerHealth playerHealth; // Referencia directa al script que maneja las vidas

    [SerializeField]
    public GameObject objectToWatch;

    [SerializeField]
    public string objectDestroyedSceneName;

    private bool sceneLoaded = false;

    void Start()
    {
        if (playerHealth == null)
        {
            Debug.LogWarning("PlayerHealth no está asignado en ConditionInfinitLevel.");
        }

        if (objectToWatch == null)
        {
            Debug.LogWarning("objectToWatch no está asignado en ConditionInfinitLevel.");
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
                return;
            }
        }

        // Verificar si el objeto monitoreado fue destruido
        if (objectToWatch == null)
        {
            LoadSceneObjectDestroyed();
            return;
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

    private void LoadSceneObjectDestroyed()
    {
        if (sceneLoaded) return;

        sceneLoaded = true;

        if (!string.IsNullOrEmpty(objectDestroyedSceneName))
        {
            SceneManager.LoadScene(objectDestroyedSceneName);
        }
        else
        {
            Debug.Log("No se cargará la escena para objeto destruido porque objectDestroyedSceneName no está especificada.");
        }
    }
}
