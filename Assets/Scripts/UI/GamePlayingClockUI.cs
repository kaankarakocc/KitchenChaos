using UnityEngine;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private KitchenGameManager kitchenGameManager;
    [SerializeField] private UnityEngine.UI.Image timerImage;

    private void Start()
    {
        kitchenGameManager.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }

    private void Update()
    {
        timerImage.fillAmount = kitchenGameManager.GetGamePlayingTimer();
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (kitchenGameManager.IsGamePlaying())
        {
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
