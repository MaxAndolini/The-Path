using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource sound;

    public AudioClip menu;
    public AudioClip button;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        PlayMusic(menu);
    }

    public void PlayOneShot(string clip)
    {
        AudioClip play = null;
        switch (clip)
        {
            case "Button":
                play = button;
                break;
        }

        if (play != null) PlaySound(play);
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (music.isPlaying) music.Stop();

        music.clip = musicClip;
        music.Play();
    }

    public void PlaySound(AudioClip soundClip)
    {
        Debug.Log(soundClip.name);
        sound.PlayOneShot(soundClip);
    }
}