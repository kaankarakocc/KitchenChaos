using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    bool isAddedToPlate = plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO());
                    if (isAddedToPlate)
                    {
                        GetKitchenObject().DestroyObject();
                    }
                }
                else if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObjectOnCounter))
                {
                    bool isAddedToPlate = plateKitchenObjectOnCounter.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO());
                    if (isAddedToPlate)
                    {
                        player.GetKitchenObject().DestroyObject();
                    }
                }
            }
        }
    }


}
