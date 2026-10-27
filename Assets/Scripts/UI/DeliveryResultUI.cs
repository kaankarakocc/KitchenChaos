using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private DeliveryManager deliveryManager;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Image resultIcon;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        deliveryManager.OnRemoveRecipe += DeliveryManager_OnRemoveRecipe;
        deliveryManager.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        deliveryManager.OnRemoveRecipe -= DeliveryManager_OnRemoveRecipe;
        deliveryManager.OnDeliveryFailed -= DeliveryManager_OnDeliveryFailed;
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        backgroundImage.color = failedColor;
        resultIcon.sprite = failedSprite;
        resultText.text = "DELIVERY\nWRONG";
        Debug.Log("Teslimat paneli açıldı", this);
        animator.SetTrigger("ShowResult");
    }

    private void DeliveryManager_OnRemoveRecipe(object sender, DeliveryManager.DeliveryOrderEventArgs e)
    {
        gameObject.SetActive(true);
        backgroundImage.color = successColor;
        resultIcon.sprite = successSprite;
        resultText.text = "DELIVERY\nSUCCESS";
        Debug.Log("Teslimat paneli açıldı", this);
        animator.SetTrigger("ShowResult");
    }
}
