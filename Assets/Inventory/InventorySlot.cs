using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour {

	public Image icon;
	public Button removeButton;

	Item item;  // Current item in the slot

    // Add item to the slot
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon; // Update to use the correct property name
        icon.enabled = true;
        removeButton.interactable = true;
    }

    // Clear the slot
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    // If the remove button is pressed, this function will be called.
    public void RemoveItemFromInventory ()
	{
        Debug.Log("removeitemfrominventory()");
        Inventory.instance.Remove(item);
	}

	// Use the item
	public void UseItem ()
	{
		if (item != null)
		{
			item.Use();
		}
	}
    // Quit to the main menu
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
