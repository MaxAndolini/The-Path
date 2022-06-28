using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHit : MonoBehaviour
{
    public GameObject bullet;

    private float fireRate;
    private float nextFire;
    
    void Start()
    {
        fireRate = 1f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
