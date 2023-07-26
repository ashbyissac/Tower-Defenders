using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBillboard : MonoBehaviour
{
    Transform cam;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void LateUpdate()
    {
        transform.LookAt(cam.forward + transform.position);
    }
}
