using System;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    private int platesCount;
    [SerializeField] private int platesCountMax;
    [SerializeField] private KitchenGameManager kitchenGameManager;
    [SerializeField] private float plateTimerMax;
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    private float plateTimer;
    public event EventHandler OnPlatesSpawned;
    public event EventHandler OnPlatesRemoved;

    private void Update()
    {
        if (!kitchenGameManager.IsGamePlaying()) return;
        plateTimer += Time.deltaTime;

        if (plateTimer >= plateTimerMax)
        {
            plateTimer = 0f;
            if (platesCount < platesCountMax)
            {
                platesCount++;
                OnPlatesSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (platesCount > 0)
            {
                platesCount--;
                KitchenObject.SpawnKitchenObject(kitchenObjectsSO, player);
                OnPlatesRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
