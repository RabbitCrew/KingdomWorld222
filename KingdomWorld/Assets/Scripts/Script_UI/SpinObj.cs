using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObj : MonoBehaviour
{
    public float RotateSpeed = 100f; //ȸ�� �ӵ�
    public GameObject SpinTarget; //ȸ����ų ���

    private void Update()
    {
        Spinning();
    }

    public void Spinning()
    {
        SpinTarget.transform.Rotate(new Vector3(0, 0, RotateSpeed * Time.deltaTime)); 
        //�����Ͻ� ������ ȸ�� //����Ͻ� ���� ȸ��
    }
}
