using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;  

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int completedOrdersAmount;
    private int wrongOrdersAmount;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(CupKitchenObject cupKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == cupKitchenObject.GetKitchenObjectSOList().Count)
            {
                // has same number of ingredients
                bool cupContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    // cycling through all ingredients in the recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO cupKitchenObjectSO in cupKitchenObject.GetKitchenObjectSOList())
                    {
                        if (cupKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            // ingredient matches
                            ingredientFound = true;
                            break;

                        }
                        
                    }
                    if (!ingredientFound)
                    {
                        // ingredient was not found in the cup
                        cupContentsMatchesRecipe = false;
                    }
                }
                if (cupContentsMatchesRecipe) 
                {
                    // player delivered correct recipe
                    completedOrdersAmount++;

                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty); // playing sfx for correct recipe made
                    return;
                }
            }
        }

        // no matches found; did not deliver correct recipe
        wrongOrdersAmount++;

        OnRecipeFailed?.Invoke(this, EventArgs.Empty); // playing sfx for wrong recipe made
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetCompletedOrdersAmount()
    {
        return completedOrdersAmount;
    }

    public int GetWrongOrdersAmount()
    {
        return wrongOrdersAmount;
    }
}
