using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcmovetest : MonoBehaviour
{
    public float speed = 2f;
    public bool moveRight = true;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 movement = (moveRight ? Vector2.right : Vector2.left) * speed * Time.deltaTime;
        transform.Translate(movement);

        // Get the NPC's screen position
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

        //// Check if the NPC has hit the screen's edge
        //if (moveRight && transform.position.x >= 5f) // Adjust the value based on your test
        //{
        //    ChangeDirection();
        //}
        //else if (!moveRight && transform.position.x <= -5f) // Adjust the value based on your test
        //{
        //    ChangeDirection();
        //}
    }

    private void ChangeDirection()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        moveRight = !moveRight;
    }
}
