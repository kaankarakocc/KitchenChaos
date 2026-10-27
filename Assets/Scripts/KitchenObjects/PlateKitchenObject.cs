using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private KitchenObjectsSO[] validIngredients;
    private List<KitchenObjectsSO> addedIngredients;
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectsSO kitchenObjectsSO;
    }

    private void Awake()
    {
        addedIngredients = new List<KitchenObjectsSO>();
    }

    public bool TryAddIngredient(KitchenObjectsSO kitchenObjectsSO)
    {
        foreach (KitchenObjectsSO obj in validIngredients)
        {
            if (obj == kitchenObjectsSO && addedIngredients.Contains(kitchenObjectsSO) == false)
            {
                addedIngredients.Add(obj);
                OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
                {
                    kitchenObjectsSO = kitchenObjectsSO
                });
                return true;
            }
        }
        return false;
    }

    public List<KitchenObjectsSO> GetPlateIngredients()
    {
        return addedIngredients;
    }
}
