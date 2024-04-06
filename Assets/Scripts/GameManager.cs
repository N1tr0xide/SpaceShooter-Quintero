using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab, _uiCanvas;
    [SerializeField] private Text _healthText, _scoreText;
    private bool _isGameOver;
    private int _currentHealth, _currentScore;
    
    public BulletPoolerController BulletPooler { get; private set; }
    public static GameManager Instance { get; private set; }
    public static GameObject Player { get; private set; }
    public bool IsGameOver => _isGameOver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StartGame();
            return;
        }

        Destroy(gameObject);
    }

    private void StartGame()
    {
        BulletPooler = new BulletPoolerController(_bulletPrefab, gameObject.transform);
        Player = GameObject.FindWithTag("Player");
        _currentHealth = 3;
        _currentScore = 0;
        _healthText.text = $"Health: {_currentHealth}";
        _scoreText.text = $"Score: {_currentScore}";
    }

    public void ChangeHealth(int byAmount)
    {
        _currentHealth += byAmount;
        _healthText.text = $"Health: {_currentHealth}";
        if (_currentHealth > 0) return;
        _currentHealth = 0;
        _isGameOver = true;
    }
    
    public void ChangeScore(int byAmount)
    {
        _currentScore += byAmount;
        if (_currentScore < 0) _currentScore = 0;
        _scoreText.text = $"Score: {_currentScore}";
    }
}
