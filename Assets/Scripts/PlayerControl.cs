using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [Header("Wall Sliding")] 
    private bool isTouchingFront;
    public Transform frontCheck;
    private bool wallSliding;
    public float wallSlidingSpeed;

    [Header("Wall Jumping")] private bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;
    
    public float speed;
    public float jumpForce;
    public float groundCheckRadius;
    private float moveInput;// which direction player move (1,0,-1)
   

    private Rigidbody2D rb;

    private bool facingRight = true;
    private bool isGrounded;
    private bool isRunning;
    private bool isCrouching = false;
    private bool isSliding = false;

    public Transform groundCheck;
    public float checkRadius;
    
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpValue;
    private int facingDirection = 1;
    
    public BoxCollider2D regularColl;
    public BoxCollider2D crouchColl;
    public BoxCollider2D slideColl;
    
    public float slideSpeed = 1000f;
    public float maxSlideTime = 1.5f;
    
    private Animator anim;
    
    [Space] [Header("Gold")] public Text goldText;
    public int gold;
    
    [Space] [Header("Trampoline")] public float trampolineSpeed = 35.0f;
    
    void Start()
    {
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        goldText.text = gold.ToString();
    }
    
    void Update()
    {
        if (!Menu.gamePause)
        {
            if (isGrounded)
            {
                extraJumps = extraJumpValue;
            }

            if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
            {
                rb.velocity = Vector2.up * jumpForce;
            }

            //FOR SLIDING
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Slide();
            }

            //FOR CROUCH
            if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && isGrounded)
            {
                Crouch();
            }
            else if ((Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) && isGrounded)
            {
                isCrouching = false;
                anim.Play("Idle");
                anim.SetBool("isCrouching", false);
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                regularColl.enabled = true;
                crouchColl.enabled = false;
            }

            //WallSlide
            if (isTouchingFront == true && isGrounded == false && moveInput != 0)
            {
                wallSliding = true;
            }
            else
            {
                wallSliding = false;
            }

            if (wallSliding)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            }

            //WallJump

            if (Input.GetKeyDown(KeyCode.Space) && wallSliding == true)
            {
                wallJumping = true;
                Invoke("SetWallJumpingToFalse", wallJumpTime);
            }

            if (wallJumping == true)
            {
                rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
            }

            Animations();
        }
    }

    private void FixedUpdate()
    {
        if (!Menu.gamePause)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
            isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, groundCheckRadius, whatIsGround);

            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);


            if (facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if (facingRight == true && moveInput < 0)
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
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!Menu.gamePause)
        {
            if (col.CompareTag("Gold"))
            {
                var gold = GameObject.Find("GoldImage");
                var animGameObject = Instantiate(gold, Camera.main.WorldToScreenPoint(transform.position),
                    gold.transform.rotation,
                    gold.transform);
                Destroy(col.gameObject);
                animGameObject.transform.DOMove(gold.transform.position, 1.5f).SetEase(Ease.OutSine)
                    .OnComplete(() =>
                    {
                        Destroy(animGameObject);
                        AddGold();
                    });
            }
            else if (col.CompareTag("Trampoline"))
            {
                rb.velocity = Vector2.up * trampolineSpeed;
            }
        }
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    private void Slide()
    {
        isSliding = true;
        anim.SetBool("isSlide", true);

        regularColl.enabled = false;
        slideColl.enabled = true;


        if (!facingRight)
        {
            rb.AddForce(Vector2.right * slideSpeed);
        }
        else
        {
            rb.AddForce(Vector2.left * slideSpeed);
        }

        StartCoroutine("stopSlide");
    }

    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(maxSlideTime);
        anim.Play("Idle");
        anim.SetBool("isSlide", false);
        regularColl.enabled = true;
        slideColl.enabled = false;
        isSliding = false;
    }

    private void Crouch()
    {
        isCrouching = true;
        anim.SetBool("isCrouching", true);

        regularColl.enabled = false;
        crouchColl.enabled = true;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }

    void Flip()
    {
        facingDirection *= -1;
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
    
    private void Animations()
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    
    public void AddGold()
    {
        gold++;
        goldText.text = gold.ToString();
    }
}
