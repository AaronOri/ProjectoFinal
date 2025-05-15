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

    public void ExportarJugador()
    {
        Jugador j = new Jugador
        {
            username = "PlayerOne",
            score = 4200,
            timeSeconds = 153.5f,
            shotsFired = 87,
            wins = 3,
            losses = 1
        };

        string json = JsonUtility.ToJson(j, true);
        string path = Path.Combine(Application.dataPath, "../Java_Gestio_BDD/jugador.json");

        File.WriteAllText(path, json);
        Debug.Log("✅ Fitxer jugador.json exportat: " + path);
    }
}
