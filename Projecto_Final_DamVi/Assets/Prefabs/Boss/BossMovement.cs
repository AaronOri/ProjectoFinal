using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float minX = -6f;
    [SerializeField] private float maxX = 6f;

    private bool movingRight = true;

    void Update()
    {
        Vector3 pos = transform.position;

        if (movingRight)
        {
            pos.x += speed * Time.deltaTime;
            if (pos.x >= maxX)
                movingRight = false;
        }
        else
        {
            pos.x -= speed * Time.deltaTime;
            if (pos.x <= minX)
                movingRight = true;
        }

        transform.position = pos;
    }
}

