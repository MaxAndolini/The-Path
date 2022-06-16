using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator anim;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !PlayerControl.Instance.keyStatus)
        {
            anim.SetTrigger("openChest");
            SoundManager.Instance.PlayOneShot("Chest");
            PlayerControl.Instance.ChangeKey(true);
        }
    }
}
