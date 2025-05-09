using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    private int currentScore = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Opcional si vols mantenir-lo entre escenes
    }

    public void AddPoints(int points)
    {
        currentScore += points;
        scoreText.text = currentScore.ToString("D7"); // Ex: 0000100
    }
}


