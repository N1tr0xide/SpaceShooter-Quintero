using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs, _pickupPrefabs;
    [SerializeField] private int _xLimits;
    private ObjectPool<GameObject> _objectPool;
    
    void Start()
    {
        _objectPool = new ObjectPool<GameObject>(CreateObject, RetrieveObject, ReleaseObject, DestroyObject);

        foreach (var prefab in _pickupPrefabs)
        {
            CreateObject(prefab);
        }
        foreach (var prefab in _enemyPrefabs)
        {
            CreateObject(prefab);
        }
        
        StartCoroutine(InstantiateObject());
    }

    private IEnumerator InstantiateObject()
    {
        while (!GameManager.Instance.IsGameOver)
        {
            yield return new WaitForSeconds(.5f);
            _objectPool.Get();
        }
    }

    private GameObject CreateObject()
    {
        Vector3 position = SetRandomPosition();
        GameObject randomObj = GetRandomObject();
        GameObject obj = Instantiate(randomObj, position, transform.rotation, transform);
        obj.GetComponent<SpawnerObject>().Pool = _objectPool;
        return obj;
    }
    
    private GameObject CreateObject(GameObject prefab)
    {
        Vector3 position = SetRandomPosition();
        GameObject obj = Instantiate(prefab, position, transform.rotation, transform);
        obj.GetComponent<SpawnerObject>().Pool = _objectPool;
        return obj;
    }
    
    private void RetrieveObject(GameObject obj)
    {
        Vector3 position = SetRandomPosition();
        obj.transform.position = position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
    }

    private void ReleaseObject(GameObject bullet)
    {
        bullet.SetActive(false);
    }

    private void DestroyObject(GameObject bullet)
    {
        Destroy(bullet);
    }

    private GameObject GetRandomObject()
    {
        return Random.Range(0, 100) > 70? _pickupPrefabs[Random.Range(0, _pickupPrefabs.Length)]  : _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
    }
    
    private Vector3 SetRandomPosition()
    {
        float randomX = transform.position.x + Random.Range(-_xLimits, _xLimits);
        Vector3 position = new Vector3(randomX, transform.position.y, transform.position.z);
        return position;
    }
}
