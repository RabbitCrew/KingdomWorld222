using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : NPCScrip
{
    public GameObject TestBuildingTransform;
    
    public string NPCClass;
    public delegate void MyDelegate(Transform start, Transform end);

    public MyDelegate myDelegate = null;

    private bool reSetPathTrigger = false;//update마다 Astar가 작동하지 않게 해주는 bool값

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

    //창고지기 
    private bool WorkStart = false;
    void CargoClass()
    {

        //건물에 배정되었을때 경로수정
        if (NPCBUildTrigger)
        {
            ResetPath(this.transform, BuildingNum.transform);
            currentPathIndex = 0;
            NPCBUildTrigger = false;
        }
        if(BuildingNum != null)
        {
            //낮과밤이 바뀔때 한번만 경로수정
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
                     if(collider.건물내부저장공간 == colldier.건물내부저장공간최대치){
                        ResetPath(this.transform, collider.transform)
                        currentPathIndex = 0;
                        WorkStart = true;
                        break;
                    }*/
                }
            }else if (GameManager.instance.isDaytime && (this.transform.position == building.position))
            {
                /*건물에서 자원꺼내기*/
            }
            Move();
        }
        
    }

    private bool treeCuting = false;
    void WoodCutter()
    {
        bool workTrigger = false; //출근했는지 체크
        
        Transform tree = null;
        //건물에 배정되었을때 경로수정
        if (NPCBUildTrigger)
        {
            ResetPath(this.transform, BuildingNum.transform);
            NPCBUildTrigger = false;
        }
        //낮과밤이 바뀔때 한번만 경로수정
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
