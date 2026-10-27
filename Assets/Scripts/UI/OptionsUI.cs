using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button volumeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI volumeText;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button keybindingsButton;
    private Action onCloseAction;
    [SerializeField] private KitchenGameManager kitchenGameManager;
    [SerializeField] private KeyBindingsUI keyBindingsUI;

    private void Start()
    {
        soundEffectsText.text = "Sound Effect : " + soundManager.GetSoundEffectsVolumeLevel();
        volumeText.text = "Volume : " + musicManager.GetMusicVolumeLevel();
        kitchenGameManager.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void OnDestroy()
    {
        kitchenGameManager.OnGameUnpaused -= KitchenGameManager_OnGameUnpaused;
    }

    private void Awake()
    {
        soundEffectsButton.onClick.AddListener(() =>
        {
            soundManager.ChangeSoundEffectsVolumeLevel();

            soundEffectsText.text = "Sound Effect : " + soundManager.GetSoundEffectsVolumeLevel();
        });

        volumeButton.onClick.AddListener(() =>
        {
            musicManager.ChangeMusicVolumeLevel();

            volumeText.text = "Volume : " + musicManager.GetMusicVolumeLevel();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseAction?.Invoke();
        });
        keybindingsButton.onClick.AddListener(() =>
        {
            Hide();
            keyBindingsUI.Show(ReturnFromKeyBindings);
        });
    }

    public void Show(Action onClose)
    {
        onCloseAction = onClose;
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }

    void ReturnFromKeyBindings()
    {
        gameObject.SetActive(true);
    }
}
