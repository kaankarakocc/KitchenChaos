using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    [SerializeField] private List<KitchenObjectsSO> recipeObjects;
    [SerializeField] private string recipeName;


    public List<KitchenObjectsSO> GetRecipeIngredients()
    {
        return recipeObjects;
    }

    public string GetRecipeName()
    {
        return recipeName;
    }

}
