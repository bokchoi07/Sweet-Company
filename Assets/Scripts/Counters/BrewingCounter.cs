using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingCounter : BaseCounter, IHasProgress
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
        Brewing,
        Brewed,
    }

    [SerializeField] private BrewingRecipeSO[] brewingRecipeSOArray;
    
    private State state;
    private float brewingTimer;
    private BrewingRecipeSO brewingRecipeSO;

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
                case State.Brewing:
                    if (HasKitchenObject())
                    {
                        brewingTimer += Time.deltaTime;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = brewingTimer / brewingRecipeSO.brewProgressMax
                        });

                        if (brewingTimer > brewingRecipeSO.brewProgressMax)
                        {
                            // Object is brewed
                            GetKitchenObject().DestroySelf();

                            KitchenObject.SpawnKitchenObject(brewingRecipeSO.output, this);

                            state = State.Brewed;
                            brewingTimer = 0f;

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
                    break;
                case State.Brewed:
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

                    brewingRecipeSO = GetBrewingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Brewing;
                    brewingTimer = 0f;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = brewingTimer / brewingRecipeSO.brewProgressMax
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
                // player is holding something
                if (player.GetKitchenObject().TryGetCup(out CupKitchenObject cupKitchenObject))
                {
                    // player is holding a cup
                    if (cupKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
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
        BrewingRecipeSO brewingRecipeSO = GetBrewingRecipeSOWithInput(inputKitchenObjectSO);
        if (brewingRecipeSO != null)
        {
            return brewingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        BrewingRecipeSO brewingRecipeSO = GetBrewingRecipeSOWithInput(inputKitchenObjectSO);
        return brewingRecipeSO != null;
    }

    private BrewingRecipeSO GetBrewingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BrewingRecipeSO brewingRecipeSO in brewingRecipeSOArray)
        {
            if (brewingRecipeSO.input == inputKitchenObjectSO)
            {
                return brewingRecipeSO;
            }
        }

        return null;
    }
}

    /*
    public override void Interact(BobaShopPlayerController player)
    {
        if (!HasKitchenObject())
        {
            // there's no kitchen Object on the counter
            if (player.HasKitchenObject())
            {
                // player is holding something
                // if Object has valid brewing recipe
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // drop item onto counter
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    brewingProgress = 0;

                    BrewingRecipeSO brewingRecipeSO = GetBrewingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    // fire progress event
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
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
            // there is a kitchen Object on the counter
            if (player.HasKitchenObject())
            {
                // player is holding something
            }
            else
            {
                // player is not holding anything
                // give kitchen Object to player to pick up
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(BobaShopPlayerController player)
    {
        // if there is KitchenObject here AND it can be brewed
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            // there is Object on brewing counter
            brewingProgress++;
            BrewingRecipeSO brewingRecipeSO = GetBrewingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            // fire progress event
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float) brewingProgress / brewingRecipeSO.brewProgressMax
            });

            // if we reach the max brewing progress
            if (brewingProgress >= brewingRecipeSO.brewProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                // spawn brewed version
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        BrewingRecipeSO brewingRecipeSO = GetBrewingRecipeSOWithInput(inputKitchenObjectSO);
        if (brewingRecipeSO != null)
        {
            return brewingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }
    
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        BrewingRecipeSO brewingRecipeSO = GetBrewingRecipeSOWithInput(inputKitchenObjectSO);
        return brewingRecipeSO != null;
    }

    private BrewingRecipeSO GetBrewingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BrewingRecipeSO brewingRecipeSO in brewingRecipeSOArray)
        {
            if (brewingRecipeSO.input == inputKitchenObjectSO)
            {
                return brewingRecipeSO;
            }
        }

        return null;
    }
}*/
