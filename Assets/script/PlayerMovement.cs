using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("public variables")]
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public PhysicsMaterial2D butterPhysics;
    public PhysicsMaterial2D noButterPhysics;
    public BoxCollider2D colliderPlayer;
    public Transform spawnPoint;
    private Collider2D colliderObj;
    private Vector2 respawnPoint;

    private float horizontal; //input pemain, kiri atau kanan
    private bool isFacingRight = true;
    
    #region player stat default
    public float speedDefault = 9f;
    private float jumpingPower = 11f;
    //private static float defaultFriction = 0.2f;
    private static float accelerationDefault = 4f;
    private static float decelerationDefault = 9f;
    private bool butteredDefault = false;
    private float doubleJumpPower = 9f;
    private bool doubleJump;
    private bool active = true;
    private Vector2 spawnCoords;
    #endregion

    #region stat dinamik player
    [Header("Stas Player")]
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float speed;
    [SerializeField] public bool buttered;
    #endregion

    #region butter stats
    private float butterSpeed = 10f;
    private float butterAcceleration = 5f;
    private float butterDeceleration = 1.5f;
    #endregion

    private void Start()
    {
        NoButter();
        //colliderPlayer.sharedMaterial.friction = defaultFriction;
        /*acceleration = accelerationDefault;
        deceleration = decelerationDefault;
        speed = speedDefault;
        buttered = butteredDefault;*/
        //Vector2 spawnCoords = spawnPoint.transform.position;
        transform.localPosition = spawnPoint.localPosition;
        colliderObj = GetComponent<Collider2D>();
        //SetRespawnLocation((Vector2)transform.position);
    }
    private void FixedUpdate()
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
    // Update is called once per frame
    void Update()
    {
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
    }

    private void OnTriggerEnter2D(Collider2D other) //dapet butter stat berubah, butter hancur
    {
        if (other.gameObject.CompareTag("Butter") && buttered == false)
        {
            AddButter();
            //Destroy(other.gameObject);
            /*Debug.Log("buttered terpanggil" + "\nacceleration: " + acceleration + "\ndeceleration: " + deceleration + "\nspeed: " + speed + "\nbuttered: " + buttered);
            acceleration = butterAcceleration;
            deceleration = butterDeceleration;
            speed = butterSpeed;
            buttered = true;
            Debug.Log("buttered selesai" + "\nacceleration: " + acceleration + "\ndeceleration: " + deceleration + "\nspeed: " + speed + "\nbuttered: " + buttered);*/
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed /*&& IsGrounded()*/)
        {
            if (IsGrounded() || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, doubleJump ? doubleJumpPower : jumpingPower);
                doubleJump = !doubleJump;
            }
        }
        //reset double jump
        if (!context.performed && IsGrounded())
        {
            doubleJump = false;
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localscale = transform.localScale;
        localscale.x *= -1f;
        transform.localScale = localscale;
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void SetRespawnLocation(Vector2 position)
    {
        respawnPoint = position;
    }
    public void RespawnPlayer()
    {
        /*Debug.Log("Respawn Player kepanggil" + "\nacceleration: " + acceleration + "\ndeceleration: " + deceleration + "\nspeed: " +speed + "\nbuttered: " + buttered);
        acceleration = accelerationDefault;
        deceleration = decelerationDefault;
        speed = speedDefault;
        buttered = butteredDefault;
        Debug.Log("Respawn Player berubah" + "\nacceleration: " + acceleration + "\ndeceleration: " + deceleration + "\nspeed: " + speed + "\nbuttered: " + buttered);*/
        NoButter();
    }

    public void AddButter()
    {
        buttered = true;
        acceleration = butterAcceleration;
        deceleration = butterDeceleration;
        speed = butterSpeed;
        if (buttered)
        {
            Debug.Log("buttered is true");
        } else
        {
            Debug.Log("buttered is false");
        }
    }

    public void NoButter()
    {
        Debug.Log("Respawn Player kepanggil" + "\nacceleration: " + acceleration + "\ndeceleration: " + deceleration + "\nspeed: " + speed + "\nbuttered: " + buttered);
        acceleration = accelerationDefault;
        deceleration = decelerationDefault;
        speed = speedDefault;
        buttered = butteredDefault;
        Debug.Log("Respawn Player berubah" + "\nacceleration: " + acceleration + "\ndeceleration: " + deceleration + "\nspeed: " + speed + "\nbuttered: " + buttered);
    }
    /*public void Die()
    {
        active = false;
        colliderObj.enabled = false;
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        transform.position = respawnPoint;
        active = true;
        colliderObj.enabled = true;
    }*/
}
