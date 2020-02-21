using UnityEngine;

// manages the background music
public class BackgroundAudioManager : MonoBehaviour
{
    private AudioClip defaultInGameMusic; // music in game
    private AudioClip bossBattleMusic; // music for boss battle
    private AudioSource audioSource; // an audiosource

    void Awake()
    {
        // assign the music clips
        defaultInGameMusic = Resources.Load("InGameMusic") as AudioClip;
        bossBattleMusic = Resources.Load("BossBattleMusic") as AudioClip;
    }
    void Start()
    {
        // create an audiosource we can refer to
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // if the boss is following the player and we are not currently playing the battle music,
        // stop the current music and play the new music
        if (BossManager.followPLayer && audioSource.clip != bossBattleMusic)
        {
            audioSource.Stop();
            audioSource.clip = bossBattleMusic;
            audioSource.Play();
        }

        // if no other background music is playing, play the defaultInGameMusic 
        if (!audioSource.isPlaying)
        {
            audioSource.clip = defaultInGameMusic;
            audioSource.Play();
        }
    }
}
