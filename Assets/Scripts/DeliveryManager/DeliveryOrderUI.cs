using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DeliveryOrderUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private GameObject iconContainer;
    [SerializeField] private GameObject iconTemplate;

    private void Awake()
    {
        iconTemplate.SetActive(false);
    }

    public void SetOrder(DeliveryOrder order)
    {
        string orderName = order.GetRecipeSO().GetRecipeName();
        recipeNameText.text = orderName;
        List<KitchenObjectsSO> newListIngredients = order.GetRecipeSO().GetRecipeIngredients();

        foreach (KitchenObjectsSO obj in newListIngredients)
        {
            GameObject newIconRow = Instantiate(iconTemplate, iconContainer.transform);
            newIconRow.SetActive(true);
            Image newRowImage = newIconRow.GetComponent<Image>();
            newRowImage.sprite = obj.sprite;
        }

    }
}
