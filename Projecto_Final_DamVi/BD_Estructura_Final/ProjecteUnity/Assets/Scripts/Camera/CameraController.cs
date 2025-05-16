using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float verticalSpeed = 2f;

    void LateUpdate()
    {
        // Mueve la cámara hacia arriba a velocidad constante
        transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
    }
}


