using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    [SerializeField] private FryingObjectSO[] fryingObjectSOArray;
    [SerializeField] private BurntObjectSO[] burntObjectSOArray;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    private float fryingTimer;
    private float burnTimer;
    private FryingObjectSO activeFryingObj;
    private BurntObjectSO activeBurntObj;
    private State currentState = State.Idle;
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burnt
    }


    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    private void Update()
    {
        if (HasKitchenObject() && currentState == State.Frying)
        {
            fryingTimer += Time.deltaTime;
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progress = fryingTimer / activeFryingObj.fryingTime,
            });

            if (fryingTimer >= activeFryingObj.fryingTime)
            {
                GetKitchenObject().DestroyObject();
                KitchenObject.SpawnKitchenObject(activeFryingObj.output, this);
                activeBurntObj = GetBurntObjectWithInput(GetKitchenObject().GetKitchenObjectSO());
                burnTimer = 0;
                currentState = State.Fried;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = currentState
                });
            }
        }
        else if (HasKitchenObject() && currentState == State.Fried && activeBurntObj != null)
        {
            burnTimer += Time.deltaTime;
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progress = burnTimer / activeBurntObj.bruntTimeMax,
            });
            if (burnTimer >= activeBurntObj.bruntTimeMax)
            {
                GetKitchenObject().DestroyObject();
                KitchenObject.SpawnKitchenObject(activeBurntObj.output, this);
                currentState = State.Burnt;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = currentState
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progress = 0,
                });
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                activeFryingObj = GetFryingObjectWithInput(player.GetKitchenObject().GetKitchenObjectSO());
                if (activeFryingObj != null)
                {
                    fryingTimer = 0;
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    currentState = State.Frying;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = currentState
                    });
                }
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                currentState = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = currentState
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progress = 0,
                });
            }
            else
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    bool isAddedToPlate = plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO());
                    if (isAddedToPlate)
                    {
                        GetKitchenObject().DestroyObject();
                        currentState = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = currentState
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progress = 0,
                        });
                    }
                }
            }
        }
    }


    public FryingObjectSO GetFryingObjectWithInput(KitchenObjectsSO kitchenObjectsSO)
    {
        if (kitchenObjectsSO != null)
        {
            foreach (FryingObjectSO fryingObject in fryingObjectSOArray)
            {
                if (fryingObject.input != null && fryingObject.input == kitchenObjectsSO)
                {
                    return fryingObject;
                }
            }
        }
        return null;
    }

    public BurntObjectSO GetBurntObjectWithInput(KitchenObjectsSO kitchenObjectsSO)
    {
        if (kitchenObjectsSO != null)
        {
            foreach (BurntObjectSO burntObject in burntObjectSOArray)
            {
                if (burntObject.input != null && burntObject.input == kitchenObjectsSO)
                {
                    return burntObject;
                }
            }
        }
        return null;
    }

    public bool IsFried()
    {
        return currentState == State.Fried;
    }
}
