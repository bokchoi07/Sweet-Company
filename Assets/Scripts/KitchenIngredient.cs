using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenIngredient : MonoBehaviour
{
    [SerializeField] private KitchenIngredientSO kitchenIngredientSO;

    private KitchenCounter kitchenCounter;

    public KitchenIngredientSO GetKitchenIngredientSO()
    {
        return kitchenIngredientSO;
    }

    public void SetKitchenCounter(KitchenCounter kitchenCounter)
    {
        if (this.kitchenCounter != null)
        {
            this.kitchenCounter.ClearKitchenIngredient();
        }

        this.kitchenCounter = kitchenCounter;
        
        if (kitchenCounter.hasKitchenIngredient())
        {
            Debug.LogError("counter already has KitchenIngredient");
        }

        kitchenCounter.SetKitchenIngredient(this);

        transform.parent = kitchenCounter.GetKitchenIngredientFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public KitchenCounter GetKitchenCounter()
    {
        return kitchenCounter;
    }
}
