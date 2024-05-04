using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    private int maxSpace = 25;   // Maximum amount of item spaces
    public int MaxSpace
    {
        get { return maxSpace; }
        set
        {
            maxSpace = value;
            UpdateInventoryUI();
        }
    }

    // Our current list of items in the inventory
    public List<Item> items = new List<Item>();

    // Add a new item if enough room
    public void Add(string itemName, Sprite itemIcon)
    {
        if (items.Count >= MaxSpace)
        {
            Debug.Log("Inventory is full.");
            return;
        }

        Item newItem = new Item(itemName, itemIcon);
        items.Add(newItem);

        // Trigger the onItemChangedCallback if it's not null
        onItemChangedCallback?.Invoke();
    }

    // Remove an item
    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    // Update the inventory UI
    private void UpdateInventoryUI()
    {
        InventoryUI.instance.UpdateUI();
    }
    // Clear the inventory
    public void ClearInventory()
    {
        items.Clear();

        // Trigger the onItemChangedCallback if it's not null
        onItemChangedCallback?.Invoke();
    }

    // Method to populate empty slots with X sprites beyond the current maximum
    public void PopulateEmptySlots()
    {
        int emptySlots = MaxSpace - items.Count;
        for (int i = 0; i < emptySlots; i++)
        {
            items.Add(new Item("Empty", GetXSprite()));
        }
        UpdateInventoryUI();
    }

    private Sprite GetXSprite()
    {
        // Load the sprite from the specified path
        Sprite xSprite = Resources.Load<Sprite>("Sprites/CloseButton");

        // Check if the sprite is loaded successfully
        if (xSprite == null)
        {
            Debug.LogError("X sprite not found!");
        }

        return xSprite;
    }
}

