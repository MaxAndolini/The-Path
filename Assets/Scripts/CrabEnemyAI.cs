using System.Collections;
using System.Collections.Generic;
using Pathfinding.Examples;
using UnityEngine;

public class CrabEnemyAI : MonoBehaviour
{
    public float moveSpeed;
    public GameObject[] wayPoints;

    private int nextPoint = 1;
    private float distToPoint; //store the remaining distance between player and nextpoint

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        distToPoint = Vector2.Distance(transform.position, wayPoints[nextPoint].transform.position);

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextPoint].transform.position,
            moveSpeed * Time.deltaTime);

        if (distToPoint < 0.2f)
        {
            Turn();
        }
    }

    void Turn()
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z += wayPoints[nextPoint].transform.eulerAngles.z;
        transform.eulerAngles = currentRotation;

        ChooseNextPoint();
    }
    void ChooseNextPoint()
    {
        nextPoint++;

        if (nextPoint == wayPoints.Length)
        {
            nextPoint = 0;
        }

    }
   
}


