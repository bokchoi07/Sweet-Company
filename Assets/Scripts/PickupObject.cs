using System.Collections;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public float pickupRange = 3f; // Range within which the player can pick up the object
    public KeyCode pickupKey = KeyCode.E; // Key to press to pick up the object
    public Sprite itemIcon; // Icon representing the item in the inventory
    public GameObject promptUI; // UI element to display the pickup prompt
 
    private bool isInRange = false;
    public GameObject player;
    private bool isPickedUp = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetPromptTextVisibility(false); // Hide the pickup prompt UI initially
    }

    private void Update()
    {
        if (isInRange && !isPickedUp && Input.GetKeyDown(pickupKey))
        {
            // Perform pickup action
            PickupItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isInRange = true;
            SetPromptTextVisibility(true); // Show the pickup prompt UI
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isInRange = false;
            SetPromptTextVisibility(false); // Hide the pickup prompt UI
        }
    }

    private void PickupItem()
    {
        // Get the TestItem component attached to the parent object
        TestItem testItem = GetComponentInParent<TestItem>();
        if (testItem != null)
        {
            // Perform pickup action based on the TestItem's itemType
            switch (testItem.itemType)
            {
                case TestItem.ItemType.UncookedBoba:
                    Debug.Log("UncookedBoba picked up!");
                    break;
                case TestItem.ItemType.TeaLeaves:
                    Debug.Log("TeaLeaves picked up!");
                    break;
                case TestItem.ItemType.Milk:
                    Debug.Log("Milk picked up!");
                    break;
                case TestItem.ItemType.MatchaPowder:
                    Debug.Log("MatchaPowder picked up!");
                    break;
                //case TestItem.ItemType.MangoPoster:
                    //Debug.Log("MangoPoster picked up!");
                    //break;
                default:
                    Debug.Log("Unknown item type!");
                    break;
            }

            isPickedUp = true;
            Destroy(testItem.gameObject); // Destroy the parent object (which contains the pickup)
            SetPromptTextVisibility(false); // Hide the pickup prompt UI
                                            // Add the item to the inventory
            Inventory.instance.Add(testItem.itemType.ToString(), itemIcon);
        }
    }


    // Method to set the visibility of the pickup prompt UI
    private void SetPromptTextVisibility(bool isVisible)
    {
            promptUI.SetActive(isVisible);
    }
}








