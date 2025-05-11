using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawner : MonoBehaviour
{
    [SerializeField] private float lifespan = 15f;

    void Start()
    {
        Destroy(gameObject, lifespan);
    }
}
