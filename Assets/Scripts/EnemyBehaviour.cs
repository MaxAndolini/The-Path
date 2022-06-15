using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Move right or left
        rb.velocity = new Vector2(IsFacingRight() ? moveSpeed : -moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Turn
        transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x) * 10), transform.localScale.y);
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x >= 0.01f;
    }
}