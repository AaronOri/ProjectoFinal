using UnityEngine;

public class ZigZagEnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float amplitude = 2f;
    [SerializeField] float frequency = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float zigzag = Mathf.Sin(Time.time * frequency) * amplitude;
        Vector3 movement = new Vector3(zigzag, -1f, 0f).normalized * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }
}
