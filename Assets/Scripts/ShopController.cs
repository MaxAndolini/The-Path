using System;
using DG.Tweening;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject shop;
    private GameObject text;
   /* private bool shopActive = false;

    private void Start()
    {
        text = gameObject.transform.GetChild(0).gameObject;

        var sequence = DOTween.Sequence();
        sequence.Append(text.transform.DOLocalMoveY(-2f, 1f));
        sequence.Append(text.transform.DOLocalMoveY(-1f, 1f));
        sequence.SetUpdate(true);
        sequence.SetLoops(-1);
    }

    private void Update()
    {
        if(shopActive && Input.GetKey(KeyCode.E)) shop.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) shopActive = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        shopActive = false;
    }*/
}