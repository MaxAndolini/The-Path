using UnityEngine;

public class SpawnTarget : MonoBehaviour
{
    public Transform startPoint;
    public GameObject sphere;
    public float startTimeBetween;
    private float betweenTime;

    private void Start()
    {
        betweenTime = startTimeBetween;
    }

    private void Update()
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