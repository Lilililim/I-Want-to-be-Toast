using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollisions : MonoBehaviour
{
    private PlayerMovementNew playerMovement;
    private PlayerManager playerManager;
    private GameObject currentOneWayPlatform;

    [SerializeField] private BoxCollider2D playerCollider;
    void Start()
    {
        PlayerMovementNew playerScript = GetComponent<PlayerMovementNew>();
        playerMovement = playerScript;
        playerManager = GetComponent<PlayerManager>();
    }

    public void DropDown(InputAction.CallbackContext context) //for dropiing down platform
    {
        if (currentOneWayPlatform != null)
        {
            Debug.Log("drop down called");
            StartCoroutine(DisableCollisions());
        }
    }
    private void OnTriggerEnter2D(Collider2D other) //Kena butter
    {
        if (other.gameObject.CompareTag("Butter"))
        {
            if (!playerMovement.buttered)
            {
            playerMovement.AddButter();
                playerManager.PlayAudio("butterSFX");
            }
        }
        if (other.gameObject.CompareTag("KillFloor"))
        {
            if (playerMovement.buttered)
            {
                playerMovement.NoButter();
            }
            playerMovement.Spawn();
        }
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            if(playerMovement.currentCheckpoint != other.transform.position)
            {
                playerMovement.currentCheckpoint = other.transform.position;
                playerManager.PlayAudio("checkpointSFX");
            }
        }
        if (other.gameObject.CompareTag("Toaster"))
        {
            if (playerMovement.buttered)
            {
                playerManager.PlayAudio("winSFX");
                FindObjectOfType<GameManager>().CompleteLevel();
                playerMovement.deceleration = 20f;
                playerMovement.canMove = false;
            }
            else
            {
                Debug.Log("Not buttered yet");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        /*if (other.gameObject.CompareTag("Jello"))
        {
            playerMovement.OutsideJello();
        }*/
        /*if (other.gameObject.CompareTag("Switch"))
        {
            playerMovement.activateSwitch = false;
        }*/
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) //if player hits enemy
        {
            if (playerMovement.buttered)
            {
                Debug.Log("no more butter");
                playerMovement.NoButter();
            }
            playerManager.PlayAudio("duckSFX");
            playerMovement.Spawn();
        }

        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
            playerMovement.rb.interpolation = RigidbodyInterpolation2D.None;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            currentOneWayPlatform = null;
            playerMovement.rb.interpolation = RigidbodyInterpolation2D.Extrapolate;
        }
    }

    private IEnumerator DisableCollisions()
    {
        CompositeCollider2D compositePlatformCollider = currentOneWayPlatform.GetComponent<CompositeCollider2D>();
        BoxCollider2D boxPlatformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();
        if(compositePlatformCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, compositePlatformCollider);
            yield return new WaitForSeconds(0.3f);
            Physics2D.IgnoreCollision(playerCollider, compositePlatformCollider, false);
        } 
        else if (boxPlatformCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, boxPlatformCollider);
            yield return new WaitForSeconds(0.3f);
            Physics2D.IgnoreCollision(playerCollider, boxPlatformCollider, false);
        }
        
    }
}
