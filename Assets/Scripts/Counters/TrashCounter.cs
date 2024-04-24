using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(BobaShopPlayerController player)
    {
        if (player.HasKitchenIngredient())
        {
            player.GetKitchenIngredient().DestroySelf();
        }
    }
}
