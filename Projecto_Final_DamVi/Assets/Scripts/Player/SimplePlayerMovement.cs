using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float horizontalSpeed = 5f;
    [SerializeField] float verticalScrollSpeed = 2f;
    [SerializeField] float verticalControlAmount = 0.5f; // per permetre un petit control vertical
    [SerializeField] float leanAngle = 15f;

    SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical") * verticalControlAmount;

        Vector3 movement = new Vector3(moveX * horizontalSpeed, verticalScrollSpeed + moveY, 0f) * Time.deltaTime;
        transform.Translate(movement);

        // Rotació per inclinació visual (efecte estètic)
        float targetZRotation = -moveX * leanAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, targetZRotation);
    }
}



