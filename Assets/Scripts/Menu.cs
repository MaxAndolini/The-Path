using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public bool gamePause;
    public bool gameFinish;
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
        Main(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
                UnPause();
            else
                Pause();
        }
    }

    public void PauseGame(bool status)
    {
        Time.timeScale = status ? 0 : 1;
        gamePause = status;
    }

    public void Logo()
    {
        mySequence1 = DOTween.Sequence();
        mySequence1.Append(logo1.transform.DOLocalMoveX(-350f, 2f));
        mySequence1.Append(logo1.transform.DOLocalMoveX(-280f, 2f));
        mySequence1.SetUpdate(true);
        mySequence1.SetLoops(-1);

        mySequence2 = DOTween.Sequence();
        mySequence2.Append(logo2.transform.DOLocalMoveX(263f, 2f));
        mySequence2.Append(logo2.transform.DOLocalMoveX(193f, 2f));
        mySequence2.SetUpdate(true);
        mySequence2.SetLoops(-1);
    }

    public void Main(bool h)
    {
        if (h) SoundManager.Instance.PlayOneShot("Button");
        mainMenu.SetActive(true);
        main.SetActive(true);
        instructions.SetActive(false);
        settings.SetActive(false);
    }

    public void Play()
    {
        SoundManager.Instance.PlayOneShot("Button");
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
        gameFinish = h;
        SoundManager.Instance.PlayOneShot(h ? "Win" : "Lose");
        gameOverText.text = h ? "YOU WIN" : "GAME OVER";
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
        SoundManager.Instance.PlayOneShot("Button");
        Application.Quit();
    }

    public void Restart()
    {
        SoundManager.Instance.PlayOneShot("Button");
        UnPause();
        gameOverMenu.SetActive(false);
        var scene = 1;
        if (!gameFinish) scene = SceneManager.GetActiveScene().buildIndex;
        gameFinish = false;
        SceneManager.LoadScene(scene);
        PlayerController.Instance.Reset();
        SoundManager.Instance.PlayMusic("Background1");
    }

    public void MainMenu()
    {
        SoundManager.Instance.PlayOneShot("Button");
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        SceneManager.LoadScene(1);
        PlayerController.Instance.Reset();
        mySequence1.Play();
        mySequence2.Play();
        SoundManager.Instance.PlayMusic("Menu");
        Main(false);
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