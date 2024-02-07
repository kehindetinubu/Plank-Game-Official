using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public GameManager gameManager;// added to stop game process on win / lose
    public TextMeshProUGUI timerText; // Reference to the text component to display the timer
    public float levelTime = 90f; // Total time for the level in seconds
    public float timeRemaining { get { return Mathf.Max(0f, timer); } /* Ensure the time remaining is never negative */ }
    public bool isTimerRunning = false;

    private float timer = 0f;

    // Start is called before the first frame update
    public void Start()
    {
        // Find the GameManager component in the scene
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }

        StartTimer();

    }

    // Update is called once per frame
    private void Update()
    {
        if (gameManager.gameState == GameState.Playing)
        {
            // Your normal update logic here
            if (isTimerRunning)
            {
                // Decrease the timer value and update the timer text
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    timer = 0f;
                    isTimerRunning = false;
                    // Handle level time-up logic here (e.g., game over or level completion)
                }
                UpdateTimerText();
            }
        }
    }

    // Method to start the level timer
    public void StartTimer()
    {
        isTimerRunning = true;
        timer = levelTime;
        UpdateTimerText();
    }

    // Method to stop the level timer
    public void StopTimer()
    {
        isTimerRunning = false;
        UpdateTimerText();
    }

    // Method to update the timer text
    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            //timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
