using UnityEngine;
using System.Collections.Generic;

public class DeliverManagerUI : MonoBehaviour
{
    [SerializeField] private DeliveryManager deliveryManager;
    [SerializeField] private GameObject orderContanier;
    [SerializeField] private GameObject orderTemplate;
    Dictionary<int, GameObject> orderRows;

    private void Awake()
    {
        orderRows = new Dictionary<int, GameObject>();
    }
    private void Start()
    {
        deliveryManager.OnAddedNewRecipe += DeliveryManagerUI_OnAddedNewRecipe;
        deliveryManager.OnRemoveRecipe += DeliveryManagerUI_OnRemoveRecipe;
        orderTemplate.SetActive(false);
    }

    private void DeliveryManagerUI_OnRemoveRecipe(object sender, DeliveryManager.DeliveryOrderEventArgs e)
    {
        int orderId = e.order.GetIdentity();
        if (orderRows.TryGetValue(orderId, out GameObject orderRow))
        {
            orderRows.Remove(orderId);
            Destroy(orderRow);
        }
    }

    private void DeliveryManagerUI_OnAddedNewRecipe(object sender, DeliveryManager.DeliveryOrderEventArgs e)
    {
        GameObject newRow = Instantiate(orderTemplate, orderContanier.transform);
        newRow.SetActive(true);
        DeliveryOrderUI rowUI = newRow.GetComponent<DeliveryOrderUI>();
        rowUI.SetOrder(e.order);
        orderRows.Add(e.order.GetIdentity(), newRow);
    }
    private void OnDestroy()
    {
        deliveryManager.OnAddedNewRecipe -= DeliveryManagerUI_OnAddedNewRecipe;
        deliveryManager.OnRemoveRecipe -= DeliveryManagerUI_OnRemoveRecipe;
    }
}
