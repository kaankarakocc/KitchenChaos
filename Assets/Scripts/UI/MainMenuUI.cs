using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button quitButton;

    private void Awake()
    {
        Time.timeScale = 1f;
        playButton.onClick.AddListener(PlayButton_OnClick);
        quitButton.onClick.AddListener(QuitButton_OnClick);
    }

    void PlayButton_OnClick()
    {
        Loader.Load(Loader.Scene.GameScene);
    }
    void QuitButton_OnClick()
    {
        Application.Quit();
    }
}
