using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPoolerController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private ObjectPool<GameObject> bulletPool;

    private void Awake()
    {
        bulletPool = new ObjectPool<GameObject>(CreateBullet);
    }

    private void CreateBullet()
    {

    }

    private void RetrieveBullet(GameObject bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void ReleaseBullet(GameObject bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void DestroyBullet(GameObject bullet)
    {
        Destroy(bullet);
    }
}
