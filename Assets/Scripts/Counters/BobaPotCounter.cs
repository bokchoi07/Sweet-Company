using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaPotCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Boiling,
        Boiled,
        Burned,
    }

    [SerializeField] private BoilingRecipeSO[] boilingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private float boilingTimer;
    private float burningTimer;
    private BoilingRecipeSO boilingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenIngredient())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Boiling:
                    if (HasKitchenIngredient())
                    {
                        boilingTimer += Time.deltaTime;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = boilingTimer / boilingRecipeSO.boilTimerMax
                        });
                     
                        if (boilingTimer > boilingRecipeSO.boilTimerMax)
                        {
                            // Ingredient is boiled
                            GetKitchenIngredient().DestroySelf();

                            KitchenIngredient.SpawnKitchenIngredient(boilingRecipeSO.output, this);

                            state = State.Boiled;
                            burningTimer = 0f;
                            burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenIngredient().GetKitchenIngredientSO());

                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                            {
                                state = state
                            });
                        }
                    }
                    break;
                case State.Boiled:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burnTimerMax
                    });

                    if (burningTimer > burningRecipeSO.burnTimerMax)
                    {
                        GetKitchenIngredient().DestroySelf();

                        KitchenIngredient.SpawnKitchenIngredient(burningRecipeSO.output, this);

                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(BobaShopPlayerController player)
    {
        if (!HasKitchenIngredient())
        {
            // there's no kitchen ingredient on the counter
            if (player.HasKitchenIngredient()) //&& player.GetKitchenIngredient().CompareTag("TeaLeaves")
            {
                // player is holding something
                // if ingredient has valid boiling recipe
                if (HasRecipeWithInput(player.GetKitchenIngredient().GetKitchenIngredientSO()))
                {
                    // drop item onto counter
                    player.GetKitchenIngredient().SetKitchenIngredientParent(this);

                    boilingRecipeSO = GetBoilingRecipeSOWithInput(GetKitchenIngredient().GetKitchenIngredientSO());

                    state = State.Boiling;
                    boilingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = boilingTimer / boilingRecipeSO.boilTimerMax
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

                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    private KitchenIngredientSO GetOutputForInput(KitchenIngredientSO inputKitchenIngredientSO)
    {
        BoilingRecipeSO boilingRecipeSO = GetBoilingRecipeSOWithInput(inputKitchenIngredientSO);
        if (boilingRecipeSO != null)
        {
            return boilingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private bool HasRecipeWithInput(KitchenIngredientSO inputKitchenIngredientSO)
    {
        BoilingRecipeSO boilingRecipeSO = GetBoilingRecipeSOWithInput(inputKitchenIngredientSO);
        return boilingRecipeSO != null;
    }

    private BoilingRecipeSO GetBoilingRecipeSOWithInput(KitchenIngredientSO inputKitchenIngredientSO)
    {
        foreach (BoilingRecipeSO boilingRecipeSO in boilingRecipeSOArray)
        {
            if (boilingRecipeSO.input == inputKitchenIngredientSO)
            {
                return boilingRecipeSO;
            }
        }

        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenIngredientSO inputKitchenIngredientSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenIngredientSO)
            {
                return burningRecipeSO;
            }
        }

        return null;
    }
}
