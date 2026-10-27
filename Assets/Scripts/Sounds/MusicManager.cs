using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    private int musicVolumeLevel = 4;
    private const string MUSIC_VOLUME_KEY = "MusicVolume";


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        musicVolumeLevel = PlayerPrefs.GetInt(MUSIC_VOLUME_KEY);
    }
    public void ChangeMusicVolumeLevel()
    {
        musicVolumeLevel++;
        if (musicVolumeLevel > 10)
        {
            musicVolumeLevel = 0;
        }

        audioSource.volume = musicVolumeLevel / 10f;
        PlayerPrefs.SetInt(MUSIC_VOLUME_KEY, musicVolumeLevel);
        PlayerPrefs.Save();
    }

    public int GetMusicVolumeLevel()
    {
        return musicVolumeLevel;
    }
}
