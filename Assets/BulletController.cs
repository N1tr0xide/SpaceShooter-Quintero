using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private int speed, lifetime;

    private void Start()
    {
        StartCoroutine(SelfDestroy(lifetime));
    }

    void Update()
    {
        float zPosition = transform.position.z + speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
    }

    private IEnumerator SelfDestroy(int lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
