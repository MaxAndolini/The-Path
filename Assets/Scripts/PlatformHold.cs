using UnityEngine;

public class PlatformHold : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform")) transform.parent = other.gameObject.transform;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform")) transform.parent = null;
    }
}