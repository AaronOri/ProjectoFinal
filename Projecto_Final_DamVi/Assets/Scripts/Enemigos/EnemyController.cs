using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum MovementType { DownPauseSide, Looping, ZigZag }
    public MovementType movementType;

    public float speed = 2f;
    public float pauseTime = 1.5f;

    float moveTimer;
    float zigzagTimer;

    void Update()
    {
        switch (movementType)
        {
            case MovementType.DownPauseSide:
                DownPauseSideMovement();
                break;
            case MovementType.Looping:
                LoopingMovement();
                break;
            case MovementType.ZigZag:
                ZigZagMovement();
                break;
        }
    }

    void DownPauseSideMovement()
    {
        if (moveTimer < pauseTime)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            moveTimer += Time.deltaTime;
        }
        else if (moveTimer < pauseTime + 1.5f)
        {
            moveTimer += Time.deltaTime; // pausa
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    void LoopingMovement()
    {
        float radius = 2f;
        float frequency = 1.5f;
        float x = Mathf.Cos(Time.time * frequency) * radius;
        float y = -speed * Time.deltaTime;
        transform.Translate(new Vector3(x, y, 0) * Time.deltaTime * 60);
    }

    void ZigZagMovement()
    {
        zigzagTimer += Time.deltaTime * speed;
        float x = Mathf.Sin(zigzagTimer * 4) * 2f;
        transform.position += new Vector3(x, -speed * Time.deltaTime, 0) * Time.deltaTime * 60;
    }
}

