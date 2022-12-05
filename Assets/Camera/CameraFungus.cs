using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFungus : MonoBehaviour
{
    CinemachineVirtualCamera cinemachine;

    Vector3 pos;
    Quaternion rot;
    void Start()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
        pos = transform.position;
        rot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
