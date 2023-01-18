using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    #region Variables
    public float speed;
    public int startingPoint;
    public Transform[] points;
    private int i;
    public bool openGate = false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (openGate)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
            {
                openGate = false;
            }
        }
        
    }

    public void OpenGate()
    {
        Debug.Log("opened");
        openGate = true;
    }
}
