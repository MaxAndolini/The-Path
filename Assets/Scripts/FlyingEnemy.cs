using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed;

    public bool follow;
    public Transform startPoint;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void Update()
    {
        if (player != null)
        {
            if (follow)
                Follow();
            else
                GoSTartPoint();

            Flip();
        }
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
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}