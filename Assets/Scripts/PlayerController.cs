using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //NOTES FROM INTERFACE:
    //In materials folder I use Physics Material 2D to prevent the character from Sticking to the wall. I reduce the friction to 0.

    [Header("Character")] private float moveDirection; // which direction player move (1,0,-1)

    private int amountOfJumpsLeft;
    private int facingDirection = 1;

    private bool isFacingRight = true;
    private bool isRunning;
    private bool isGrounded;
    private bool canJump;
    private bool isCrouching = false;
    private bool isSliding = false;

    [Header("Wall Sliding")] 
    private bool isTouchingFront;
    public Transform frontCheck;
    private bool wallSliding;
    public float wallSlidingSpeed;

 
    

    private GameObject player;
    private Rigidbody2D rb;
    private Animator anim;

    public int amountOfJumps = 1;

    public float moveSpeed = 15.0f;
    public float jumpForce = 16.0f; //In rigidbody2d, we changed gravity scale with 4 for better jump experience.
    public float groundCheckRadius;
    public float slideSpeed = 1000f;
    public float maxSlideTime = 1.5f;
    public float trampolinSpeed = 35.0f;
    
    
    public Transform groundCheck;
    

    public BoxCollider2D regularColl;
    public BoxCollider2D crouchColl;
    public BoxCollider2D slideColl;

    public LayerMask whatIsGround; //using this, we can assign layers to the things we want.

    
    [Space] [Header("Hearth")] public int currentHearth = 5;
    public GameObject[] hearth;

    [Space] [Header("Gold")] public Text goldText;
    public int gold;

    [Space] [Header("Inventory")] public Slot[] inventory;

    
    void Start()
    {
        player = gameObject.transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps; //we should equalize first. Because character does not jump yet. So if the character has the 1 jump, than 1 jump left.
        
        goldText.text = gold.ToString();
        SetHealth(currentHearth);
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
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, groundCheckRadius, whatIsGround);
    }

    private void Inputs() //all the inputs from the player
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        
        //WallSlide
        if (isTouchingFront == true && isGrounded == false && moveDirection != 0)
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
        
        
        
       




        //FOR JUMP
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        //FOR INVENTORY
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
    }
    

    private void Slide()
    {
        isSliding = true;
        anim.SetBool("isSlide", true);

        regularColl.enabled = false;
        slideColl.enabled = true;


        if (!isFacingRight)
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

    private void CheckJump() //Prevents the character from jumping infinitely.
    {
        if ((isGrounded && rb.velocity.y <= 0.01f)) //If the character interact with the ground or sth.
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
        if (isGrounded)
        {
            rb.velocity = new Vector2(moveSpeed * moveDirection, rb.velocity.y); //In Rigidbody2D you should freeze the z rotation in constraints.
        }
    }

    private void Animations()
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", wallSliding);
    }

    private void Flip()
    {
        facingDirection *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
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

    private void OnTriggerEnter2D(Collider2D col)
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
            rb.velocity = Vector2.up * trampolinSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            //Hurt Animation
            var spriteRenderer = player.GetComponent<SpriteRenderer>();
            DOTween.Sequence()
                .Append(spriteRenderer.DOColor(Color.red, 0.05f))
                .Append(spriteRenderer.DOColor(Color.white, 0.7f));

            //Change Health
            currentHearth--;
            if (currentHearth >= 0) SetHealth(currentHearth);
            else GameOver();
        }
    }

    private void SetHealth(int h)
    {
        for (var i = 0; i < 5; i++)
        {
            hearth[i].SetActive(false);
        }

        for (var i = 0; i < h; i++)
        {
            hearth[i].SetActive(true);
        }

        currentHearth = h;
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}