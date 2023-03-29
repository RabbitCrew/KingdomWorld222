using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObj : MonoBehaviour
{
    public float RotateSpeed = 100f; //회전 속도
    public GameObject SpinTarget; //회전시킬 대상

    private void Update()
    {
        Spinning();
    }

    public void Spinning()
    {
        SpinTarget.transform.Rotate(new Vector3(0, 0, RotateSpeed * Time.deltaTime)); 
        //음수일시 오른쪽 회전 //양수일시 왼쪽 회전
    }
}
