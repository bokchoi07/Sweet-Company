using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenCounter : BaseCounter
{
    //[SerializeField] private KitchenIngredientSO kitchenIngredientSO;

    public override void Interact(PlayerController player)
    {
        if (!HasKitchenIngredient())
        {
            // there's no kitchen ingredient on the counter
            if (player.HasKitchenIngredient())
            {
                // player is holding something
                player.GetKitchenIngredient().SetKitchenIngredientParent(this);
            }
            else
            {
                // player is not holding anything
            }
        }
        else
        {
            // there is a kitchen ingredient on the counter
            if (player.HasKitchenIngredient())
            {
                // player is holding something
            }
            else
            {
                // player is not holding anything
                // give kitchen ingredient to player to pick up
                GetKitchenIngredient().SetKitchenIngredientParent(player);
            }
        }
    }
}
