using UnityEngine;
using UnityEngine.Audio;

public class SaveManager : MonoBehaviour
{
    public AudioMixer mixer;
    public float music;
    public float sound;

    public static SaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        music = PlayerPrefs.GetFloat("MusicVolume", 0.6f);
        sound = PlayerPrefs.GetFloat("SoundVolume", 0.6f);
        Debug.Log(music);
        mixer.SetFloat("Music", Mathf.Log(music) * 20f);
        mixer.SetFloat("Sound", Mathf.Log(sound) * 20f);
    }

    public void SaveMusic(float musicVolume)
    {
        music = musicVolume;
        mixer.SetFloat("Music", Mathf.Log(musicVolume) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void SaveSound(float soundVolume)
    {
        sound = soundVolume;
        mixer.SetFloat("Sound", Mathf.Log(soundVolume) * 20f);
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
    }
}