using UnityEngine;
using System.IO;
using MongoDB.Bson;
using MongoDB.Bson.IO;

public class BinReader : MonoBehaviour
{
    void Awake()
    {
    DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        string path = Path.Combine(Application.dataPath, "../Java_Gestio_BDD/Jugadors.bson");
        if (!File.Exists(path))
        {
            Debug.LogWarning("⚠ No s'ha trobat el fitxer Jugadors.bson.");
            return;
        }

        byte[] bsonData = File.ReadAllBytes(path);
        BsonDocument doc = BsonDocument.ReadFrom(new BsonBinaryReader(new MemoryStream(bsonData)));

        string user = doc.GetValue("username").AsString;
        int score = doc.GetValue("score").AsInt32;

        Debug.Log("🏅 Últim jugador: " + user + " | Score: " + score);
    }
}

