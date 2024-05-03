using UnityEngine;
using System.Collections.Generic;

public class RandomItemAssignment : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Array of item prefabs to choose from

    // Map each prefab to its corresponding item type
    public Dictionary<GameObject, TestItem.ItemType> prefabToItemType = new Dictionary<GameObject, TestItem.ItemType>();

    void Start()
    {
        // Populate the dictionary
        InitializePrefabToItemType();

        // Get all child TestItem parent objects
        TestItem[] testItemParents = GetComponentsInChildren<TestItem>();

        foreach (TestItem testItemParent in testItemParents)
        {
            // Skip if the itemType is already set
            if (testItemParent.itemType != TestItem.ItemType.None)
            {
                continue;
            }

            // Find the cube placeholder associated with this TestItem parent and disable it
            GameObject cubePlaceholder = testItemParent.transform.Find("Cube").gameObject;
            Destroy(cubePlaceholder);

            // Generate a random index to choose a prefab
            int randomIndex = Random.Range(0, itemPrefabs.Length);

            // Instantiate the randomly chosen prefab as a child of the TestItem parent
            GameObject randomPrefab = itemPrefabs[randomIndex];
            GameObject instantiatedPrefab = Instantiate(randomPrefab, testItemParent.transform.position, Quaternion.identity, testItemParent.transform);

            // Get the TestItem component from the TestItem parent
            TestItem testItem = testItemParent.GetComponent<TestItem>();

            // Set the itemType of the TestItem based on the prefab
            testItem.itemType = prefabToItemType[randomPrefab];
        }
    }

    // Initialize the prefab to item type mapping
    void InitializePrefabToItemType()
    {
        // Example mapping: modify this according to your actual prefab-item type relationships
        prefabToItemType[itemPrefabs[0]] = TestItem.ItemType.UncookedBoba;
        prefabToItemType[itemPrefabs[1]] = TestItem.ItemType.TeaLeaves;
        prefabToItemType[itemPrefabs[2]] = TestItem.ItemType.Milk;
        prefabToItemType[itemPrefabs[3]] = TestItem.ItemType.MatchaPowder;
        //prefabToItemType[itemPrefabs[4]] = TestItem.ItemType.MangoPoster;
    }
}






