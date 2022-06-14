using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private GameObject text;
    public GameObject shop;
    
    void Start()
    {
        text = gameObject.transform.GetChild(0).gameObject;
        
        var sequence = DOTween.Sequence();
        sequence.Append(text.transform.DOLocalMoveY(4.25f, 1f));
        sequence.Append(text.transform.DOLocalMoveY(4.75f, 1f));
        sequence.SetUpdate(true);
        sequence.SetLoops(-1);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            shop.SetActive(true);
        }
    }
}
