using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NPCMovement : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    public GameManager gameManager; // added to stop game process on win / lose

    public float speed = 2f;
    public float pushForce = 2f;

    public bool moveRight = true; // Flag to control movement direction

    private bool isFrozen = false;

    private Rigidbody2D npcRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        npcRigidbody= GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Find the GameManager component in the scene
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameState == GameState.Playing)
        {
            // Your normal update logic here

            // Freeze logic
            if (isFrozen)
            {
                // NPCs are frozen, do not update movement
                return;
            }

            // Move in the specified direction
            Vector2 movement = (moveRight ? Vector2.right : Vector2.left) * speed * Time.deltaTime;
            transform.Translate(movement);

            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

            // Check if the NPC has hit the screen's edge
            if (moveRight && screenPosition.x >= Screen.width)
            {
                ChangeDirection();
            }
            else if (!moveRight && screenPosition.x <= 0)
            {
                ChangeDirection();
            }
        }
    }
    private void ChangeDirection()
    {
        // Change the direction of movement
        moveRight = !moveRight;
        // Flip the sprite's direction
        spriteRenderer.flipX = !spriteRenderer.flipX;
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

    public void PushAway(Vector3 pushDirection)
    {
        // Apply force to push the NPC away from the player
        npcRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
    }
}
