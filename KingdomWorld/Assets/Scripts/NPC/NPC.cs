using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : NPCScrip
{
    public string NPCClass;
    public delegate void MyDelegate(Transform start, Transform end);

    public MyDelegate myDelegate = null;

    private bool reSetPathTrigger = false;//update마다 Astar가 작동하지 않게 해주는 bool값

    private void Start()
    {
        Speed = 3f;
        reSetPathTrigger = false;
    }
    void Update()
    {
        CargoClass();
    }

    //창고지기 
    void CargoClass()
    {

        //건물에 배정되었을때 경로수정
        if (NPCBUildTrigger)
        {
            ResetPath(this.transform, BuildingNum.transform);
            
            NPCBUildTrigger = false;
        }
        if(BuildingNum != null)
        {
            //낮과밤이 바뀔때 한번만 경로수정
            if (GameManager.instance.isDaytime && !reSetPathTrigger)
            {
                Debug.LogError("낮path실행");
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
                    }*/
                }
            }
            Move();
        }
        
    }

    bool treeCuting = false;
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
