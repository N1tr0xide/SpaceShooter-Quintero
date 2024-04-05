using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPoolerController
{
    private readonly Transform _parent;
    private readonly GameObject _bulletPrefab;
    public ObjectPool<GameObject> BulletPool { get; }

    public BulletPoolerController(GameObject bulletPrefab, Transform parent)
    {
        _bulletPrefab = bulletPrefab;
        _parent = parent;
        BulletPool = new ObjectPool<GameObject>(CreateBullet, RetrieveBullet, ReleaseBullet, DestroyBullet);
    }

    private GameObject CreateBullet()
    {
        GameObject bullet = Object.Instantiate(_bulletPrefab, _parent);
        bullet.GetComponent<BulletController>().Pool = BulletPool;
        return bullet;
    }

    private void RetrieveBullet(GameObject bullet)
    {
        bullet.SetActive(true);
    }

    private void ReleaseBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }

    private void DestroyBullet(GameObject bullet)
    {
        Object.Destroy(bullet);
    }
}
