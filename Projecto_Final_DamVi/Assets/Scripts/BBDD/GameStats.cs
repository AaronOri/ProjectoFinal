using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static int TotalShotsFired = 0;
    public static int TotalWins = 0;
    public static int TotalLosses = 0;

    private void Awake()
    {
        if (FindObjectsOfType<GameStats>().Length > 1)
        {
            Destroy(gameObject); // Evita duplicats
            return;
        }

        DontDestroyOnLoad(gameObject); // Manté entre escenes
    }

    public static void RegisterShot()
    {
        TotalShotsFired++;
    }

    public static void RegisterWin()
    {
        TotalWins++;
    }

    public static void RegisterLoss()
    {
        TotalLosses++;
    }

    public static void ResetStats()
    {
        TotalShotsFired = 0;
        TotalWins = 0;
        TotalLosses = 0;
    }
}

