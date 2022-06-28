using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBullet : MonoBehaviour
{
    private float moveSpeed = 7f;

    private Rigidbody2D rb;

    private PlayerController target;

    private Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerController>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject,3f);
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }
}
