using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingsUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    private Action onCloseAction;
    [SerializeField] private KitchenGameManager kitchenGameManager;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private TextMeshProUGUI moveUpBindingText;
    [SerializeField] private TextMeshProUGUI moveDownBindingText;
    [SerializeField] private TextMeshProUGUI moveLeftBindingText;
    [SerializeField] private TextMeshProUGUI moveRightBindingText;
    [SerializeField] private TextMeshProUGUI interactBindingText;
    [SerializeField] private TextMeshProUGUI interactAlternateBindingText;
    [SerializeField] private TextMeshProUGUI pauseBindingText;
    [SerializeField] private GameObject rebindPromptUI;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;

    private void Start()
    {
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
        rebindPromptUI.SetActive(false);
        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseAction?.Invoke();
        });

        moveUpButton.onClick.AddListener(() =>
        {
            rebindPromptUI.SetActive(true);
            gameInput.RebindBinding(GameInput.Binding.MoveUp, RebindingFinished);
        });
        moveDownButton.onClick.AddListener(() =>
        {
            rebindPromptUI.SetActive(true);
            gameInput.RebindBinding(GameInput.Binding.MoveDown, RebindingFinished);
        });
        moveLeftButton.onClick.AddListener(() =>
        {
            rebindPromptUI.SetActive(true);
            gameInput.RebindBinding(GameInput.Binding.MoveLeft, RebindingFinished);
        });
        moveRightButton.onClick.AddListener(() =>
        {
            rebindPromptUI.SetActive(true);
            gameInput.RebindBinding(GameInput.Binding.MoveRight, RebindingFinished);
        });
        interactButton.onClick.AddListener(() =>
        {
            rebindPromptUI.SetActive(true);
            gameInput.RebindBinding(GameInput.Binding.Interact, RebindingFinished);
        });
        interactAlternateButton.onClick.AddListener(() =>
        {
            rebindPromptUI.SetActive(true);
            gameInput.RebindBinding(GameInput.Binding.InteractAlternate, RebindingFinished);
        });
        pauseButton.onClick.AddListener(() =>
        {
            rebindPromptUI.SetActive(true);
            gameInput.RebindBinding(GameInput.Binding.Pause, RebindingFinished);
        });

    }

    private void UpdateVisual()
    {
        moveUpBindingText.text = gameInput.GetBindingText(GameInput.Binding.MoveUp);
        moveDownBindingText.text = gameInput.GetBindingText(GameInput.Binding.MoveDown);
        moveLeftBindingText.text = gameInput.GetBindingText(GameInput.Binding.MoveLeft);
        moveRightBindingText.text = gameInput.GetBindingText(GameInput.Binding.MoveRight);
        interactBindingText.text = gameInput.GetBindingText(GameInput.Binding.Interact);
        interactAlternateBindingText.text = gameInput.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseBindingText.text = gameInput.GetBindingText(GameInput.Binding.Pause);
    }

    public void Show(Action onClose)
    {
        onCloseAction = onClose;
        UpdateVisual();
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void RebindingFinished()
    {
        rebindPromptUI.SetActive(false);
        UpdateVisual();
    }
}
