using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //NOTES FROM INTERFACE:
    //In materials folder I use Physics Material 2D to prevent the character from Sticking to the wall. I reduce the friction to 0.
    
    [Header ("Karakter")]
    private float moveDirection; // which direction player move (1,0,-1)

    private int amountOfJumpsLeft;

    private bool isFacingRight = true;
    private bool isRunning;
    private bool isGrounded;
    private bool canJump;
    
    private Rigidbody2D rb;
    private Animator anim;

    public int amountOfJumps = 1;

    public float moveSpeed = 15.0f;
    public float jumpForce = 16.0f; //In rigidbody2d, we changed gravity scale with 4 for better jump experience.
    public float groundCheckRadius;
    
    public Transform groundCheck;

    public LayerMask whatIsGround; //using this, we can assign layers to the things we want.

    [Space]
    [Header ("Envanter")]
    public Slot[] inventory;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps; //we should equalize first. Because character does not jump yet. So if the character has the 1 jump, than 1 jump left.
    }

   
    void Update()
    {
        Inputs();
        CheckMoveDirection();
        Animations();
        CheckJump();
    }

    private void FixedUpdate()
    {
        Movement();
        CheckSurroundings();
    }

    private void CheckMoveDirection()
    {
        if (isFacingRight && moveDirection < 0)
        {
            Flip();
        }

        if (!isFacingRight && moveDirection > 0)
        {
            Flip();
        }
        if (Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }
    private void CheckSurroundings() //is for interacting with surrounding sth. (ex. Ground)
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); //Checks if a Collider falls within a circular area.
    }
    

    private void Inputs() //all the inputs from the player
    {
        moveDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if ((Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) && inventory[0] != null)
        {
            inventory[0].Use();
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) && inventory[1] != null)
        {
            inventory[1].Use();
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) && inventory[2] != null)
        {
            inventory[2].Use();
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)) && inventory[3] != null)
        {
            inventory[3].Use();
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5)) && inventory[4] != null)
        {
            inventory[4].Use();
        }
        else if ((Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6)) && inventory[5] != null)
        {
            inventory[5].Use();
        }
    }

    private void CheckJump() //Prevents the character from jumping infinitely.
    {
        if (isGrounded && rb.velocity.y <= 0.01f) //If the character interact with the ground or sth.
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }

    private void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
        
    }

    private void Movement()
    {
        rb.velocity = new Vector2(moveSpeed * moveDirection, rb.velocity.y); //In Rigidbody2D you should freeze the z rotation in constraints.
    }

    private void Animations()
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded",isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);

    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f,180.0f,0.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    public void AddInventory(GameObject gameObj, Item item)
    {
        foreach (var i in inventory)
        {
            if (!i.isEmpty) continue;
            //i.itemImage.gameObject.GetComponent<Transform>().position
            
            /*Sequence animationSequence = DOTween.Sequence();
            animationSequence.Append(gameObj.transform.DOMove(i.itemImage.gameObject.transform.position, 4f)).
                SetEase(Ease.OutSine)
                .OnComplete(() => {
                    Destroy(gameObj);
                    i.Set(item);
                });*/
            break;
        }
    }
}
