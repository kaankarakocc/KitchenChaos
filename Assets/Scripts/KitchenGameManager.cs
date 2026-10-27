using System;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    [SerializeField] DeliveryManager deliveryManager;
    [SerializeField] GameInput gameInput;
    private bool isGamePaused;
    private float deliveryTimeBonus = 15f;
    private State currentState = State.WaitingToStart;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 40f;
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;


    private void Start()
    {
        deliveryManager.OnRemoveRecipe += DeliveryManager_OnRemoveRecipe;
        gameInput.OnPauseAction += GameInput_OnPauseAction;
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (isGamePaused) return;
        if (currentState != State.WaitingToStart) return;
        currentState = State.CountdownToStart;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    void OnDestroy()
    {
        deliveryManager.OnRemoveRecipe -= DeliveryManager_OnRemoveRecipe;
        gameInput.OnPauseAction -= GameInput_OnPauseAction;
        gameInput.OnInteractAction -= GameInput_OnInteractAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }
    private void DeliveryManager_OnRemoveRecipe(object sender, DeliveryManager.DeliveryOrderEventArgs e)
    {
        if (!IsGamePlaying()) return;

        gamePlayingTimer += deliveryTimeBonus;
    }

    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private void Update()
    {
        switch (currentState)
        {
            default: return;
            case State.WaitingToStart:

                break;

            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0f)
                {
                    gamePlayingTimer = gamePlayingTimerMax;
                    currentState = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0f)
                {
                    currentState = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                }
                break;

            case State.GameOver:

                break;
        }
    }

    public bool IsGamePlaying()
    {
        return currentState == State.GamePlaying;
    }

    public bool IsCountdownActive()
    {
        return currentState == State.CountdownToStart;
    }

    public float GetCountdownTimer()
    {
        return countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return currentState == State.GameOver;
    }

    public float GetGamePlayingTimer()
    {
        return (1 - gamePlayingTimer / gamePlayingTimerMax);
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }
}
