using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float minX = -6f;
    [SerializeField] private float maxX = 6f;
    [SerializeField] private float phase2SpeedMultiplier = 1.6f;  // << editable

    private bool movingRight = true;

    void Update()
    {
        Vector3 pos = transform.position;

        if (movingRight)
        {
            pos.x += speed * Time.deltaTime;
            if (pos.x >= maxX) movingRight = false;
        }
        else
        {
            pos.x -= speed * Time.deltaTime;
            if (pos.x <= minX) movingRight = true;
        }

        transform.position = pos;
    }

    // Cridat des de BossHealth en entrar a fase 2
    public void EnterPhase2()
    {
        speed *= phase2SpeedMultiplier;
    }
}

