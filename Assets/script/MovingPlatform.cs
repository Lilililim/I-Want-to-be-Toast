using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    #region Variables
    public float speed;
    public int startingPoint;
    public Transform[] points;

    private int i;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
    }
    private void Update()
    {

    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, points[i].position ) < 0.02f)
        {
            i++;
            if(i == points.Length)
            {
                i = 0;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y > transform.position.y)
        {
            collision.transform.SetParent(transform);
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
