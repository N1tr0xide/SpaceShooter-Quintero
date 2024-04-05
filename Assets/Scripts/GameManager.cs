using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    private static GameObject _player;
    private static GameManager _instance;
    private BulletPoolerController _bulletPooler;
    
    public BulletPoolerController BulletPooler => _bulletPooler;
    public static GameManager Instance => _instance;
    public static GameObject Player => _player;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _bulletPooler = new BulletPoolerController(_bulletPrefab, gameObject.transform);
    }
}
