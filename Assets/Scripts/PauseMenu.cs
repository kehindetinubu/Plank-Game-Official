using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public string mainMenuSceneName = "mainMenuScene"; // The name of the Main Menu scene to load

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

    public void Audio()
    {

    }
}
