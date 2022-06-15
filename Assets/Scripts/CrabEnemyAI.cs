using UnityEngine;

public class CrabEnemyAI : MonoBehaviour
{
    public float moveSpeed;
    public GameObject[] wayPoints;
    private float distToPoint; //store the remaining distance between player and nextpoint

    private int nextPoint = 1;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        distToPoint = Vector2.Distance(transform.position, wayPoints[nextPoint].transform.position);

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextPoint].transform.position,
            moveSpeed * Time.deltaTime);

        if (distToPoint < 0.2f) Turn();
    }

    private void Turn()
    {
        var currentRotation = transform.eulerAngles;
        currentRotation.z += wayPoints[nextPoint].transform.eulerAngles.z;
        transform.eulerAngles = currentRotation;

        ChooseNextPoint();
    }

    private void ChooseNextPoint()
    {
        nextPoint++;

        if (nextPoint == wayPoints.Length) nextPoint = 0;
    }
}