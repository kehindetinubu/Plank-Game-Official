using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public static SceneManagement Instance; // Singleton pattern

    public string mainMenuSceneName = "mainMenuScene";
    public string levelSelectionSceneName = "LevelSelectionScene";
    public string settingsSceneName = "SettingsScene";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayButtonClicked()
    {
        SceneManager.LoadSceneAsync(levelSelectionSceneName);
    }

    public void SettingsButtonClicked()
    {
        SceneManager.LoadSceneAsync(settingsSceneName);
    }

    public void ExitButtonClicked()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void GoToNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;

        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevelIndex);
        }
        else
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Home()
    {
        SceneManager.LoadScene(mainMenuSceneName);
        Time.timeScale = 1.0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
}
