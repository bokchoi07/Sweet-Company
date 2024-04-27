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
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Boiling:
                    if (HasKitchenObject())
                    {
                        boilingTimer += Time.deltaTime;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = boilingTimer / boilingRecipeSO.boilTimerMax
                        });
                     
                        if (boilingTimer > boilingRecipeSO.boilTimerMax)
                        {
                            // Object is boiled
                            GetKitchenObject().DestroySelf();

                            KitchenObject.SpawnKitchenObject(boilingRecipeSO.output, this);

                            state = State.Boiled;
                            burningTimer = 0f;
                            burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

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
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

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
        if (!HasKitchenObject())
        {
            // there's no kitchen Object on the counter
            if (player.HasKitchenObject()) //&& player.GetKitchenObject().CompareTag("TeaLeaves")
            {
                // player is holding something
                // if Object has valid boiling recipe
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // drop item onto counter
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    boilingRecipeSO = GetBoilingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

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
            // there is a kitchen Object on the counter
            if (player.HasKitchenObject())
            {
                // player is holding a cup
                if (player.GetKitchenObject().TryGetCup(out CupKitchenObject cupKitchenObject))
                {
                    // player is holding a cup
                    if (cupKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

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
            else
            {
                // player is not holding anything
                // give kitchen Object to player to pick up
                GetKitchenObject().SetKitchenObjectParent(player);

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

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        BoilingRecipeSO boilingRecipeSO = GetBoilingRecipeSOWithInput(inputKitchenObjectSO);
        if (boilingRecipeSO != null)
        {
            return boilingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        BoilingRecipeSO boilingRecipeSO = GetBoilingRecipeSOWithInput(inputKitchenObjectSO);
        return boilingRecipeSO != null;
    }

    private BoilingRecipeSO GetBoilingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BoilingRecipeSO boilingRecipeSO in boilingRecipeSOArray)
        {
            if (boilingRecipeSO.input == inputKitchenObjectSO)
            {
                return boilingRecipeSO;
            }
        }

        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }

        return null;
    }
}
