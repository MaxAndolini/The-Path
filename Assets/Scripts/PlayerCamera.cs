using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindWithTag("Player").transform;
    }
}