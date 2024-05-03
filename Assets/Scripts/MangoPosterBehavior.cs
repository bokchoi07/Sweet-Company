using UnityEngine;

public class MangoPosterBehavior : MonoBehaviour
{
    public GameObject mangoposterPrefab; // Reference to the mangoposter prefab
    public int damageAmount = 10; // Amount of damage to inflict on the player

    private bool isActivated = false;
    private GameObject activeChild; // Reference to the active child object

    void Start()
    {
        // Get a random child object of mangoposterPrefab
        activeChild = GetRandomChildObject();
        activeChild.SetActive(true);
    }

    void ActivateMangoposter()
    {
        if (!isActivated)
        {
            isActivated = true;

            // Deactivate the active child object
            activeChild.SetActive(false);

            // Activate the mangoposter prefab
            mangoposterPrefab.SetActive(true);

            // Damage the player
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }

            // Add mangoposter sprite to inventory
            Inventory.instance.Add("Mangoposter", null); // Assuming you don't have a specific sprite to add
        }
    }

    // Method to get a random child object of mangoposterPrefab
    GameObject GetRandomChildObject()
    {
        int randomIndex = Random.Range(0, mangoposterPrefab.transform.childCount);
        return mangoposterPrefab.transform.GetChild(randomIndex).gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateMangoposter();
        }
    }
}










