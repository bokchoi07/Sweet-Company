using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{


    public override void Interact(BobaShopPlayerController player)
    {
        if (player.HasKitchenObject()) 
        { 
            if (player.GetKitchenObject().TryGetCup(out CupKitchenObject cupKitchenObject))
            {
                // only accepts cups
                DeliveryManager.Instance.DeliverRecipe(cupKitchenObject);
                player.GetKitchenObject().DestroySelf();
            }
        }

    }
}
