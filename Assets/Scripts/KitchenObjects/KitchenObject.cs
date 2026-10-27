using System;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectsSO GetKitchenObjectSO()
    {
        return kitchenObjectsSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (kitchenObjectParent.HasKitchenObject())
        {
            return;
        }

        this.kitchenObjectParent?.ClearKitchenObject();

        this.kitchenObjectParent = kitchenObjectParent;
        Transform kitchenObjectFollowTransform = this.kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.parent = kitchenObjectFollowTransform;
        transform.localPosition = Vector3.zero;
        this.kitchenObjectParent.SetKitchenObject(this);

    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroyObject()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(
    KitchenObjectsSO kitchenObjectsSO,
    IKitchenObjectParent parent)
    {
        Transform spawnedTransform =
            Instantiate(kitchenObjectsSO.prefab);

        KitchenObject spawnedObject =
            spawnedTransform.GetComponent<KitchenObject>();

        spawnedObject.SetKitchenObjectParent(parent);

        return spawnedObject;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        plateKitchenObject = this as PlateKitchenObject;
        if (plateKitchenObject == null)
        {
            return false;
        }
        return true;
    }
}
