using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu()]
public class RecipeListSO : ScriptableObject
{
    [SerializeField] private List<RecipeSO> recipeList;

    public List<RecipeSO> GetRecipes()
    {
        return recipeList;
    }
}
