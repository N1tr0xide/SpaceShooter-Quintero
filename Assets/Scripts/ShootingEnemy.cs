using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootingEnemy : SpawnerObject
{
    [SerializeField] private float _speed;
    private BulletPoolerController _bulletPool;
    private GameObject _player;

    private void Start()
    {
        _player = GameManager.Player;
        _bulletPool = GameManager.Instance.BulletPooler;
    }

    private void OnEnable()
    {
        StartCoroutine(ShootBullet());
    }
    
    private void Update()
    {
        float zPosition = transform.position.z - _speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
    }

    private IEnumerator ShootBullet()
    {
        while (!GameManager.Instance.IsGameOver)
        {
            float delay = Random.Range(2, 4);
            yield return new WaitForSeconds(delay);
            GameObject newBullet = _bulletPool.BulletPool.Get();
            newBullet.transform.position = transform.position;
            newBullet.transform.LookAt(_player.transform.position);
            newBullet.tag = "Enemy";
        }
    }
}
