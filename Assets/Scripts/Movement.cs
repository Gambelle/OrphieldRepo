using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Movement : MonoBehaviour
{
    //Animation
    private static readonly int WALK_PROPERTY = Animator.StringToHash("Walk");
    private static readonly int IDLE_PROPERTY = Animator.StringToHash("Idle");
    private static readonly int JUMP_PROPERTY = Animator.StringToHash("Jump");
    private static readonly int FALL_PROPERTY = Animator.StringToHash("Fall");
    private static readonly int GLID_PROPERTY = Animator.StringToHash("Glide");
    private static readonly int CROUCH_PROPERTY = Animator.StringToHash("Crouch");
    [Header("Relations")]
    [SerializeField]
    private Animator animator = null;
    private bool isWalking = false;
    private bool isIdle = false;
    private bool isJumping = false;
    private bool isFalling = false;
    private bool isGliding = false;

    private SpriteRenderer sprRend;


    public Animator transition;
    public float transitionTime = 2f;


    //Movement
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private bool isOnBox;
    public LayerMask whatIsBox;

    private int extraJumps;
    public int extraJumpsValue;

    private Vector3 originalScale;
    private bool isCrouching;

    public float glideSpeeed;

    private bool isClimbable;
    private bool isClimbing;
    public float climbSpeed;

    public HealthBar healthBar;
    public float maxHealth = 100.0f;
    public float currentHealth;
    private Transform monsterPosition;
    private GameObject monster;

    public AudioClip groundWalkLoop;
    public AudioClip boxWalkLoop;
    public AudioClip jumpSound;
    AudioSource audioSrc;
    bool groundWalkAudio;
    bool cardboardBoxWalkAudio;
    bool jumpAudio;
    public Interact interactScript;
    public DeathZone deathZoneScript;

    void Start()
    {
        originalScale = transform.localScale;
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        isCrouching = false;

        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        monster = GameObject.Find("Monster");
        audioSrc = GetComponent<AudioSource>();
        groundWalkAudio = false;
        cardboardBoxWalkAudio = false;
    }

    void FixedUpdate()
    {
        whatIsGround = LayerMask.GetMask("Ground");
        whatIsBox = LayerMask.GetMask("Cardboard Box");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isOnBox = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsBox);

        moveInput = Input.GetAxis("Horizontal");
        if (Input.GetAxis("Shift") == 1f)
        {
            Run();
        }
        else if (Input.GetAxis("Shift") != 1f)
        {
            Walk();
        }

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * climbSpeed);
        }
        else if (!isClimbing)
        {
            rb.gravityScale = 2f;
        }

        // Normalize Animations
        if (isGliding)
        {
            isIdle = false;
            isWalking = false;
            isJumping = false;
            isFalling = false;
        }
        else if (isCrouching)
        {
            isJumping = false;
            isFalling = false;
            isGliding = false;
            if (rb.velocity.x != 0)
                isWalking = true;
            else
                isWalking = false;
        }
        else if (rb.velocity.y > 0.05)
        {
            isJumping = true;
            isWalking = false;
            isIdle = false;
            isFalling = false;
        }
        else if (rb.velocity.y < -0.05)
        {
            isFalling = true;
            isWalking = false;
            isIdle = false;
            isJumping = false;
        }
        else if (rb.velocity.x != 0)
        {
            isWalking = true;
            isIdle = false;
            isJumping = false;
            isFalling = false;

        }
        else
        {
            isIdle = true;
            isWalking = false;
            isJumping = false;
            isFalling = false;
        }
        animator.SetBool(GLID_PROPERTY, isGliding);
        animator.SetBool(JUMP_PROPERTY, isJumping);
        animator.SetBool(FALL_PROPERTY, isFalling);
        animator.SetBool(WALK_PROPERTY, isWalking);
        animator.SetBool(IDLE_PROPERTY, isIdle);
        animator.SetBool(CROUCH_PROPERTY, isCrouching);



    }

    void Update()
    {
        if(monster!=null)
        {
            monsterPosition = monster.transform;
            //Debug.Log(monsterPosition);
            //if (monsterPosition!=null)
            //{
            if (Vector2.Distance(transform.position, monsterPosition.position) < 3f)
            {
                Debug.Log("Here");
                TakeDamage(0.1f);
            }
            healthBar.setHealth(currentHealth -= 0.001f);
            //}

            if (currentHealth <= 0.1)
            {
                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
            }

        }

        if (isGrounded || isOnBox)
        {
            if (Math.Abs(rb.velocity.x) > 4)
                audioSrc.pitch = 1.1f;
            else
                audioSrc.pitch = 0.8f;
            if (!interactScript.doorOpenPlaying && !deathZoneScript.gunShotPlaying && !interactScript.pullingAudioPlaying)
            {
                if (isWalking && !groundWalkAudio && isGrounded)
                {
                    audioSrc.loop = true;
                    audioSrc.PlayOneShot(groundWalkLoop, 1.0f);
                    groundWalkAudio = true;
                }
                if (isWalking && !cardboardBoxWalkAudio && isOnBox)
                {
                    audioSrc.loop = true;
                    audioSrc.PlayOneShot(boxWalkLoop, 1.0f);
                    cardboardBoxWalkAudio = true;
                }
            }
            if (!isWalking)
            {
                audioSrc.Stop();
                if (groundWalkAudio)
                    groundWalkAudio = false;
                else if (cardboardBoxWalkAudio)
                    cardboardBoxWalkAudio = false;

            }
            extraJumps = extraJumpsValue;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            Debug.Log("jump sound");
            audioSrc.loop = false;
            audioSrc.Stop();
            audioSrc.clip = jumpSound;
            audioSrc.Play();
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetAxis("Crouch") == 1f && isCrouching == false)
        {
            isCrouching = true;
            Crouch();
        }
        else if (Input.GetAxis("Crouch") != 1f && isCrouching == true)
        {
            isCrouching = false;
            Stand();
        }

        if (Input.GetAxis("Glide") == 1f)
        {
            Glide();
            isGliding = true;
        }
        else
            isGliding = false;

        if (isClimbable && Input.GetAxis("Vertical") == 1f)
        {
            isClimbing = true;
        }

        
        //Debug.Log(monsterPosition);
        if (monsterPosition != null)
        {
            monsterPosition = monster.transform;
            if (Vector2.Distance(transform.position, monsterPosition.position) < 0.1f)
            {
                Debug.Log("Here");
                TakeDamage(0.1f);
            }
            healthBar.setHealth(currentHealth -= 0.0001f);
        }

    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ClimbableObj"))
        {
            Debug.Log("Rope detected");
            isClimbable = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ClimbableObj"))
        {
            isClimbable = false;
            isClimbing = false;
        }
    }

    void Walk()
    {
        rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
    }

    void Run()
    {
        rb.velocity = new Vector2(moveInput * runSpeed, rb.velocity.y);
    }

    void Stand()
    {
        transform.localScale = originalScale;
    }

    void Crouch()
    {
        Vector3 Scaler = transform.localScale;
        sprRend = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        sprRend.size += new Vector2(1f, 1f);
        Scaler.y *= .5f;
        transform.localScale = Scaler;
    }

    void Flip()
    {
        facingRight = !facingRight;
        if (transform.localEulerAngles.y != 180 && !facingRight)
            transform.Rotate(0f, 180f, 0f);
        else if (transform.localEulerAngles.y != 0 && facingRight)
            transform.Rotate(0f, -180f, 0f);
    }

    void Glide()
    {
        rb.velocity = Vector2.up * -(glideSpeeed);
    }

    public float getCurrentXPosition()
    {
        return transform.position.x;
    }

    void TakeDamage(float damage)
    {
        healthBar.setHealth(currentHealth -= damage);
    }
}



