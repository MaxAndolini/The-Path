using UnityEngine;

public class FlyingEnemyControl : MonoBehaviour
{
    public FlyingEnemy[] enemies;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            foreach (var enemy in enemies)
                enemy.follow = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            foreach (var enemy in enemies)
                enemy.follow = false;
    }
}