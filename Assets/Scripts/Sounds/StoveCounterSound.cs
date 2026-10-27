using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private SoundManager soundManager;
    private AudioSource audioSource;
    private bool playWarningSound = false;
    private float warningTimer = 0f;
    private float warningInterval = 0.3f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        audioSource.Stop();
    }
    private void Update()
    {
        if (!playWarningSound || Time.deltaTime == 0)
        {
            return;
        }
        warningTimer -= Time.deltaTime;

        if (warningTimer <= 0f)
        {
            warningTimer = warningInterval;
            soundManager.PlayWarningSound();
        }
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        playWarningSound = stoveCounter.IsFried() && e.progress >= 0.5f;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        if (e.state != StoveCounter.State.Fried)
        {
            playWarningSound = false;
            warningTimer = 0f;
        }

        if (e.state == StoveCounter.State.Fried ||
            e.state == StoveCounter.State.Frying)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void OnDestroy()
    {
        stoveCounter.OnStateChanged -= StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged -= StoveCounter_OnProgressChanged;
    }
}
