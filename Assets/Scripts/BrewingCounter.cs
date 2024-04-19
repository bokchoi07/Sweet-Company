using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingCounter : BaseCounter
{
    [SerializeField] private KitchenIngredientSO brewedKitchenIngredientSO;

    public override void Interact(PlayerController player)
    {
        if (!HasKitchenIngredient())
        {
            // there's no kitchen ingredient on the counter
            if (player.HasKitchenIngredient() && player.GetKitchenIngredient().CompareTag("TeaLeaves"))
            {
                // player is holding something
                // so drop item onto counter
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

    public override void InteractAlt(PlayerController player)
    {
        if (HasKitchenIngredient())
        {
            // there is ingredient on brewing counter
            // destroy tea leaves
            GetKitchenIngredient().DestroySelf();

            // spawn new brewed tea object
            KitchenIngredient.SpawnKitchenIngredient(brewedKitchenIngredientSO, this);
        }
    }
}
