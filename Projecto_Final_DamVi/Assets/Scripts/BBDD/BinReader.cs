using UnityEngine;
using System.IO;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

public class BinReader : MonoBehaviour
{
    void Awake()
    {
        if (FindObjectsOfType<BinReader>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void LlegirBson()
    {
        string path = Path.Combine(Application.dataPath, "../Java_Gestio_BDD/Jugadors.bson");

        if (!File.Exists(path))
        {
            Debug.LogWarning("No s'ha trobat Jugadors.bson");
            return;
        }

        byte[] data = File.ReadAllBytes(path);
        using var ms = new MemoryStream(data);
        var reader = new BsonBinaryReader(ms);
        var context = BsonDeserializationContext.CreateRoot(reader);
        var doc = BsonDocumentSerializer.Instance.Deserialize(context);

        string username = doc["username"].AsString;
        int score = doc["score"].AsInt32;

        Debug.Log($"Rànquing jugador: {username} - Puntuació: {score}");
    }
}


