using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerStandard : MonoBehaviour
{

    public float moveSpeed = 5f;

    public float pushForce = 10f;
    public float pushDuration = 0.5f;

    private bool canPush = true;

    public int maxHealth = 3;
    public float invincibilityDuration = 2f;
    public GameObject gameOverScreen;

    private int currentHealth;
    private bool isInvincible = false;

    public TimerController timerController; // Reference to the TimerController

    public int requiredNPCsToSurvive = 10; // Number of NPCs the player needs to survive

    private int survivedNPCs = 0;

    public void NPCSurvived()
    {
        survivedNPCs++;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the plank left or right based on player taps/clicks
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Fire1") && canPush)
        {
            // Push opponents back when the player taps the screen
            canPush= false;
            Vector3 pushDirection = Vector3.right;
            StartCoroutine(PushOpponentsBack(pushDirection));
        }
       
        /*
         */
        // Check if the player meets the win conditions
        if ((timerController != null && timerController.isTimerRunning && survivedNPCs >= requiredNPCsToSurvive) ||
            (timerController != null && !timerController.isTimerRunning))
        {
            // Trigger level completion logic here
            // For example, load the level completion screen or load the next level
        }
    }

    private IEnumerator PushOpponentsBack(Vector3 direction)
    {
        foreach (GameObject opponent in GameObject.FindGameObjectsWithTag("Opponent"))
        {
            Rigidbody2D opponentRigidbody = opponent.GetComponent<Rigidbody2D>();
            opponentRigidbody.AddForce(direction * pushForce, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(pushDuration);
        canPush= true;
    }

    public void TakeDamage(int amount)
    {
        if (!isInvincible)
        {
            currentHealth -= amount;
            if(currentHealth <= 0)
            {
                GameOver();
            }
            else
            {
                // Trigger invincibility after taking damage
                isInvincible = true;
                Invoke("ResetInvincibility", invincibilityDuration);
            }
        }
    }

    private void ResetInvincibility()
    {
        isInvincible = false;
    }

    private void GameOver()
    {
        // Handle game over logic here(e.g., show game over screen, restart the level, etc.)
        gameOverScreen.SetActive(true);
        // You can handle restarting the level or showing an ad to continue playing, etc.
    }
}
