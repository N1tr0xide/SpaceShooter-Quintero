using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : SpawnerObject
{
    [SerializeField] private float _speed;
    
    void Update()
    {
        float zPosition = transform.position.z - _speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
    }
}
