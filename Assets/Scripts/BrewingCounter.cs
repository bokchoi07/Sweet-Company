using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    [SerializeField] private BrewingRecipeSO[] brewingRecipeSOArray;

    private int brewingProgress;

    public override void Interact(BobaShopPlayerController player)
    {
        if (!HasKitchenIngredient())
        {
            // there's no kitchen ingredient on the counter
            if (player.HasKitchenIngredient()) //&& player.GetKitchenIngredient().CompareTag("TeaLeaves")
            {
                // player is holding something
                // if ingredient has valid brewing recipe
                if (HasRecipeWithInput(player.GetKitchenIngredient().GetKitchenIngredientSO()))
                {
                    // drop item onto counter
                    player.GetKitchenIngredient().SetKitchenIngredientParent(this);
                    brewingProgress = 0;

                    BrewingRecipeSO brewingRecipeSO = GetBrewingRecipeSOWithInput(GetKitchenIngredient().GetKitchenIngredientSO());

                    // fire progress event
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = (float) brewingProgress / brewingRecipeSO.brewProgressMax
                    });
                }
                
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

    public override void InteractAlt(BobaShopPlayerController player)
    {
        // if there is KitchenIngredient here AND it can be brewed
        if (HasKitchenIngredient() && HasRecipeWithInput(GetKitchenIngredient().GetKitchenIngredientSO()))
        {
            // there is ingredient on brewing counter
            brewingProgress++;
            BrewingRecipeSO brewingRecipeSO = GetBrewingRecipeSOWithInput(GetKitchenIngredient().GetKitchenIngredientSO());

            // fire progress event
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = (float) brewingProgress / brewingRecipeSO.brewProgressMax
            });

            // if we reach the max brewing progress
            if (brewingProgress >= brewingRecipeSO.brewProgressMax)
            {
                KitchenIngredientSO outputKitchenIngredientSO = GetOutputForInput(GetKitchenIngredient().GetKitchenIngredientSO());

                GetKitchenIngredient().DestroySelf();

                // spawn brewed version
                KitchenIngredient.SpawnKitchenIngredient(outputKitchenIngredientSO, this);
            }
        }
    }

    private KitchenIngredientSO GetOutputForInput(KitchenIngredientSO inputKitchenIngredientSO)
    {
        BrewingRecipeSO brewingRecipeSO = GetBrewingRecipeSOWithInput(inputKitchenIngredientSO);
        if (brewingRecipeSO != null)
        {
            return brewingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }
    
    private bool HasRecipeWithInput(KitchenIngredientSO inputKitchenIngredientSO)
    {
        BrewingRecipeSO brewingRecipeSO = GetBrewingRecipeSOWithInput(inputKitchenIngredientSO);
        return brewingRecipeSO != null;
    }

    private BrewingRecipeSO GetBrewingRecipeSOWithInput(KitchenIngredientSO inputKitchenIngredientSO)
    {
        foreach (BrewingRecipeSO brewingRecipeSO in brewingRecipeSOArray)
        {
            if (brewingRecipeSO.input == inputKitchenIngredientSO)
            {
                return brewingRecipeSO;
            }
        }

        return null;
    }
}
