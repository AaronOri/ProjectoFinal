using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private TextMeshProUGUI scoreText; // Referencia al texto del marcador
    private int currentScore = 0;

    private string scoreTextObjectName = "PuntosTot"; 

    [SerializeField]
    public PlayerHealth playerHealth;

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
        FindPlayerHealth();
    }

    private void FindScoreText()
    {
        GameObject scoreTextObject = GameObject.Find(scoreTextObjectName);
        if (scoreTextObject != null)
        {
            scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
            
        }
    }

    private void FindPlayerHealth()
    {
        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
            
        }
    }

    private void Update()
    {
        if (playerHealth != null)
        {
            if (playerHealth.GetCurrentLives() <= 0)
            {
                ResetScore();
            }
        }
    }

    public void AddPoints(int points)
    {
        currentScore += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString("D7"); // Ejemplo: 0000100
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreText();
    }
}
