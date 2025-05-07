using UnityEngine;

public class DropAndExitEnemyMovement : MonoBehaviour
{
    [SerializeField] float dropSpeed = 2f;
    [SerializeField] float waitTime = 2f;
    [SerializeField] float sideExitSpeed = 3f;
    [SerializeField] bool exitLeft = true;

    private float timer = 0f;
    private bool waiting = false;
    private bool exiting = false;

    void Update()
    {
        if (!waiting && !exiting)
        {
            transform.Translate(Vector3.down * dropSpeed * Time.deltaTime);
            if (transform.position.y <= Camera.main.transform.position.y +4) // Arribat al punt mig
            {
                waiting = true;
                timer = waitTime;
            }
        }
        else if (waiting)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                exiting = true;
                waiting = false;
            }
        }
        else if (exiting)
        {
            Vector3 direction = exitLeft ? Vector3.left : Vector3.right;
            transform.Translate(direction * sideExitSpeed * Time.deltaTime);
        }
    }
}
