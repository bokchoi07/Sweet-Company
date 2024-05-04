using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    // Static reference to the InventoryUI instance
    public static InventoryUI instance;

    public GameObject inventoryUI; // The entire UI
    public Transform itemsParent; // The parent object of all the items

    Inventory inventory; // Our current inventory

    InventorySlot[] slots;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Ensure that there's only one instance of InventoryUI
        if (instance != null)
        {
            // Destroy this instance if another instance already exists
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scene changes
    }

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        // Update UI on start
        UpdateUI();
    }

    // Check to see if we should open/close the inventory
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            UpdateUI();
        }
    }

    // Update the inventory UI by:
    //      - Adding items
    //      - Clearing empty slots
    // This is called using a delegate on the Inventory.
    public void UpdateUI()
    {
        if (slots.Length > 0)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < inventory.items.Count)
                {
                    slots[i].AddItem(inventory.items[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }
    }
}
