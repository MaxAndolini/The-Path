using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static bool gamePause;
    public GameObject logo1;
    public GameObject logo2;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject main;
    public GameObject instructions;
    public GameObject settings;
    public GameObject musicSlider;
    public GameObject soundSlider;
    private Sequence mySequence1;
    private Sequence mySequence2;

    private void Start()
    {
        PauseGame(true);

        musicSlider.GetComponent<Slider>().value = SaveManager.Instance.music;
        soundSlider.GetComponent<Slider>().value = SaveManager.Instance.sound;

        Logo();
        Main();
    }

    public void PauseGame(bool status)
    {
        Time.timeScale = status ? 0 : 1;
        gamePause = status;
    }

    public void Logo()
    {
        mySequence1 = DOTween.Sequence();
        mySequence1.Append(logo1.transform.DOLocalMoveX(-95f, 2f));
        mySequence1.Append(logo1.transform.DOLocalMoveX(-246f, 2f));
        mySequence1.SetUpdate(true);
        mySequence1.SetLoops(-1);

        mySequence2 = DOTween.Sequence();
        mySequence2.Append(logo2.transform.DOLocalMoveX(95f, 2f));
        mySequence2.Append(logo2.transform.DOLocalMoveX(158f, 2f));
        mySequence2.SetUpdate(true);
        mySequence2.SetLoops(-1);
    }

    public void Main()
    {
        main.SetActive(true);
        instructions.SetActive(false);
        settings.SetActive(false);
    }

    public void Play()
    {
        mainMenu.SetActive(false);
        mySequence1.Kill();
        mySequence2.Kill();
        PauseGame(false);
    }

    public void Pause()
    {
        PauseGame(true);
        pauseMenu.SetActive(true);
    }

    public void UnPause()
    {
        PauseGame(false);
        pauseMenu.SetActive(false);
    }

    public void Instructions()
    {
        SoundManager.Instance.PlayOneShot("Button");
        main.SetActive(false);
        instructions.SetActive(true);
    }

    public void Settings()
    {
        SoundManager.Instance.PlayOneShot("Button");
        main.SetActive(false);
        settings.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
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