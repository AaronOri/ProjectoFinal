using UnityEngine;

public class CircleAndExitEnemy : MonoBehaviour
{
    [SerializeField] float approachSpeed = 5f;
    [SerializeField] float radius = 2f;
    [SerializeField] float angularSpeed = 180f; // graus/s
    [SerializeField] int numberOfCircles = 2;
    [SerializeField] float exitSpeed = 5f;
    [SerializeField] float distanceToStartLoop = 0.5f;

    private enum State { Approaching, Looping, Exiting }
    private State currentState = State.Approaching;

    private float angle = 0f;
    private int circlesDone = 0;
    private Vector3 loopCenter;
    private Vector3 loopStartOffset;
    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        float cameraVerticalSpeed = 2f;
        transform.Translate(Vector3.up * cameraVerticalSpeed * Time.deltaTime);

        switch (currentState)
        {
            case State.Approaching:
                Approach();
                break;
            case State.Looping:
                Loop();
                break;
            case State.Exiting:
                Exit();
                break;
        }
    }

    void Approach()
    {
        Vector3 cameraCenter = Camera.main.transform.position;
        Vector3 centerTarget = new Vector3(cameraCenter.x, cameraCenter.y + 2f, 0f); // punt lleugerament a sobre del jugador

        Vector3 direction = (centerTarget - transform.position).normalized;
        transform.position += direction * approachSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, centerTarget) < distanceToStartLoop)
        {
            loopCenter = transform.position + (Vector3.left * radius); // o right, segons la direcció que vulguis
            loopStartOffset = transform.position - loopCenter;
            angle = Mathf.Atan2(loopStartOffset.y, loopStartOffset.x) * Mathf.Rad2Deg;
            currentState = State.Looping;
        }
    }

    void Loop()
    {
        angle -= angularSpeed * Time.deltaTime; // Sentit horari
        float angleRad = angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0f) * radius;
        transform.position = loopCenter + offset;

        transform.rotation = Quaternion.Euler(0f, 0f, angle); // Rotació visual horària

        if (angle <= -360f)
        {
            angle += 360f;
            circlesDone++;

            if (circlesDone >= numberOfCircles)
            {
                currentState = State.Exiting;
            }
        }
    }


    void Exit()
    {
        transform.position += Vector3.down * exitSpeed * Time.deltaTime;
    }
}

