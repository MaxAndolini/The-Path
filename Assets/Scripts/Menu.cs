using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject logo1;
    public GameObject logo2;
    public GameObject main;
    public GameObject settings;
    public GameObject musicSlider;
    public GameObject soundSlider;

    private void Start()
    {
        Time.timeScale = 0;
        
        var mySequence1 = DOTween.Sequence();
        mySequence1.Append(logo1.transform.DOLocalMoveX(-95f, 2f));
        mySequence1.Append(logo1.transform.DOLocalMoveX(-246f, 2f));
        mySequence1.SetUpdate(true);
        mySequence1.SetLoops(-1);
        
        var mySequence2 = DOTween.Sequence();
        mySequence2.Append(logo2.transform.DOLocalMoveX(95f, 2f));
        mySequence2.Append(logo2.transform.DOLocalMoveX(158f, 2f));
        mySequence2.SetUpdate(true);
        mySequence2.SetLoops(-1);

        musicSlider.GetComponent<Slider>().value = SaveManager.Instance.music;
        soundSlider.GetComponent<Slider>().value = SaveManager.Instance.sound;
    }

    public void Play()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Settings()
    {
        main.SetActive(false);
        settings.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Main()
    {
        main.SetActive(true);
        settings.SetActive(false);  
    }
    
    public void SetMusicVolume(float musicVolume)
    {
        SaveManager.Instance.SaveMusic(musicVolume);
    }
    
    public void SetSoundVolume(float soundVolume)
    {
        SaveManager.Instance.SaveSound(soundVolume);
    }
}
