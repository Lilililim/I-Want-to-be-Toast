using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float offset = 10f;
    private Vector3 playerPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        if (player.transform.localScale.x > 0f)
        {
            playerPosition = new Vector3(player.transform.position.x + offset, player.transform.position.y, player.transform.position.z);
        } else
        {
            playerPosition = new Vector3(player.transform.position.x - offset, player.transform.position.y, player.transform.position.z);
        }
        transform.position = playerPosition;
    }
}
