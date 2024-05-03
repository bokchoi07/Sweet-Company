using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    // Define enum for different types of items
    public enum ItemType
    {
        None,  // Special value to represent uninitialized state
        UncookedBoba,
        TeaLeaves,
        Milk,
        MatchaPowder
        //MangoPoster
        
        // Add more types as needed
    }

    // Declare public variables for each prefab
    public GameObject UncookedBobaPrefab;
    public GameObject TeaLeavesPrefab;
    public GameObject MilkPrefab;
    public GameObject MatchaPowderPrefab;
    //public GameObject MangoPosterPrefab;

    // Enum variable to store the type of this item
    public ItemType itemType = ItemType.None; // Initialize to None

    private void Start()
    {
        // Instantiate the selected prefab if itemType is not None
        if (itemType != ItemType.None)
        {
            GameObject newItem = Instantiate(GetSelectedPrefab(), transform.position, Quaternion.identity, transform);
        }
    }

    private GameObject GetSelectedPrefab()
    {
        switch (itemType)
        {
            case ItemType.UncookedBoba:
                return UncookedBobaPrefab;
            case ItemType.TeaLeaves:
                return TeaLeavesPrefab;
            case ItemType.Milk:
                return MilkPrefab;
            case ItemType.MatchaPowder:
                return MatchaPowderPrefab;
            //case ItemType.MangoPoster:
                //return MangoPosterPrefab;
            default:
                return null;
        }
    }
}
