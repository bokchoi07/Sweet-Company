using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenCounter : BaseCounter
{
    //[SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(BobaShopPlayerController player)
    {
        if (!HasKitchenObject())
        {
            // there's no kitchen Object on the counter
            if (player.HasKitchenObject())
            {
                // player is holding something
                // so drop item onto counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // player is not holding anything
            }
        }
        else
        {
            // there is a kitchen Object on the counter
            if (player.HasKitchenObject())
            {
                // player is holding something
                if (player.GetKitchenObject().TryGetCup(out CupKitchenObject cupKitchenObject))
                {
                    // player is holding a cup
                    if (cupKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) 
                    { 
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    // player is not holding cup but something else
                    if (GetKitchenObject().TryGetCup(out cupKitchenObject))
                    {
                        // counter is holding a cup
                        if (cupKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                // player is not holding anything
                // give kitchen Object to player to pick up
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
