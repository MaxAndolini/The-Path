using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [Space] [Header("Hearth")] public float currentHearth = 5;

    public Sprite[] textureHearth;
    public GameObject[] hearth;

    private void Start()
    {
        SetHealth(currentHearth);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Hurt();

            //Change Health
            currentHearth -= 0.5f;
            if (currentHearth >= 0) SetHealth(currentHearth);
            //else GameOver();
        }
        else if (col.gameObject.CompareTag("Thorn"))
        {
            Hurt();

            //Change Health
            currentHearth -= 0.5f;
            if (currentHearth >= 0) SetHealth(currentHearth);
            //else GameOver();
        }
        else if (col.gameObject.CompareTag("Spike"))
        {
            Hurt();

            //Change Health
            currentHearth -= 0.5f;
            if (currentHearth >= 0) SetHealth(currentHearth);
            //else GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Space"))
        {
            Hurt();

            //Change Health
            currentHearth = 0;
            if (currentHearth >= 0) SetHealth(currentHearth);
            //else GameOver();
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

    private void SetHealth(float h)
    {
        for (var i = 0; i < 5; i++) hearth[i].SetActive(false);

        var whole = Mathf.CeilToInt(h);

        for (var i = 0; i < whole; i++)
        {
            if (i == whole - 1)
            {
                hearth[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite =
                    IsThisInteger(h) ? textureHearth[0] : textureHearth[1];
            }

            hearth[i].SetActive(true);
        }

        currentHearth = h;
    }

    private static bool IsThisInteger(float h)
    {
        return Mathf.Approximately(h, Mathf.RoundToInt(h));
    }
}