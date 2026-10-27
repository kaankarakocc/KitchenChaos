using System;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private KitchenGameManager kitchenGameManager;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private SoundManager soundManager;
    private Animator animator;
    private int previousCountdownNumber = 0;

    private void Start()
    {
        kitchenGameManager.OnStateChanged += KitchenGameManager_OnStateChanged;
        animator = GetComponent<Animator>();
        Hide();
    }
    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(kitchenGameManager.GetCountdownTimer());
        countdownText.text = countdownNumber.ToString();

        if (countdownNumber > 0 && countdownNumber != previousCountdownNumber)
        {
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger("CountdownPopup");
            soundManager.PlayCountdownSound();
        }
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (kitchenGameManager.IsCountdownActive())
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
