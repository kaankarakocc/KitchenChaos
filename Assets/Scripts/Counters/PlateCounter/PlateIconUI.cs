using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image image;

    public void SetKitchenObjectSOSprite(KitchenObjectsSO kitchenObjectSO)
    {
        image.sprite = kitchenObjectSO.sprite;
    }
}
