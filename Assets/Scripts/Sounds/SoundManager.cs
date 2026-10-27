using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private DeliveryManager deliveryManager;
    [SerializeField] private AudioClip[] deliverySuccessClips;
    [SerializeField] private AudioClip[] deliveryFailedClips;
    [SerializeField] private AudioClip[] chopClips;
    [SerializeField] private Player player;
    [SerializeField] private AudioClip[] pickupClips;
    [SerializeField] private AudioClip[] dropClips;
    [SerializeField] private AudioClip[] trashClips;
    [SerializeField] private AudioClip[] footstepClips;
    private int soundEffectsVolumeLevel = 10;
    private AudioSource audioSource;
    private const string SOUND_EFFECTS_VOLUME_KEY = "SoundEffectsVolume";

    [SerializeField] private AudioClip countdownClip;
    [SerializeField] private AudioClip warningClip;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        soundEffectsVolumeLevel = PlayerPrefs.GetInt(SOUND_EFFECTS_VOLUME_KEY);
    }

    private void Start()
    {
        deliveryManager.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;
        deliveryManager.OnRemoveRecipe += DeliveryManager_OnDeliverySuccess;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        player.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        audioSource.PlayOneShot(trashClips[Random.Range(0, trashClips.Length)]);
    }

    private void BaseCounter_OnAnyObjectPlaced(object sender, System.EventArgs e)
    {
        audioSource.PlayOneShot(dropClips[Random.Range(0, dropClips.Length)]);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        audioSource.PlayOneShot(pickupClips[Random.Range(0, pickupClips.Length)]);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        audioSource.PlayOneShot(chopClips[Random.Range(0, chopClips.Length)]);
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, DeliveryManager.DeliveryOrderEventArgs e)
    {
        audioSource.PlayOneShot(deliverySuccessClips[Random.Range(0, deliverySuccessClips.Length)]);
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, System.EventArgs e)
    {
        audioSource.PlayOneShot(deliveryFailedClips[Random.Range(0, deliveryFailedClips.Length)]);
    }

    public void PlayFootstep()
    {
        audioSource.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]);
    }

    private void OnDestroy()
    {
        deliveryManager.OnDeliveryFailed -= DeliveryManager_OnDeliveryFailed;
        deliveryManager.OnRemoveRecipe -= DeliveryManager_OnDeliverySuccess;
        CuttingCounter.OnAnyCut -= CuttingCounter_OnAnyCut;
        player.OnPickedSomething -= Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlaced -= BaseCounter_OnAnyObjectPlaced;
        TrashCounter.OnAnyObjectTrashed -= TrashCounter_OnAnyObjectTrashed;
    }

    public void ChangeSoundEffectsVolumeLevel()
    {
        soundEffectsVolumeLevel++;
        if (soundEffectsVolumeLevel > 10)
        {
            soundEffectsVolumeLevel = 0;
        }

        audioSource.volume = soundEffectsVolumeLevel / 10f;
        PlayerPrefs.SetInt(SOUND_EFFECTS_VOLUME_KEY, soundEffectsVolumeLevel);
        PlayerPrefs.Save();
    }

    public int GetSoundEffectsVolumeLevel()
    {
        return soundEffectsVolumeLevel;
    }

    public void PlayCountdownSound()
    {
        audioSource.PlayOneShot(countdownClip);
    }

    public void PlayWarningSound()
    {
        audioSource.PlayOneShot(warningClip);
    }
}
