using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance { get; private set; }

    // Referencia al texto del marcador
    private TextMeshProUGUI scoreText;

    // Referencia al texto del tiempo
    private TextMeshProUGUI timeText;

    // Puntuación actual
    private int currentScore = 0;

    // Nombre de la escena para resetear la puntuación
    [SerializeField]
    private string sceneToResetScore = "";

    // Nombre de la escena para iniciar el contador
    [SerializeField]
    private string sceneToStartCounter1 = "";

    [SerializeField]
    private string sceneToStartCounter2 = "";


    // Nombre de la escena para detener el contador
    [SerializeField]
    private string sceneToStopCounter = "";

    // Nombre de la escena para imprimir el tiempo
    [SerializeField]
    private string sceneToPrintTime = "";

    // Nombre del texto del tiempo
    [SerializeField]
    private string timeTextObjectName = "TiempoTot";

    // Tiempo inicial
    private float startTime = 0f;

    // Tiempo final
    private float endTime = 0f;

    // Tiempo transcurrido
    private float elapsedTime = 0f;

    // Nombre de la escena para resetear el contador
    [SerializeField]
    private string sceneToResetCounter = "";

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

        // Buscar el texto en la escena actual
        FindScoreText();
        FindTimeText();
    }

    private void OnDestroy()
    {
        // Desuscribir para evitar fugas de memoria
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindScoreText();
        UpdateScoreText();
        FindTimeText();

        // Iniciar el contador si la escena es la especificada
        if (scene.name == sceneToStartCounter1 || scene.name == sceneToStartCounter2)
        {
            startTime = Time.time;
        }

        // Detener el contador si la escena es la especificada
        if (scene.name == sceneToStopCounter)
        {
            endTime = Time.time;
            elapsedTime = endTime - startTime;
        }

        // Imprimir el tiempo si la escena es la especificada
        if (scene.name == sceneToPrintTime)
        {
            PrintTime();
        }

        // Resetea la puntuación si la escena cargada es la especificada
        if (!string.IsNullOrEmpty(sceneToResetScore) && scene.name == sceneToResetScore)
        {
            ResetScore();
        }

        // Resetea el contador si la escena cargada es la especificada
        if (!string.IsNullOrEmpty(sceneToResetCounter) && scene.name == sceneToResetCounter)
        {
            ResetCounter();
        }
    }

    private void FindScoreText()
    {
        // Buscar el texto en la escena actual
        GameObject scoreTextObject = GameObject.Find("PuntosTot");
        if (scoreTextObject != null)
        {
            scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
        }
    }

    private void FindTimeText()
    {
        // Buscar el texto del tiempo en la escena actual
        GameObject timeTextObject = GameObject.Find(timeTextObjectName);
        if (timeTextObject != null)
        {
            timeText = timeTextObject.GetComponent<TextMeshProUGUI>();
        }
    }

    public void AddPoints(int points)
    {
        // Agregar puntos a la puntuación actual
        currentScore += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        // Actualizar el texto de la puntuación
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString("D7"); // Ejemplo: 0000100
        }
    }

    public void ResetScore()
    {
        // Resetear la puntuación
        currentScore = 0;
        UpdateScoreText();
    }

    private void PrintTime()
    {
        // Imprimir el tiempo en el texto
        if (timeText != null)
        {
            int hours = Mathf.FloorToInt(elapsedTime / 3600);
            int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timeText.text = hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2");
        }
    }

    private void ResetCounter()
    {
        // Resetear el contador
        startTime = 0f;
        endTime = 0f;
        elapsedTime = 0f;
    }
}