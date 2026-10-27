using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()) return;

        KitchenObject.SpawnKitchenObject(kitchenObjectsSO, player);
        OnPlayerGrabbedObject.Invoke(this, EventArgs.Empty);

    }
}
