using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenIngredientSO kitchenIngredientSO;

    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenIngredient())
        {
            // player is not holding anything
            Transform kitchenIngredientTransform = Instantiate(kitchenIngredientSO.prefab);
            kitchenIngredientTransform.GetComponent<KitchenIngredient>().SetKitchenIngredientParent(player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
