using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 마우스를 드래그하여 화면을 이동시킨다.
public class MoveBackgroundAsMouse : MonoBehaviour
{
    public Camera CAM;

    float mouseX, mouseY;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            CAM.transform.position += new Vector3(-mouseX * 0.9f,0,-mouseY * 0.9f);
        }
    }
}
