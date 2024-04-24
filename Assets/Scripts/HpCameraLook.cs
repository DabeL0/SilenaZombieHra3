using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpCameraLook : MonoBehaviour
{
    static Camera cam;

    private void Start()
    {
        if (cam == null) cam = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform);
    }
}
