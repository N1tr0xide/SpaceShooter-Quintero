using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class ShootingEnemy : SpawnerObject
{
    [SerializeField] private float _speed, _lifeTime;
    private bool _isGameOver;
    private BulletPoolerController _bulletPool;
    private GameObject _player;

    private void Start()
    {
        _player = GameManager.Player;
        _bulletPool = GameManager.Instance.BulletPooler;
    }

    private void OnEnable()
    {
        StartCoroutine(SelfDeactivate(_lifeTime));
        StartCoroutine(InstantiateObject());
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
    
    private IEnumerator InstantiateObject()
    {
        while (!_isGameOver)
        {
            float delay = Random.Range(2, 4);
            yield return new WaitForSeconds(delay);
            GameObject newBullet = _bulletPool.BulletPool.Get();
            newBullet.transform.position = transform.position;
            newBullet.transform.LookAt(_player.transform.position);
        }
    }
}
