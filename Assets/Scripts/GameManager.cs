using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab, _uiPanel, _gameOverPanel;
    [SerializeField] private Text _healthText, _scoreText, _multiplierText;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SoundReferences _soundReferences;
    private bool _isGameOver;
    private int _currentHealth, _currentScore, _currentMultiplier, _highScore;

    public BulletPoolerController BulletPooler { get; private set; }
    public static GameManager Instance { get; private set; }
    public static GameObject Player { get; private set; }
    public bool IsGameOver => _isGameOver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        _currentMultiplier = 1;
        _healthText.text = $"Health: {_currentHealth}";
        _scoreText.text = $"Score: {_currentScore}";
        _multiplierText.text = $"X{_currentMultiplier}";
        _uiPanel.SetActive(true);
        _gameOverPanel.SetActive(false);
    }
    
    private void GameOver()
    {
        _isGameOver = true;
        CheckHighScore();
        _uiPanel.SetActive(false);
        _gameOverPanel.SetActive(true);
        BulletPooler.BulletPool.Clear();
    }

    private void CheckHighScore()
    {
        if (_currentScore <= _highScore) return;
        
        _highScore = _currentScore;
        PlayerPrefs.SetInt("HighScore", _highScore);
    }

    public void ChangeHealth(int byAmount)
    {
        _currentHealth += byAmount;

        if (byAmount < 0)
        {
            _currentMultiplier = 1;
            _multiplierText.text = $"X{_currentMultiplier}";
        }
        
        if (_currentHealth > 3) _currentHealth = 3;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            GameOver();
        }
        
        _healthText.text = $"Health: {_currentHealth}";
    }
    
    public void ChangeScore(int byAmount)
    {
        _currentScore += byAmount * _currentMultiplier;
        if (_currentScore < 0) _currentScore = 0;
        _scoreText.text = $"Score: {_currentScore}";
    }
    
    public void IncreaseMultiplier()
    {
        _currentMultiplier *= 2;
        _multiplierText.text = $"X{_currentMultiplier}";
    }

    public void PlayPlayerShootingSfx()
    {
        _audioSource.PlayOneShot(_soundReferences.PlayerShooting);
    }
    
    public void PlayEnemyShootingSfx()
    {
        _audioSource.PlayOneShot(_soundReferences.EnemyShooting);
    }
    
    public void PlayPlayerDamagedSfx()
    {
        _audioSource.PlayOneShot(_soundReferences.PlayerDamaged);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
