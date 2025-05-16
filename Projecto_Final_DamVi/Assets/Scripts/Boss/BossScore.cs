using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScore : MonoBehaviour
{
    [SerializeField] private int points = 30000;
    private bool scoreGiven = false;

    public void GiveScore()
    {
        if (!scoreGiven)
        {
            ScoreManager.Instance?.AddPoints(points);
            scoreGiven = true;
        }
    }
}