/*
float currentYAxis; //The ground

//Variables used for jump
bool singleJump;
float singleJumpCap;
// Start is called before the first frame update
void Start()
{
    currentYAxis = transform.position.y;
    singleJumpCap = currentYAxis + 5.0f;
}

// Update is called once per frame
void Update()
{
    //Fetch and initialize play position
    Vector2 position = transform.position;
    //Debug.Log("Position.y1: " + position.y);
    //Debug.Log("single jump: " + singleJump);
    //KeyBindings
    float horizontal = Input.GetAxis("Horizontal");
    float run = Input.GetAxis("Shift");
    float crouch = Input.GetAxis("Crouch");
    float jump = Input.GetAxis("Jump");

    //Update position

    //Horizontal movement (walk/run)
    if(run == 1)
        position.x = position.x + 6.0f * horizontal * Time.deltaTime;
    if(run == 0)
        position.x = position.x + 3.0f * horizontal * Time.deltaTime;

    //Jump
    if (jump == 1)
    {
        Debug.Log("test1");
        //singleJump = true;
        position.y = position.y + 4.1f;
    }

    //Crouch
    if (crouch == 1)
        position.y = currentYAxis - 0.5f;
    if (crouch == 0)
        position.y = currentYAxis;


    /*
    //Continue jump until cap is reached/surpassed
    if (singleJump == true && position.y < singleJumpCap)
    {
        Debug.Log("test2");
       // position.y = position.y + 1.0f * 0.5f;
    }
    //Begin falling
    else if (position.y >= singleJumpCap)
    {
        Debug.Log("test2");
        singleJump = false;
        position.y = position.y - 1.0f;
    }

    Debug.Log("Position.y2: " + position.y);

    transform.position = position;
    Debug.Log("Test End");
}
*/





