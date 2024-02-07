using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public GameManager gameManager; // added to stop game process on win / lose
    public PlayerController playerController;
    public float learnTime = 2f;
    //public float attackCooldown = 3f;

    private bool hasLearned = false;
    private bool isAttacking = false;
    private float cooldownTimer = 0f;

    private bool isFrozen = false;

    // Start is called before the first frame update
    void Start()
    {


        // Find the GameManager component in the scene
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }

        // Find the player GameObject based on its tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        // Check if the player GameObject was found and has the PlayerController script
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("Player GameObject not found or missing PlayerController script.");
        }
    }

    private void Update()
    {
        if (gameManager.gameState == GameState.Playing)
        {
            // Freeze logic
            if (isFrozen)
            {
                // NPCs are frozen, do not update attack
                return;
            }
            // Your normal update logic here
            if (hasLearned && isAttacking && cooldownTimer > 0f)
            {
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0f)
                {
                    AttackPlayer();
                    ResetAttack();
                }
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasLearned && collision.gameObject.CompareTag("Player"))
        {
            hasLearned = true;
            cooldownTimer = learnTime;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (hasLearned && !isAttacking && collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResetAttack();
        }
    }

    private void ResetAttack()
    {
        Debug.Log("npc attack reset");
        hasLearned = false;
        isAttacking = false;
        cooldownTimer = 0f;
    }

    private void AttackPlayer()
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController script reference is missing.");
            return;
        }

        if (playerController.isInvincible)
        {
            // Player is invincible, do not apply damage
            return;
        }

        Debug.Log("npc attacked");
        playerController.TakeDamage(1);
    }
    public void Freeze()
    {
        isFrozen = true;
        StartCoroutine(UnfreezeAfterDelay(3f)); // Freeze for 3 seconds
    }

    private IEnumerator UnfreezeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isFrozen = false;
    }
}
