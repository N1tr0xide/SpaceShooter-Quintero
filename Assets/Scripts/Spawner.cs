using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsPrefabs;
    [SerializeField] private int _xLimits;
    private ObjectPool<GameObject> _objectPool;
    
    void Start()
    {
        _objectPool = new ObjectPool<GameObject>(CreateObject, RetrieveObject, ReleaseObject, DestroyObject);
        StartCoroutine(InstantiateObject());
    }

    private IEnumerator InstantiateObject()
    {
        while (!GameManager.Instance.IsGameOver)
        {
            float delay = Random.Range(1, 4);
            yield return new WaitForSeconds(delay);
            _objectPool.Get();
        }
    }

    private GameObject CreateObject()
    {
        Vector3 position = SetRandomPosition();
        GameObject randomObj = _objectsPrefabs[Random.Range(0, _objectsPrefabs.Length)];
        GameObject obj = Instantiate(randomObj, position, transform.rotation, transform);
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
        Object.Destroy(bullet);
    }
    
    private Vector3 SetRandomPosition()
    {
        float randomX = transform.position.x + Random.Range(-_xLimits, _xLimits);
        Vector3 position = new Vector3(randomX, transform.position.y, transform.position.z);
        return position;
    }
}
