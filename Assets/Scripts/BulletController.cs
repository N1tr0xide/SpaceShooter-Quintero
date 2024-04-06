using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _speed;
    private ObjectPool<GameObject> _pool;

    public ObjectPool<GameObject> Pool
    {
        set
        {
            if(_pool == null) _pool = value;
        } 
    }

    void Update()
    {
        transform.Translate(Vector3.forward * (_speed * Time.deltaTime), Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DeathZone")) _pool.Release(gameObject);
        
        else if (CompareTag("Bullet") && other.CompareTag("Enemy"))
        {
            _pool.Release(gameObject);
            //other.gameObject.SetActive(false);
            GameManager.Instance.ChangeScore(100);
        }
    }
}
