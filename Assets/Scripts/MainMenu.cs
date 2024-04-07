using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text _highScoreText;
    [SerializeField] private GameObject _instructionsPanel;

    private void Start()
    {
        _instructionsPanel.SetActive(false);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        _highScoreText.text = $"High Score: {highScore}";
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ActivatePanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void DeactivatePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
