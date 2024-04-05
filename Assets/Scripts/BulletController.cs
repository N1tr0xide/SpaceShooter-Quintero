using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _speed, _lifeTime;
    private ObjectPool<GameObject> _pool;

    public ObjectPool<GameObject> Pool
    {
        get => _pool;
        set
        {
            if(_pool == null) _pool = value;
        } 
    }

    private void OnEnable()
    {
        StartCoroutine(SelfDeactivate(_lifeTime));
    }

    void Update()
    {
        transform.Translate(Vector3.forward * (_speed * Time.deltaTime), Space.Self);
    }

    private IEnumerator SelfDeactivate(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        _pool.Release(gameObject);
    }
}
