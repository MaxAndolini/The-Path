using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource sound;

    public AudioClip menu;
    public AudioClip background1;
    public AudioClip button;
    public AudioClip pain;
    public AudioClip jump;
    public AudioClip slide;
    public AudioClip doorLocked;
    public AudioClip doorOpen;
    public AudioClip chest;
    public AudioClip gold;
    public AudioClip potion;
    public AudioClip trampoline;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void PlayOneShot(string clip)
    {
        AudioClip play = null;
        switch (clip)
        {
            case "Button":
                play = button;
                break;
            case "Pain":
                play = pain;
                break;
            case "Jump":
                play = jump;
                break;
            case "Slide":
                play = slide;
                break;
            case "DoorLocked":
                play = doorLocked;
                break;
            case "DoorOpen":
                play = doorOpen;
                break;
            case "Chest":
                play = chest;
                break;
            case "Gold":
                play = gold;
                break;
            case "Potion":
                play = potion;
                break;
            case "Trampoline":
                play = trampoline;
                break;
        }

        if (play != null) PlayOneShot(play);
    }

    public void PlayMusic(string clip)
    {
        AudioClip play = null;
        switch (clip)
        {
            case "Menu":
                play = menu;
                break;
            case "Background1":
                play = background1;
                break;
        }

        if (play != null) PlayMusic(play);
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (music.isPlaying) music.Stop();

        music.clip = musicClip;
        music.Play();
    }

    public void PlayOneShot(AudioClip soundClip)
    {
        sound.PlayOneShot(soundClip);
    }
}