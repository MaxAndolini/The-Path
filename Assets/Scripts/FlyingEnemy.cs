using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FlyingEnemy : MonoBehaviour
{
    public float speed;
    private GameObject player;
    
    public bool follow = false;
    public Transform startPoint;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        if (follow == true)
        {
            Follow();
        }
        else
        {
            GoSTartPoint();
        }
        
        Flip();
        
    }

    private void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void GoSTartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);
    }

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
    }
}
