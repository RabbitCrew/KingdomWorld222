using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : NPCScrip
{
    public string NPCClass;

    public delegate void MyDelegate(Transform start, Transform end);

    public MyDelegate myDelegate = null;

    private bool reSetPathTrigger = false;

    private void Start()
    {

    }
    void Update()
    {
        CargoClass();
    }

    //â������ 
    void CargoClass()
    {
        //�ǹ��� �����Ǿ����� ��μ���
        if (NPCTrigger)
        {
            ResetPath(this.transform, BuildingNum.transform);
            NPCTrigger = false;
        }
        //�������� �ٲ� �ѹ��� ��μ���
        if (GameManager.instance.isDaytime && !reSetPathTrigger) 
        {
            ResetPath(this.transform, BuildingNum.transform);
            reSetPathTrigger = true;
        }
        else if (!GameManager.instance.isDaytime && reSetPathTrigger)
        {
            ResetPath(this.transform, HouseTr);
            reSetPathTrigger = false;
        }

        if (GameManager.instance.isDaytime)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
            foreach (Collider collider in colliders)
            {
                /*
                 if(collider.�ǹ������������ == colldier.�ǹ�������������ִ�ġ){
                    ResetPath(this.transform, collider.transform)
                    break;
                }
                 */
            }
        }
        Move();
    }

}
