using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private TextMeshPro scoreText; // Texto del marcador
    private int currentScore = 0;

    private string scoreTextObjectName = "PuntosTot"; // Nombre del objeto texto puntuación
    private string timerTextObjectName = "TiempoTot"; // Nombre del objeto texto temporizador

    [Tooltip("Nombre de la escena donde se debe resetear la puntuación.")]
    [SerializeField]
    private string sceneToResetScore = "";

    [Tooltip("Nombre de la escena donde se debe resetear el temporizador.")]
    [SerializeField]
    private string sceneToResetTimer = "";

    [Tooltip("Nombres de escenas donde inicia el temporizador.")]
    [SerializeField]
    private string[] timerStartScenes = new string[2] { "StartScene1", "StartScene2" };

    private TextMeshProUGUI timerText; // Texto del temporizador

    private bool timerRunning = false;
    private bool scoreRunning = true;
    private float elapsedTime = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        FindScoreText();
        timerText = FindTimerText();
        if (timerText != null)
            timerText.text = "00:00:00";
    }

    private void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindScoreText();
        UpdateScoreText();

        timerText = FindTimerText();
        if (timerText != null && !timerRunning)
            UpdateTimerText();

        if (scene.name == "Win_Scene")
        {
            StopTimer();
            scoreRunning = false;
        }
        else
        {
            scoreRunning = true;

            if (System.Array.Exists(timerStartScenes, s => s == scene.name))
                StartTimer();

            if (!string.IsNullOrEmpty(sceneToResetTimer) && scene.name == sceneToResetTimer)
                ResetTimer();
        }

        if (!string.IsNullOrEmpty(sceneToResetScore) && scene.name == sceneToResetScore)
            ResetScore();
    }

    private TextMeshProUGUI FindTimerText()
    {
        GameObject obj = GameObject.Find(timerTextObjectName);
        if (obj != null)
        {
            TextMeshProUGUI txt = obj.GetComponent<TextMeshProUGUI>();
            if (txt == null)
                Debug.LogWarning($"El objeto '{timerTextObjectName}' no tiene componente TextMeshProUGUI.");
            return txt;
        }
        Debug.LogWarning($"No se encontró el objeto de texto del temporizador: {timerTextObjectName}");
        return null;
    }

    private void FindScoreText()
    {
        GameObject obj = GameObject.Find(scoreTextObjectName);
        if (obj != null)
        {
            scoreText = obj.GetComponent<TextMeshPro>();
            if (scoreText == null)
                Debug.LogWarning($"El objeto '{scoreTextObjectName}' no tiene componente TextMeshProUGUI.");
        }
        else
        {
            Debug.LogWarning($"No se encontró el objeto de texto de puntuación: {scoreTextObjectName}");
        }
    }

    private void Update()
    {
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;
            if (timerText != null)
                UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        int h = Mathf.FloorToInt(elapsedTime / 3600);
        int m = Mathf.FloorToInt((elapsedTime % 3600) / 60);
        int s = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = $"{h:D2}:{m:D2}:{s:D2}";
    }

    private void StartTimer()
    {
        elapsedTime = 0f;
        timerRunning = true;
        if (timerText != null)
            UpdateTimerText();
    }

    private void StopTimer()
    {
        timerRunning = false;
    }

    private void ResetTimer()
    {
        elapsedTime = 0f;
        if (timerText != null)
            timerText.text = "00:00:00";
    }

    public void AddPoints(int points)
    {
        if (!scoreRunning) return;
        currentScore += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = currentScore.ToString("D7");
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreText();
    }
}
