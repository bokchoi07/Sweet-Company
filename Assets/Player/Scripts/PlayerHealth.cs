using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth; // Current health of the player

    public HealthBar healthBar;
    public Inventory playerInventory; // Reference to the player's inventory
    public AudioSource deathSound; // Reference to the audio source for death sound

    private void Start()
    {
        // Initialize current health to max health when the game starts
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        // Check if the player's health is less than or equal to zero
        if (currentHealth <= 0)
        {
            Die(); // Call the Die method if health is zero or below
        }
    }

    // Method to handle player's death
    private void Die()
    {
        // Play death sound
        if (deathSound != null)
        {
            deathSound.Play();
        }
        // Clear the inventory when the player dies
        playerInventory.ClearInventory();

        Debug.Log("Player has died!");

        // Transition to the Office scene
        SceneManager.LoadScene("Office");
    }
}


