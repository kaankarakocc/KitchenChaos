using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private DeliveryManager deliveryManager;
    [SerializeField] private TextMeshProUGUI deliveryCountText;
    [SerializeField] private KitchenGameManager kitchenGameManager;

    private void Start()
    {
        kitchenGameManager.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (kitchenGameManager.IsGameOver())
        {
            deliveryCountText.text = deliveryManager.GetSuccessfulDeliveryCount().ToString();
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
