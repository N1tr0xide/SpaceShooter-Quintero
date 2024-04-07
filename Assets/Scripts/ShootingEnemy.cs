using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootingEnemy : SpawnerObject
{
    [SerializeField] private float _speed;
    private BulletPoolerController _bulletPool;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        _bulletPool = GameManager.Instance.BulletPooler;
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
            newBullet.transform.LookAt(GameManager.Player.transform.position);
            newBullet.tag = "Enemy";
            GameManager.Instance.PlayEnemyShootingSfx();
        }
    }
}
