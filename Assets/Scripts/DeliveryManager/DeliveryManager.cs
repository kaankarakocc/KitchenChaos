using UnityEngine;
using System.Collections.Generic;
using System;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private RecipeListSO recipeList;
    [SerializeField] private KitchenGameManager kitchenGameManager;

    private List<DeliveryOrder> waitingOrders;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private List<RecipeSO> availableRecipes;
    public event EventHandler<DeliveryOrderEventArgs> OnAddedNewRecipe;
    public event EventHandler<DeliveryOrderEventArgs> OnRemoveRecipe;
    public event EventHandler OnDeliveryFailed;
    private int nextOrderId = 1;
    private RecipeSO selectedRecipe;
    private int successDeliveryCount = 0;

    public class DeliveryOrderEventArgs : EventArgs
    {
        public DeliveryOrder order;
    }

    private void Awake()
    {
        waitingOrders = new List<DeliveryOrder>();
        spawnRecipeTimer = spawnRecipeTimerMax;
        availableRecipes = recipeList.GetRecipes();
    }

    private void Update()
    {
        if (!kitchenGameManager.IsGamePlaying()) return;
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingOrders.Count < waitingRecipesMax)
            {
                selectedRecipe = (availableRecipes[UnityEngine.Random.Range(0, availableRecipes.Count)]);
                DeliveryOrder recipeWithIdentity = new DeliveryOrder(nextOrderId, selectedRecipe);
                waitingOrders.Add(recipeWithIdentity);
                nextOrderId++;
                OnAddedNewRecipe?.Invoke(this, new DeliveryOrderEventArgs
                {
                    order = recipeWithIdentity
                });
            }
        }
    }

    public void DeliverRecipes(PlateKitchenObject plateKitchenObject)
    {
        List<KitchenObjectsSO> plateObjectList = plateKitchenObject.GetPlateIngredients();
        for (int i = 0; i < waitingOrders.Count; i++)
        {
            DeliveryOrder orderWithIndex = waitingOrders[i];
            List<KitchenObjectsSO> recipeIngredients = orderWithIndex.GetRecipeSO().GetRecipeIngredients();

            if (recipeIngredients.Count == plateObjectList.Count)
            {
                bool allIngredientMatch = true;

                foreach (KitchenObjectsSO kitchenObjectsSO in recipeIngredients)
                {
                    if (!plateObjectList.Contains(kitchenObjectsSO))
                    {
                        allIngredientMatch = false;
                        break;
                    }
                }
                if (allIngredientMatch)
                {
                    Debug.Log("Recipe Matched");
                    waitingOrders.RemoveAt(i);
                    successDeliveryCount++;

                    OnRemoveRecipe?.Invoke(this, new DeliveryOrderEventArgs
                    {
                        order = orderWithIndex,
                    });

                    return;
                }
            }
        }
        Debug.Log("Recipe Not Match");

        OnDeliveryFailed?.Invoke(this, EventArgs.Empty);
    }

    public int GetSuccessfulDeliveryCount()
    {
        return successDeliveryCount;
    }
}