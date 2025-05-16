using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VerticalCameraLoop : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float posicion_inicial = 0f;
    [SerializeField] private float posicion_final = 10f;

    private void Start()
    {
        // Asegurarse que la cámara empieza en la posición inicial
        Vector3 pos = transform.position;
        pos.y = posicion_inicial;
        transform.position = pos;
    }

    private void Update()
    {
        // Mover la cámara hacia arriba
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Si supera la posición final, reiniciarla a la inicial
        if (transform.position.y >= posicion_final)
        {
            Vector3 pos = transform.position;
            pos.y = posicion_inicial;
            transform.position = pos;
        }
    }
}
