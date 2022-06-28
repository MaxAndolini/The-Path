using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTarget : MonoBehaviour
{
    public Transform startPoint;
    public GameObject sphere;
    private float betweenTime;
    public float startTimeBetween;
    void Start()
    {
        betweenTime = startTimeBetween;
    }

    // Update is called once per frame
    void Update()
    {
        if (betweenTime <= 0)
        {
            Instantiate(sphere, startPoint.position, startPoint.rotation);
            betweenTime = startTimeBetween;
        }
        else
        {
            betweenTime -= Time.deltaTime;
        }
    }
}
