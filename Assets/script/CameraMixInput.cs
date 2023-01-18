using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraMixInput : MonoBehaviour
{
    public CinemachineMixingCamera mixer;
    public float totalTransitionTime = 1f;

    private int totalCameras;
    private int selectedCamera = 0;
    private int lastCamera = 0;
    private float elapsedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        totalCameras = mixer.ChildCameras.Length;
        elapsedTime = 0;
        for (int a = 0; a < totalCameras; a++)
        {
            float dest = (a == selectedCamera) ? 1f : 0f;
            mixer.SetWeight(a, dest);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime < totalTransitionTime)
        {
            for (int a = 0; a < totalCameras; a++)
            {
                float dest = (a == selectedCamera) ? 1f : 0f;
                float start = mixer.GetWeight(a);
                float toLerp = Mathf.Lerp(start, dest, elapsedTime);
                mixer.SetWeight(a, toLerp);
            }
            elapsedTime += Time.deltaTime;
        }
        /*if (Input.GetKey(KeyCode.W))
        {
            selectedCamera = 1;
        } 
        else if (Input.GetKey(KeyCode.S))
        {
            selectedCamera = 1;
        }
        else
        {
            selectedCamera = 0;
        }*/
        
        if (lastCamera != selectedCamera)
        {
            elapsedTime = 0;
            lastCamera = selectedCamera;
        }
    }
    public void KeyLook(InputAction.CallbackContext context)
    {
        Debug.Log("liat atas/bawah");
    }
}
