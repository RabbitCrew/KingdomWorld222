using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : NPCScrip
{
    public GameObject TestBuildingTransform;
    
    public string NPCClass;
    public delegate void MyDelegate(Transform start, Transform end);

    public MyDelegate myDelegate = null;

    private bool reSetPathTrigger = false;//update���� Astar�� �۵����� �ʰ� ���ִ� bool��

    private void Start()
    {
        Grid = GameManager.instance.GetComponent<Setgrid>();
        BuildingNum = TestBuildingTransform;
        Speed = 3f;
        reSetPathTrigger = false;
    }
    void Update()
    {
        CargoClass();
    }

    //â������ 
    private bool WorkStart = false;
    void CargoClass()
    {

        //�ǹ��� �����Ǿ����� ��μ���
        if (NPCBUildTrigger)
        {
            ResetPath(this.transform, BuildingNum.transform);
            currentPathIndex = 0;
            NPCBUildTrigger = false;
        }
        if(BuildingNum != null)
        {
            //�������� �ٲ� �ѹ��� ��μ���
            if (GameManager.instance.isDaytime && !reSetPathTrigger)
            {
                ResetPath(this.transform, BuildingNum.transform);
                currentPathIndex = 0;
                reSetPathTrigger = true;
            }
            else if (!GameManager.instance.isDaytime && reSetPathTrigger)
            {
                ResetPath(this.transform, HouseTr);
                currentPathIndex = 0;
                reSetPathTrigger = false;
            }

            Transform building = null;
            if (GameManager.instance.isDaytime && !WorkStart)
            {
                Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
                foreach (Collider collider in colliders)
                {
                    building = collider.transform;
                    /*
                     if(collider.�ǹ������������ == colldier.�ǹ�������������ִ�ġ){
                        ResetPath(this.transform, collider.transform)
                        currentPathIndex = 0;
                        WorkStart = true;
                        break;
                    }*/
                }
            }else if (GameManager.instance.isDaytime && (this.transform.position == building.position))
            {
                /*�ǹ����� �ڿ�������*/
            }
            Move();
        }
        
    }

    private bool treeCuting = false;
    void WoodCutter()
    {
        bool workTrigger = false; //����ߴ��� üũ
        
        Transform tree = null;
        //�ǹ��� �����Ǿ����� ��μ���
        if (NPCBUildTrigger)
        {
            ResetPath(this.transform, BuildingNum.transform);
            NPCBUildTrigger = false;
        }
        //�������� �ٲ� �ѹ��� ��μ���
        if (GameManager.instance.isDaytime && !reSetPathTrigger && !workTrigger)
        {
            ResetPath(this.transform, BuildingNum.transform);
            reSetPathTrigger = true;
        }
        else if (!GameManager.instance.isDaytime && reSetPathTrigger)
        {
            ResetPath(this.transform, HouseTr);
            reSetPathTrigger = false;
        }
        if(this.transform.position == BuildingNum.transform.position)
        {
            workTrigger = true;
        }
        if (workTrigger)
        {
            if (!treeCuting)
            {
                Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("tree"))
                    {
                        tree = collider.transform;
                        ResetPath(this.transform, tree);
                        treeCuting = true;
                        break;
                    }
                }
            }
            if(this.transform.position == tree.position)
            {
                StartCoroutine(DestroyAfterDelay(3f, tree));
            }

        }
    }
    private IEnumerator DestroyAfterDelay(float delay, Transform tree)
    {
        yield return new WaitForSeconds(delay);
        Destroy(tree);
        treeCuting = true;
    }


}
