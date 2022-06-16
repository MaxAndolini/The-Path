using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float length, startpos;

    public float parallaxEffect;
    private GameObject _gamecam;

    private void Start()
    {
        startpos = transform.position.x;
        _gamecam = GameObject.FindWithTag("PlayerCamera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        var temp = _gamecam.transform.position.x * (1 - parallaxEffect);
        var dist = _gamecam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}