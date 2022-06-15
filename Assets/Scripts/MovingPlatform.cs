using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
   
    public Transform pos1, pos2;
    public float platformSpeed;

    Vector3 nextPos;

    
    void Start()
    {
        nextPos = pos1.position;    
    }

    void FixedUpdate()
    {
        if (transform.position == pos1.position)
            nextPos = pos2.position;
        if (transform.position == pos2.position)
            nextPos = pos1.position;

        transform.position = Vector2.MoveTowards(transform.position, nextPos, platformSpeed * Time.deltaTime);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
    
}
