using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] private CuttingObjectSO[] cuttingObjects;
    private int cuttingProgress;
    public event EventHandler OnCut;
    public static event EventHandler OnAnyCut;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;


    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (GetCuttingObjectWithInput(player.GetKitchenObject().GetKitchenObjectSO()) != null)
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progress = 0,
                    });
                }
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
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
                        cuttingProgress = 0;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progress = 0,
                        });
                    }
                }
            }
        }

    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            KitchenObjectsSO kitchenObjectSO = GetKitchenObject().GetKitchenObjectSO();
            CuttingObjectSO cuttingObjectSO = GetCuttingObjectWithInput(kitchenObjectSO);

            if (cuttingObjectSO == null) return;

            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            float progressBar = (float)cuttingProgress / cuttingObjectSO.cuttingProgressMax;
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progress = progressBar,
            });

            if (cuttingProgress == cuttingObjectSO.cuttingProgressMax)
            {
                GetKitchenObject().DestroyObject();
                KitchenObjectsSO newSpawnedKitchenObjectSO = cuttingObjectSO.output;
                KitchenObject.SpawnKitchenObject(newSpawnedKitchenObjectSO, this);
            }

        }
    }

    private CuttingObjectSO GetCuttingObjectWithInput(KitchenObjectsSO input)
    {
        foreach (CuttingObjectSO cuttingObject in cuttingObjects)
        {
            if (cuttingObject.input == input)
            {
                return cuttingObject;
            }
        }
        return null;
    }
}
