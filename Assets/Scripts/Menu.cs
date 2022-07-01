using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public bool gamePause;
    public GameObject logo1;
    public GameObject logo2;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject shopMenu;
    public GameObject gameOverMenu;
    public Text gameOverText;
    public Text gameOverGold;
    public GameObject main;
    public GameObject instructions;
    public GameObject settings;
    public GameObject musicSlider;
    public GameObject soundSlider;
    private Sequence mySequence1;
    private Sequence mySequence2;

    public static Menu Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        PauseGame(true);

        musicSlider.GetComponent<Slider>().value = SaveManager.Instance.music;
        soundSlider.GetComponent<Slider>().value = SaveManager.Instance.sound;

        SoundManager.Instance.PlayMusic("Menu");
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
        mainMenu.SetActive(true);
        main.SetActive(true);
        instructions.SetActive(false);
        settings.SetActive(false);
    }

    public void Play()
    {
        mainMenu.SetActive(false);
        mySequence1.Pause();
        mySequence2.Pause();
        PauseGame(false);
        SoundManager.Instance.PlayMusic("Background1");
    }

    public void Pause()
    {
        if (!gamePause)
        {
            PauseGame(true);
            pauseMenu.SetActive(true);
        }
    }

    public void UnPause()
    {
        PauseGame(false);
        pauseMenu.SetActive(false);
    }

    public void Shop()
    {
        if (!gamePause)
        {
            PauseGame(true);
            shopMenu.SetActive(true);
        }
    }

    public void CloseShop()
    {
        PauseGame(false);
        shopMenu.SetActive(false);
    }

    public void GameOver(bool h)
    {
        PauseGame(true);
        SoundManager.Instance.PlayOneShot((h) ? "Win" : "Lose");
        gameOverText.text = (h) ? "YOU WIN" : "GAME OVER";
        gameOverGold.text = PlayerController.Instance.gold.ToString();
        gameOverMenu.SetActive(true);
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

    public void Restart()
    {
        UnPause();
        gameOverMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerController.Instance.Reset();
        SoundManager.Instance.PlayMusic("Background1");
    }

    public void MainMenu()
    {
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        SceneManager.LoadScene(1);
        PlayerController.Instance.Reset();
        mySequence1.Play();
        mySequence2.Play();
        SoundManager.Instance.PlayMusic("Menu");
        Main();
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