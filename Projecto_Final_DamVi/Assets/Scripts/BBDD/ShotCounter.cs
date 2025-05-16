using UnityEngine;

public class ShotCounter : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameStats.RegisterShot();
            // Debug opcional:
            // Debug.Log("Dispar registrat. Total: " + GameStats.TotalShotsFired);
        }
    }
}

