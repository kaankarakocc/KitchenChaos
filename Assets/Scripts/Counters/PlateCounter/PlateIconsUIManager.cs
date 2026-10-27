using UnityEngine;

public class PlateIconsUIManager : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] Transform iconTemplate;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        iconTemplate.gameObject.SetActive(false);
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        Transform newIcon = Instantiate(iconTemplate, transform);
        newIcon.gameObject.SetActive(true);
        newIcon.GetComponent<PlateIconUI>().SetKitchenObjectSOSprite(e.kitchenObjectsSO);

    }
}
