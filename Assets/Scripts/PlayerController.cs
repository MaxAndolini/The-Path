using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Transform frontCheck;
    public float wallSlidingSpeed;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    public float speed;
    public float jumpForce;
    public float groundCheckRadius;

    public Transform groundCheck;
    public float checkRadius;

    public LayerMask whatIsGround;
    public int extraJumpValue;

    public BoxCollider2D regularColl;
    public BoxCollider2D crouchColl;
    public BoxCollider2D slideColl;

    public float slideSpeed = 1000f;
    public float maxSlideTime = 1.5f;

    [Space] [Header("Gold")] public Text goldText;
    public int gold;

    [Space] [Header("Key")] public GameObject key;
    public bool keyStatus;

    private Animator anim;

    private Transform canvas;

    private int extraJumps;

    private bool facingRight = true;
    private bool isCrouching;
    private bool isGrounded;
    private bool isRunning;
    private bool isSliding;

    [Space] [Header("Wall Sliding")] private bool isTouchingFront;

    private float moveInput; // which direction player move (1,0,-1)

    private Rigidbody2D rb;
    private float slideTime;
    private float walkTime;

    [Space] [Header("Wall Jumping")] private bool wallJumping;
    private bool wallSliding;

    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void Reset()
    {
        HealthController.Instance.SetHealth(5.0f);
        gold = 0;
        goldText.text = gold.ToString();
        ChangeKey(false);
        InventoryController.Instance.ResetInventory();
    }

    private void Start()
    {
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canvas = GameObject.Find("Canvas").transform;
        goldText.text = gold.ToString();
    }

    private void Update()
    {
        if (!Menu.Instance.gamePause)
        {
            if (isGrounded) extraJumps = extraJumpValue;

            if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                SoundManager.Instance.PlayOneShot("Jump");
                extraJumps--;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
            }

            //FOR SLIDING
            if (Input.GetKeyDown(KeyCode.Z)) Slide();

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
            if (isTouchingFront && isGrounded == false && moveInput != 0)
                wallSliding = true;
            else
                wallSliding = false;

            if (wallSliding)
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));

            //WallJump
            if (Input.GetKeyDown(KeyCode.Space) && wallSliding)
            {
                wallJumping = true;
                anim.SetBool("isClimbing", true);
                Invoke("SetWallJumpingToFalse", wallJumpTime);
            }

            if (wallJumping) rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);

            walkTime += Time.deltaTime;

            if (walkTime > 0.5f && isGrounded && isRunning && !isCrouching && !isSliding && !wallJumping &&
                !wallSliding)
            {
                SoundManager.Instance.PlayOneShot("Walk");

                walkTime -= 0.5f;
            }

            slideTime += Time.deltaTime;

            if (slideTime > 0.6f && isGrounded && !isCrouching && isSliding && !wallJumping && !wallSliding)
            {
                SoundManager.Instance.PlayOneShot("Slide");

                slideTime -= 0.6f;
            }

            Animations();
        }
    }

    private void FixedUpdate()
    {
        if (!Menu.Instance.gamePause)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
            isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, groundCheckRadius, whatIsGround);

            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            if (!facingRight && moveInput > 0)
                Flip();
            else if (facingRight && moveInput < 0) Flip();

            if (Mathf.Abs(rb.velocity.x) >= 0.01f)
                isRunning = true;
            else
                isRunning = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!Menu.Instance.gamePause)
        {
            if (col.CompareTag("Gold"))
            {
                var goldGameObject = GameObject.Find("GoldImage");
                var animGameObject = Instantiate(goldGameObject, Camera.main.WorldToScreenPoint(transform.position),
                    goldGameObject.transform.rotation,
                    canvas);
                Destroy(col.gameObject);
                SoundManager.Instance.PlayOneShot("Gold");
                animGameObject.transform.DOMove(goldGameObject.transform.position, 1.5f).SetEase(Ease.OutSine)
                    .OnComplete(() =>
                    {
                        Destroy(animGameObject);
                        AddGold();
                    });
            }
            else if (col.CompareTag("Door"))
            {
                if (keyStatus)
                {
                    ChangeKey(false);
                    var active = SceneManager.GetActiveScene().buildIndex;
                    if (active == 4)
                    {
                        Menu.Instance.GameOver(true);
                    }
                    else
                    {
                        SoundManager.Instance.PlayOneShot("DoorOpen");
                        SceneManager.LoadScene(active + 1);
                    }
                }
                else
                {
                    SoundManager.Instance.PlayOneShot("DoorLocked");
                }
            }
        }
    }

    private void SetWallJumpingToFalse()
    {
        wallJumping = false;
        anim.SetBool("isClimbing", false);
    }

    private void Slide()
    {
        isSliding = true;
        anim.SetBool("isSlide", true);

        regularColl.enabled = false;
        slideColl.enabled = true;

        if (!facingRight)
            rb.AddForce(Vector2.right * slideSpeed);
        else
            rb.AddForce(Vector2.left * slideSpeed);

        StartCoroutine(StopSlide());
    }

    private IEnumerator StopSlide()
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

    private void Flip()
    {
        facingRight = !facingRight;
        var scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void Animations()
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public void AddGold()
    {
        gold++;
        goldText.text = gold.ToString();
    }

    public bool SpendGold(int h)
    {
        if (gold >= h)
        {
            gold -= h;
            goldText.text = gold.ToString();
            return true;
        }

        return false;
    }

    public void ChangeKey(bool h)
    {
        key.SetActive(h);
        keyStatus = h;
    }
}