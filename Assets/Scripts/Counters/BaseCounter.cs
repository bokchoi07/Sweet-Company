using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenIngredientParent
{
    [SerializeField] private Transform counterTopPoint;

    private KitchenIngredient kitchenIngredient;

    public virtual void Interact(BobaShopPlayerController player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAlternate(BobaShopPlayerController player)
    {
        // Debug.LogError("BaseCounter.InteractAlt();");
    }

    public Transform GetKitchenIngredientFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenIngredient(KitchenIngredient kitchenIngredient)
    {
        this.kitchenIngredient = kitchenIngredient;
    }

    public KitchenIngredient GetKitchenIngredient()
    {
        return kitchenIngredient;
    }

    public void ClearKitchenIngredient()
    {
        kitchenIngredient = null;
    }

    public bool HasKitchenIngredient()
    {
        return kitchenIngredient != null;
    }
}
