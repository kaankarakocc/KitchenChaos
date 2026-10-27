using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private KitchenGameManager kitchenGameManager;

    private void Start()
    {
        kitchenGameManager.OnStateChanged += KitchenGameManager_OnStateChanged;
        Show();
    }
    private void OnDestroy()
    {
        kitchenGameManager.OnStateChanged -= KitchenGameManager_OnStateChanged;
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (kitchenGameManager.IsCountdownActive())
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
