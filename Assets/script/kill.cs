using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class kill : MonoBehaviour 
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform butterSpawnPoint;
    //public GameObject spawnerObj;
    public GameObject playerObj;
    public GameObject butterObj;
    private PlayerMovement playerScript;
    
    private void Start()
    {
        butterObj = GameObject.FindWithTag("Butter");
        playerScript = playerObj.GetComponent<PlayerMovement>();
    }

    //public int Respawn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerScript.buttered == true)
            {
                playerScript.RespawnPlayer();
            }
            other.transform.localPosition = spawnPoint.localPosition;
        }
    }
}
