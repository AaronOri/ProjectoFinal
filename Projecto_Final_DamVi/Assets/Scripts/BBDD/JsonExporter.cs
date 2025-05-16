using UnityEngine;
using System.IO;

public class JsonExporter : MonoBehaviour
{
    [System.Serializable]
    public class Jugador
    {
        public string username;
        public int score;
        public float timeSeconds;
        public int shotsFired;
        public int wins;
        public int losses;
    }

    public string username = "PlayerOne";

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ExportarJugador()
    {
        Jugador j = new Jugador
        {
            username = username,
            score = ScoreManager.Instance != null ? ScoreManager.Instance.GetCurrentScore() : 0,
            timeSeconds = ScoreManager.Instance != null ? ScoreManager.Instance.GetElapsedTime() : 0f,
            shotsFired = GameStats.TotalShotsFired,
            wins = GameStats.TotalWins,
            losses = GameStats.TotalLosses
        };

        string json = JsonUtility.ToJson(j, true);
        string path = Path.Combine(Application.dataPath, "../Java_Gestio_BDD/jugador.json");

        File.WriteAllText(path, json);
        Debug.Log("✅ Fitxer jugador.json exportat: " + path);
    }
}

