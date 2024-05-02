using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenCounter : BaseCounter
{
    public void Interact()
    {
        Debug.Log("interact");
    }
    [SerializeField] private KitchenIngredientSO kitchenIngredientSO;

    public override void Interact(PlayerController player)
    {
        if (!HasKitchenIngredient())
        {
            // no kitchen ingredient on counter
            if (player.HasKitchenIngredient())
            {
                // player is holding ingredient
                player.GetKitchenIngredient().SetKitchenIngredientParent(this);
            }
            else
            {
                // player is not holding anything
            }
        } 
        else
        {
            // there is ingredient on counter
            if (player.HasKitchenIngredient())
            {
                // player is holding something
            }
            else
            {
                // player is not holidng anything
                GetKitchenIngredient().SetKitchenIngredientParent(player);
            }
        }
>>>>>>> Stashed changes
    }
}
