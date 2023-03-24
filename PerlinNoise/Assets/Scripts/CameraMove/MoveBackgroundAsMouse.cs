using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackgroundAsMouse : MonoBehaviour
{
    float mouseX, mouseY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton())
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }
}
