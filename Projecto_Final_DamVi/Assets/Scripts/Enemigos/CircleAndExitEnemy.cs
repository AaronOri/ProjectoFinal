using UnityEngine;

public class CircleAndExitEnemy : MonoBehaviour
{
    [SerializeField] float radius = 2f;
    [SerializeField] float angularSpeed = 180f; // graus per segon
    [SerializeField] int numberOfCircles = 2;
    [SerializeField] Vector2 circleCenter = new Vector2(0f, 0f);
    [SerializeField] float exitSpeed = 3f;

    private float angle = 0f;
    private int circlesDone = 0;
    private bool exiting = false;
    private Vector3 center;

    void Start()
    {
        center = circleCenter;
    }

    void Update()
    {
        if (!exiting)
        {
            angle += angularSpeed * Time.deltaTime;
            float angleRad = angle * Mathf.Deg2Rad;

            Vector3 offset = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0) * radius;
            transform.position = center + offset;

            if (angle >= 360f)
            {
                angle -= 360f;
                circlesDone++;

                if (circlesDone >= numberOfCircles)
                {
                    exiting = true;
                }
            }
        }
        else
        {
            transform.Translate(Vector3.left * exitSpeed * Time.deltaTime);
        }
    }
}
