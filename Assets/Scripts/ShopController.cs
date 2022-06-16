using System;
using DG.Tweening;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private bool shopActive = false;
    
    private void Update()
    {
        if (shopActive && Input.GetKey(KeyCode.E))
        {
            Menu.Instance.Shop();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) shopActive = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        shopActive = false;
    }
}