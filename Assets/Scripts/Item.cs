using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName; // Name of the item
    public Sprite icon; // Icon to represent the item in the UI
    public bool showInInventory = true; // Whether the item should be shown in the inventory

    // Constructor to initialize the item
    public Item(string name, Sprite icon)
    {
        itemName = name;
        this.icon = icon; // Assign the icon parameter to the class's icon property
    }

    // Method to use the item (you can customize this based on the behavior of your items)
    public void Use()
    {
        // Implement item usage logic here
        Debug.Log("Using " + itemName);
    }
}

