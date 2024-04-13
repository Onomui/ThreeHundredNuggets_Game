using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public Transform[] path;
    [SerializeField]
    private float speed;
    private int curPathPoint = 1;
    private bool isStopped = false;
    void Start()
    {
        transform.position = path[0].position;

    }

    
    void Update()
    {
        if (!isStopped)
        {
            MoveOnPath();
        }
    }

    private void MoveOnPath() 
    {
        if (curPathPoint == path.Length)
            isStopped = true;
        else
        { 
            transform.position = Vector2.MoveTowards(transform.position, path[curPathPoint].position, speed * Time.deltaTime);
            if (transform.position == path[curPathPoint].position)
                curPathPoint++;
        }
    }

}
