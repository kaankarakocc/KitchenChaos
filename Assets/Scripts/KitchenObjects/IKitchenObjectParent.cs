using UnityEngine;

public interface IKitchenObjectParent
{
    public void SetKitchenObject(KitchenObject kitchenObject);
    public Transform GetKitchenObjectFollowTransform();
    public void ClearKitchenObject();
    public bool HasKitchenObject();
}
