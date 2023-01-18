using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovementNew : MonoBehaviour
{
    #region Player Variables
    [Header("Player Object variables")]
    [SerializeField] private GameObject spawn;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public Transform player;
    [SerializeField] public BoxCollider2D colliderPlayer;
    [SerializeField] public Animator anim;
    [HideInInspector] private Collider2D colliderObj;
    [HideInInspector] private bool isGround;
    [HideInInspector] private bool stopGroundCheck;
    #endregion

    #region Layers
    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask toasterLayer;
    public LayerMask platformLayer;
    #endregion

    [HideInInspector] public float horizontal; //input pemain, kiri atau kanan
    [HideInInspector] private bool isFacingRight = true;
    [HideInInspector] private bool isOnPlatform;
    [HideInInspector] private Rigidbody2D platformRBody;


    #region stat dinamik player
    [Header("Stat Player")]
    [HideInInspector] public float acceleration;
    [HideInInspector] public float deceleration;
    [HideInInspector] private float speed;
    [HideInInspector] private float jumpingPower;
    [HideInInspector] public bool buttered;
    [HideInInspector] public Vector3 currentCheckpoint;
    [HideInInspector] public bool canMove = true;
    private bool isJumping;
    #endregion

    #region player stat default
    [Header("Player Stat Default")]
    [HideInInspector] public float speedDefault = 9f;
    [HideInInspector] private float jumpingPowerDefault = 11f;
    [HideInInspector] private float accelerationDefault = 4f;
    [HideInInspector] private float decelerationDefault = 9f;
    [HideInInspector] private float doubleJumpPower = 9f;
    private bool doubleJump = true;
    #endregion

    #region butter stats
    [Header("Player Stat Buttered")]
    [HideInInspector] private float butterSpeed = 14f;
    [HideInInspector] private float butterAcceleration = 2.5f;
    [HideInInspector] private float butterDeceleration = 0.5f;
    [HideInInspector] private float butterJumpingPower = 13f;
    [HideInInspector] private float wallSlideSpeedButtered = 5;
    #endregion

    #region switch
    [HideInInspector] public bool activateSwitch;
    private bool butteredDefault = false;
    private bool active = true;

    private Vector2 spawnCoords;
    #endregion
    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        try
        {
            spawn = GameObject.FindWithTag("PlayerSpawn");
        }
        catch (System.NullReferenceException)
        {
            Debug.Log("Player Spawn belum ditambahin");
        }
        NoButter();
        Spawn();
        colliderObj = GetComponent<Collider2D>();
    }
    private void FixedUpdate()
    {
        if (!/*IsGrounded()*/isGround && !doubleJump)
        {
            #region Run no double jump
            //hitung arah jalan dan kecepetan
            float targetSpeed = horizontal * (speed/1.25f);
            //untuk ketika ganti arah jalan pelan dulu baru normal
            float speedDif = targetSpeed - rb.velocity.x;
            //untuk mengatur akselerasi dan kecepatan berhenti akselerasi : deselerasi
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
            //menggunakan accelRate untuk perbedaan kecepatan, lalu dikali dengan arah gerakan
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, 1.2f) * Mathf.Sign(speedDif);
            //tambahkan force ke rigidbody, vector2.right biar berlaku di x axis
            rb.AddForce(movement * Vector2.right);
            #endregion
        }
        else
        {
            #region Run
            //hitung arah jalan dan kecepetan
            float targetSpeed = horizontal * speed;
            //untuk ketika ganti arah jalan pelan dulu baru normal
            float speedDif = targetSpeed - rb.velocity.x;
            //untuk mengatur akselerasi dan kecepatan berhenti akselerasi : deselerasi
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
            //menggunakan accelRate untuk perbedaan kecepatan, lalu dikali dengan arah gerakan
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, 1.2f) * Mathf.Sign(speedDif);
            //tambahkan force ke rigidbody, vector2.right biar berlaku di x axis
            rb.AddForce(movement * Vector2.right);
            #endregion
        }
        #region One Way Platform
        if (isOnPlatform)
        {
            rb.velocity = rb.velocity + platformRBody.velocity;
        }
        #endregion
    }
    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        if (isGround && !stopGroundCheck)
        {
            isJumping = false;
            doubleJump = true;
            /*StartCoroutine(GroundCheckDelay());*/
            anim.SetBool("jump", false);
            anim.SetBool("double jump", false);
        }

        #region flip
        if (!active)
        {
            return;
        }
        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
        #endregion

    }

    public void Spawn()
    {
        if(currentCheckpoint.x == 0 && currentCheckpoint.y == 0)
        {
            transform.localPosition = spawn.transform.localPosition;
        } 
        else
        {
            transform.localPosition = currentCheckpoint;
        }
    } 

    private void Flip()
    {
        if (canMove)
        {
            isFacingRight = !isFacingRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(groundCheck.position, new Vector3(1, 0.01f, 1));
    }

    #region Butter, Peanut
    public void AddButter()
    {
        if (!buttered)
        {
            buttered = true;
            anim.SetBool("buttered", true);
            acceleration = butterAcceleration;
            deceleration = butterDeceleration;
            speed = butterSpeed;
            jumpingPower = butterJumpingPower;
        }
    }

    public void NoButter()
    {
        anim.SetBool("buttered", false);
        anim.SetBool("butter transition", true);
        StartCoroutine(InputDelay());
        acceleration = accelerationDefault;
        deceleration = decelerationDefault;
        speed = speedDefault;
        buttered = butteredDefault;
        jumpingPower = jumpingPowerDefault;
        anim.SetBool("butter transition", false);
    }

    #endregion

    #region Input Actions
    public void Move(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            horizontal = context.ReadValue<Vector2>().x;
            anim.SetFloat("horizontal", Mathf.Abs(context.ReadValue<Vector2>().x));
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            #region Jump and Double Jump
            if (isGround)
            {
                StartCoroutine(GroundCheckDelay());
                Debug.Log("is grounded before jump?" + isGround);
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                anim.SetBool("jump", true);
                playerManager.PlayAudio("jumpSFX");
                isGround = false;
                isJumping = true;
               
            }
            else if (!isGround && doubleJump)
            {
                Debug.Log("double jumped");
                StartCoroutine(GroundCheckDelay());
                isGround = false;
                isJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, doubleJumpPower);
                playerManager.PlayAudio("jumpSFX");
                anim.SetBool("jump", true);
                anim.SetBool("double jump", true);
                doubleJump = false;
            }
            #endregion
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        player.SetParent(null);
    }
    public void Slow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            deceleration = 3.5f;
        }
        if (context.canceled)
        {
            if (buttered)
            {
                deceleration = butterDeceleration;
            }
            else
            {
                deceleration = decelerationDefault;
            }
        }
    }
    #endregion

    private IEnumerator InputDelay()
    {
        canMove = false;
        horizontal = 0;
        anim.SetFloat("horizontal", horizontal);
        yield return new WaitForSeconds(0.2f);
        canMove = true;
    }

    private IEnumerator GroundCheckDelay()
    {
        stopGroundCheck = true;
        yield return new WaitForSeconds(0.2f);
        stopGroundCheck = false;

    }
}
