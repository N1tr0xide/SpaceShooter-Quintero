using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerObject : MonoBehaviour
{
    private ObjectPool<GameObject> _pool;

    public ObjectPool<GameObject> Pool
    {
        get => _pool;
        set
        {
            if(_pool == null) _pool = value;
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.CompareTag("HealthPickup") || gameObject.CompareTag("X2Pickup"))
        {
            if(other.CompareTag("Bullet") || other.CompareTag("Enemy")) return;
            Pool.Dispose();
        }
        else if(!other.CompareTag("Enemy")) Pool.Release(gameObject);
    }
}
