using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void Start()
    {
        GameObject.FindWithTag("Player").transform.position = gameObject.transform.position;
    }
}