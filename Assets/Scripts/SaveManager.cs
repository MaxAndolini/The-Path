using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public float music;
    public float sound;
    
    public static SaveManager Instance { get; private set; }
    
    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
        
        music = PlayerPrefs.GetFloat("MusicVolume", 0f);
        sound = PlayerPrefs.GetFloat("SoundVolume", 0f);
    }

    public void SaveMusic(float musicVolume)
    {
        music = musicVolume;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }
    
    public void SaveSound(float soundVolume)
    {
        sound = soundVolume;
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
    }
}
