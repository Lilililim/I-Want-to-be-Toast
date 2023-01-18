using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToasterCollision : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    // Start is called before the first frame update
    private void Start()
    {
        if(winScreen != null)
        {
            Debug.Log("nemu winscreen");
        }
        else
        {
            Debug.Log("ga nemu winscreen");
        }
    }
}//Class
