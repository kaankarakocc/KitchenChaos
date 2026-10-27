using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<IngredientsVisual> ingredientsVisuals;

    [Serializable]
    public struct IngredientsVisual
    {
        public KitchenObjectsSO kitchenObjectsSO;
        public GameObject gameObject;
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (IngredientsVisual obj in ingredientsVisuals)
        {
            obj.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (IngredientsVisual obj in ingredientsVisuals)
        {
            if (obj.kitchenObjectsSO == e.kitchenObjectsSO)
            {
                obj.gameObject.SetActive(true);
            }
        }
    }
}
