using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private KitchenGameManager kitchenGameManager;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button maninMenuButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private OptionsUI optionsUI;


    private void Awake()
    {
        resumeButton.onClick.AddListener(ResumeButton_Onclick);
        maninMenuButton.onClick.AddListener(MainMenuButton_OnClick);
        optionsButton.onClick.AddListener(() =>
        {
            Hide();
            optionsUI.Show(this.Show);
        });
    }
    private void Start()
    {
        kitchenGameManager.OnGamePaused += KitchenGameManager_OnGamePaused;
        kitchenGameManager.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        Hide();
    }

    private void OnDestroy()
    {
        kitchenGameManager.OnGamePaused -= KitchenGameManager_OnGamePaused;
        kitchenGameManager.OnGameUnpaused -= KitchenGameManager_OnGameUnpaused;
    }


    private void ResumeButton_Onclick()
    {
        kitchenGameManager.TogglePauseGame();
    }
    private void MainMenuButton_OnClick()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void KitchenGameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
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
