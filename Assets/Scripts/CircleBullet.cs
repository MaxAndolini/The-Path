using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : MonoBehaviour
{
    
    private Rigidbody2D rb;
    public Vector2 direction = new Vector2(-5,0);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = direction;
        Destroy(gameObject,5.5f);
    }
}
