using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth; // Current health of the player

    private void Start()
    {
        // Initialize current health to max health when the game starts
        currentHealth = maxHealth;
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the player's health is less than or equal to zero
        if (currentHealth <= 0)
        {
            Die(); // Call the Die method if health is zero or below
        }
    }

    // Method to handle player's death
    private void Die()
    {
        // You can add any death-related logic here, such as game over, respawn, etc.
        Debug.Log("Player has died!");
    }
}


