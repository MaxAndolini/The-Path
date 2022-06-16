using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HealthController : MonoBehaviour
{
    [Space] [Header("Hearth")] public float currentHearth = 5;

    public Sprite[] textureHearth;
    public GameObject[] hearth;

    [Space] [Header("Blue Potion")] public GameObject bluePotion;
    public float bluePotionTimeRemaining = 10;
    private float bluePotionCurrentTime;
    private bool bluePotionIsRunning;
    private Text bluePotionText;

    public static HealthController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        SetHealth(currentHearth);
        bluePotionText = bluePotion.transform.GetChild(1).gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        if (bluePotionIsRunning)
        {
            if (bluePotionCurrentTime > 0)
            {
                bluePotionText.text = Mathf.FloorToInt(bluePotionCurrentTime % 60).ToString();
                bluePotionCurrentTime -= Time.deltaTime;
            }
            else
            {
                bluePotionCurrentTime = 0;
                bluePotionIsRunning = false;
                bluePotion.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Thorn") ||
            col.gameObject.CompareTag("Spike"))
        {
            Hurt();

            //Change Health
            if (bluePotionCurrentTime > 0) currentHearth -= Random.Range(0, Random.Range(0, 1)) == 0 ? 0f : 0.5f;
            else currentHearth -= 0.5f;
            if (currentHearth >= 0) SetHealth(currentHearth);
            else Menu.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Space"))
        {
            Menu.Instance.GameOver();
        }
    }

    public void Hurt()
    {
        //Hurt Animation
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        DOTween.Sequence()
            .Append(spriteRenderer.DOColor(Color.red, 0.05f))
            .Append(spriteRenderer.DOColor(Color.white, 0.7f));
    }

    public void GiveHealth(int h)
    {
        if (Math.Abs(currentHearth - 5.0f) > 0.1f)
        {
            if (currentHearth + h > 5.0f) currentHearth = 5.0f;
            else currentHearth += h;

            SetHealth(currentHearth);
        }
    }

    public void SetHealth(float h)
    {
        for (var i = 0; i < 5; i++) hearth[i].SetActive(false);

        var whole = Mathf.CeilToInt(h);

        for (var i = 0; i < whole; i++)
        {
            if (i == whole - 1)
                hearth[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite =
                    IsThisInteger(h) ? textureHearth[0] : textureHearth[1];
            else hearth[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = textureHearth[0];

            hearth[i].SetActive(true);
        }

        currentHearth = h;
    }

    public void UseBluePotion()
    {
        bluePotionCurrentTime += bluePotionTimeRemaining;
        bluePotionIsRunning = true;
        bluePotion.SetActive(true);
    }

    private static bool IsThisInteger(float h)
    {
        return Mathf.Approximately(h, Mathf.RoundToInt(h));
    }
}