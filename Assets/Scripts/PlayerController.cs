using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //NOTES FROM INTERFACE:
    //In materials folder I use Physics Material 2D to prevent the character from Sticking to the wall. I reduce the friction to 0.
    
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
}
