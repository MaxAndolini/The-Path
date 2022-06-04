using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
    }
}