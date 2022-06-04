using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float length, startpos;
    private GameObject _gamecam;

    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        _gamecam = GameObject.Find("Player Camera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (_gamecam.transform.position.x * (1 - parallaxEffect));
        float dist = (_gamecam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}