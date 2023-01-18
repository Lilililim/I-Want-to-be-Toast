using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] private GameObject checkpointWhite;
    [SerializeField] private GameObject checkpointGreen;

    void Start()
    {
        checkpointGreen.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (checkpointWhite.activeInHierarchy)
            {
                checkpointWhite.SetActive(false);
                checkpointGreen.SetActive(true);
            }
        }
    }
}
