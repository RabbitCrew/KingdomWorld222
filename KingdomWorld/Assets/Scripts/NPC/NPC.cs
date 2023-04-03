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

    //창고지기 
    void CargoClass()
    {
        //건물에 배정되었을때 경로수정
        if (NPCTrigger)
        {
            ResetPath(this.transform, BuildingNum.transform);
            NPCTrigger = false;
        }
        //낮과밤이 바뀔때 한번만 경로수정
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
                 if(collider.건물내부저장공간 == colldier.건물내부저장공간최대치){
                    ResetPath(this.transform, collider.transform)
                    break;
                }
                 */
            }
        }
        Move();
    }

}
