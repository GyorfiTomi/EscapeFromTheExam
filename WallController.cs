using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    // The rigidbody component of the player
    private Rigidbody2D playerRb;

    // The force multiplier for pushing the player back
    public float pushForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody component of the player by finding it by tag
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        
    }

    // This method is called when a trigger is detected
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other object has a tag "Player"
        if (other.CompareTag("Player"))
        {
            // Get the wall position
            Vector2 wallPos = other.transform.position;

            // Get the player position
            Vector2 playerPos = playerRb.transform.position;

            // Calculate the normal vector by subtracting the wall position from the player position and normalizing it
            Vector2 normal = (playerPos - wallPos).normalized;

            // Apply a force to the player's rigidbody in the opposite direction of the normal
            playerRb.AddForce(-normal * pushForce, ForceMode2D.Impulse);
        }
    }
}