using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    Won,
    Lost
}

public class GameManager : MonoBehaviour
{
    public GameState gameState = GameState.Playing;

    public TimerController timerController;
    public PlayerController playerController;

    public TextMeshProUGUI timeRemainingTextLosePanel;

    public GameObject winPanel;
    public GameObject losePanel;

    public AdsManager ads;

    public void Update()
    {
        // Call the CheckWinLoseConditions function from the PlayerController
        playerController.CheckWinLoseConditions();
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("GameWon");
        //ads.PlayAd();
    }

    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
        //ads.PlayAd();
        AudioManager.Instance.PlaySFX("GameLost");
        timeRemainingTextLosePanel.text = FormatTime(timerController.timeRemaining);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UnlockNewLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
