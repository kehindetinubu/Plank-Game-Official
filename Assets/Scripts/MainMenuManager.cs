using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public string levelSelectionSceneName = "LevelSelectionScene"; // The name of the Level Selection scene to load
    public string settingsSceneName = "SettingsScene"; // The name of the Settings scene to load
    // Add any other UI elements as needed (e.g., settings panel)

    public void PlayButtonClicked()
    {
        //SceneManager.LoadScene(levelSelectionSceneName); // Load the Level Selection scene when the Play button is clicked
        SceneManager.LoadSceneAsync(levelSelectionSceneName); // Load the Level Selection scene when the Play button is clicked
    }

    public void SettingsButtonClicked()
    {
        //SceneManager.LoadScene(settingsSceneName); // Load the Settings scene / panel when the Settings button is clicked
        SceneManager.LoadSceneAsync(settingsSceneName); // Load the Settings scene / panel when the Settings button is clicked
    }

    public void ExitButtonClicked()
    {
        Application.Quit(); // Quit the application when the Exit button is clicked (works in standalone builds)
    }
}
