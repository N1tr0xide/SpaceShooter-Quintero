using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidController : SpawnerObject
{
    [SerializeField] private float _speed, _lifeTime;

    private void OnEnable()
    {
        StartCoroutine(SelfDeactivate(_lifeTime));
    }

    // Update is called once per frame
    void Update()
    {
        float zPosition = transform.position.z - _speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
    }
    
    private IEnumerator SelfDeactivate(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Pool.Release(gameObject);
    }
}
