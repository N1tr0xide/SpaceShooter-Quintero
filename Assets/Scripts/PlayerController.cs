using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private BulletPoolerController _bulletPooler;
    [SerializeField] private int _speed, _xLimits;
    
    void Start()
    {
        _bulletPooler = GameManager.Instance.BulletPooler;
    }
    
    void Update()
    {
        MoveHorizontal();
        if(Input.GetKeyDown(KeyCode.Space)) ShootBullet();
    }

    private void MoveHorizontal()
    {
        float xPosition = transform.position.x + Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
        xPosition = Mathf.Clamp(xPosition, -_xLimits, _xLimits);
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
    }

    private void ShootBullet()
    {
        GameObject newBullet = _bulletPooler.BulletPool.Get();
        newBullet.transform.position = transform.position;
        newBullet.transform.rotation = transform.rotation;
        newBullet.tag = "Bullet";
        GameManager.Instance.PlayPlayerShootingSfx();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.Instance.IsGameOver) return;
        
        if (other.CompareTag("Enemy"))
        {
            GameManager.Instance.ChangeHealth(-1);
            GameManager.Instance.PlayPlayerDamagedSfx();
        }
        else if (other.CompareTag("HealthPickup"))
        {
            GameManager.Instance.ChangeHealth(1);
        }
        else if (other.CompareTag("X2Pickup"))
        {
            GameManager.Instance.IncreaseMultiplier();
        }
    }
}
