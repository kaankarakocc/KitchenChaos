using Unity.VisualScripting;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    private AudioSource audioSource;
    private float footstepTimer;
    private float footstepInterval = 0.3f;
    [SerializeField] private SoundManager soundManager;


    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (!player.IsWalking()) return;
        footstepTimer -= Time.deltaTime;

        if (footstepTimer < 0)
        {
            footstepTimer = footstepInterval;
            soundManager.PlayFootstep();
        }

    }
}
