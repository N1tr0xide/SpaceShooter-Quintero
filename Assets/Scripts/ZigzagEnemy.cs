using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigzagEnemy : SpawnerObject
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _rotatedOnce;

    private void OnEnable()
    {
        StartCoroutine(RandomRotate());
    }
    
    void Update()
    {
        transform.Translate(Vector3.forward * (_speed * Time.deltaTime), Space.Self);
    }

    private IEnumerator RandomRotate()
    {
        yield return new WaitForSeconds(.5f);
        transform.Rotate(Vector3.up, 45);

        while (!GameManager.Instance.IsGameOver)
        {
            yield return new WaitForSeconds(2);
            
            if (!_rotatedOnce)
            {
                transform.Rotate(Vector3.up, -90);
                _rotatedOnce = true;
            }
            else
            {
                transform.Rotate(Vector3.up, 90);
                _rotatedOnce = false;
            }
        }
        
    }
}
