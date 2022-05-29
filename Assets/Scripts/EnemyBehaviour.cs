using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (IsFacingRight())
        {
            //Move right
            rb.velocity = new Vector2( moveSpeed,0f);
        }
        else 
        {
            //Move left
            rb.velocity = new Vector2(-moveSpeed,0f);
        } 
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x >= 0.01f;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Turn
        transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x) * 10), transform.localScale.y);
        
       
    }
}
