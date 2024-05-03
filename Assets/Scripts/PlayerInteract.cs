using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 2f; // Distance to detect interactable objects
    public KeyCode interactKey = KeyCode.E; // Key to interact
    public KeyCode inventoryKey = KeyCode.Q; // Key to open inventory
    public Text interactText; // UI text to display interaction prompts
    public Text inventoryText; // UI text to display inventory

    private GameObject currentInteractable; // Current interactable object in range
    private bool isInRange = false; // Flag to check if player is in range of interactable object
    private int itemCount = 0; // Count of items in inventory

    private void Update()
    {
        if (Input.GetKeyDown(interactKey) && isInRange && currentInteractable != null)
        {
            // Add your interaction logic here
            InteractWithObject(currentInteractable);
        }

        if (Input.GetKeyDown(inventoryKey))
        {
            // Toggle inventory display
            ShowInventory();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            isInRange = true;
            currentInteractable = other.gameObject;
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            isInRange = false;
            currentInteractable = null;
            interactText.gameObject.SetActive(false);
        }
    }

    private void InteractWithObject(GameObject interactable)
    {
        // Replace interactable object with enemy object
        // You can customize this logic based on your game
        Destroy(interactable);
        SpawnEnemy();

        // Increment item count in inventory
        itemCount++;

        // Update inventory text
        UpdateInventoryText();
    }

    private void SpawnEnemy()
    {
        // Add logic to spawn enemy
    }

    private void ShowInventory()
    {
        // Toggle inventory display
        inventoryText.gameObject.SetActive(!inventoryText.gameObject.activeSelf);
    }

    private void UpdateInventoryText()
    {
        // Update inventory text with item count
        inventoryText.text = "Inventory: " + itemCount.ToString();
    }
}