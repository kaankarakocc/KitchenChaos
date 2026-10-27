using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    [SerializeField] private DeliveryManager deliveryManager;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                deliveryManager.DeliverRecipes(plateKitchenObject);
                player.GetKitchenObject().DestroyObject();

            }
        }
    }
}
