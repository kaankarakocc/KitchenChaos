using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    private InputSystem_Actions inputSystemActions;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private const string BINDINGS_SAVE_KEY = "InputBindings";


    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate,
        Pause,
    }

    void Awake()
    {
        inputSystemActions = new InputSystem_Actions();

        if (PlayerPrefs.HasKey(BINDINGS_SAVE_KEY))
        {
            string bindingsJson = PlayerPrefs.GetString(BINDINGS_SAVE_KEY);
            inputSystemActions.LoadBindingOverridesFromJson(bindingsJson);
        }

        inputSystemActions.Player.Enable();

        inputSystemActions.Player.Interact.performed += Interact_Performed;
        inputSystemActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        inputSystemActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        rebindingOperation?.Dispose();
        inputSystemActions.Player.Interact.performed -= Interact_Performed;
        inputSystemActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        inputSystemActions.Player.Pause.performed -= Pause_performed;
        inputSystemActions.Player.Disable();
        inputSystemActions.Dispose();
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector2Normalized()
    {
        Vector2 inputVector = inputSystemActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default: return string.Empty;
            case Binding.MoveUp:
                return inputSystemActions.Player.Move.GetBindingDisplayString(2);
            case Binding.MoveDown:
                return inputSystemActions.Player.Move.GetBindingDisplayString(4);
            case Binding.MoveLeft:
                return inputSystemActions.Player.Move.GetBindingDisplayString(6);
            case Binding.MoveRight:
                return inputSystemActions.Player.Move.GetBindingDisplayString(8);
            case Binding.Interact:
                return inputSystemActions.Player.Interact.GetBindingDisplayString(0);
            case Binding.InteractAlternate:
                return inputSystemActions.Player.InteractAlternate.GetBindingDisplayString(0);
            case Binding.Pause:
                return inputSystemActions.Player.Pause.GetBindingDisplayString(0);
        }
    }

    public void RebindBinding(Binding binding, Action onFinished)
    {
        InputAction targetAction;
        int bindingIndex;

        if (rebindingOperation != null) return;

        switch (binding)
        {
            default: return;
            case Binding.MoveUp:
                targetAction = inputSystemActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.MoveDown:
                targetAction = inputSystemActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.MoveLeft:
                targetAction = inputSystemActions.Player.Move;
                bindingIndex = 6;
                break;
            case Binding.MoveRight:
                targetAction = inputSystemActions.Player.Move;
                bindingIndex = 8;
                break;
            case Binding.Interact:
                targetAction = inputSystemActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                targetAction = inputSystemActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                targetAction = inputSystemActions.Player.Pause;
                bindingIndex = 0;
                break;
        }
        inputSystemActions.Player.Disable();
        rebindingOperation = targetAction.PerformInteractiveRebinding(bindingIndex);
        rebindingOperation.WithControlsHavingToMatchPath("<Keyboard>/*");
        rebindingOperation.WithCancelingThrough("<Keyboard>/escape");

        rebindingOperation.OnComplete(operation =>
        {
            operation.Dispose();
            rebindingOperation = null;

            string bindingsJson = inputSystemActions.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString(BINDINGS_SAVE_KEY, bindingsJson);
            PlayerPrefs.Save();

            inputSystemActions.Player.Enable();
            onFinished?.Invoke();
        });

        rebindingOperation.OnCancel(operation =>
        {
            operation.Dispose();
            rebindingOperation = null;
            inputSystemActions.Player.Enable();
            onFinished?.Invoke();
        });

        rebindingOperation.Start();

    }
}
