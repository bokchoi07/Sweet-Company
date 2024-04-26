using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupsCounter : BaseCounter
{
    public event EventHandler OnCupSpawned;
    public event EventHandler OnCupRemoved;

    [SerializeField] private KitchenIngredientSO cupKitchenIngredientSO;
    private float spawnCupTimer;
    private float spawnCupTimerMax = 4f;
    private int cupsSpawnedAmount;
    private int cupsSpawnedAmountMax= 3;

    private void Update()
    {
        spawnCupTimer += Time.deltaTime;
        if (spawnCupTimer >spawnCupTimerMax)
        {
            spawnCupTimer = 0f;

            if(cupsSpawnedAmount < cupsSpawnedAmountMax)
            {
                cupsSpawnedAmount++;

                OnCupSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(BobaShopPlayerController player)
    {
        if (!player.HasKitchenIngredient())
        {
            // Player is empty handed
            if (cupsSpawnedAmount > 0)
            {
                // There's at least one cup here
                cupsSpawnedAmount--;

                KitchenIngredient.SpawnKitchenIngredient(cupKitchenIngredientSO, player);
                OnCupRemoved?.Invoke(this, EventArgs.Empty);    
            }
        }
    }
}
