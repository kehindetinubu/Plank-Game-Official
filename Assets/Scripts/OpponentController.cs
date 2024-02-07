using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    //public float moveSpeed = 2f;
    public int damageAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // the npc has touched the Plank
            PlayerControllerStandard player = collision.GetComponent<PlayerControllerStandard>();
            if (player != null) 
            {
                player.TakeDamage(damageAmount);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the opponent from one side to the other
        //transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}
