using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject butter;
    public GameObject player;
    [SerializeField] private Transform spawnPlayer;
    [SerializeField] private Transform spawnButter;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(player, spawnPlayer.position, spawnPlayer.rotation);
        //Instantiate(butter, spawnButter.position, spawnButter.rotation);
    }
}
