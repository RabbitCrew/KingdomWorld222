using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : NPCScrip
{
    public bool work = false;//출근 체크 변수

    private bool reSetPathTrigger = false;//update마다 Astar가 작동하지 않게 해주는 bool값

    private void Start()
    {
        Grid = GameManager.instance.GetComponent<Setgrid>();
        Speed = 3f;
    }
    void Update()
    {
        if (gameObject.CompareTag("StorageNPC"))
        {
            CargoClass();
        }
        else if (gameObject.CompareTag("WoodCutter"))
        {
            WoodCutter();
        }else if (gameObject.CompareTag("Hunter"))
        {
            Hunter();
        }else if (gameObject.CompareTag("FarmNPC"))
        {
            Farmer();
        }
        else if (gameObject.CompareTag("StoneMineWorker"))
        {
            StoneMiner();
        }else if (gameObject.CompareTag("IronMineWorker"))
        {
            ironMiner();
        }
        else if (gameObject.CompareTag("Smith"))
        {
            Smith();
        }
        else if (gameObject.CompareTag("CheeseNPC"))
        {
            Cheese();
        }
        else if (gameObject.CompareTag("HamNPC"))
        {
            Ham();
        }
        else if (gameObject.CompareTag("CarpenterNPC"))
        {
            Carpenter();
        }
        else if (gameObject.CompareTag("FabricNPC"))
        {
            fabric();
        }
    }
    void dayTimeResetPath()
    {
        //건물에 배정되었을때 경로수정
        if (NPCBUildTrigger)
        {
            ResetPath(this.transform, BuildingNum.transform);
            currentPathIndex = 0;
            NPCBUildTrigger = false;
        }
        if (BuildingNum != null)
        {
            //낮과밤이 바뀔때 한번만 경로수정
            if (GameManager.instance.isDaytime && !reSetPathTrigger)
            {
                ResetPath(this.transform, BuildingNum.transform);
                currentPathIndex = 0;
                reSetPathTrigger = true;
            }
            else if (!GameManager.instance.isDaytime && reSetPathTrigger && !work)
            {
                ResetPath(this.transform, HouseTr);
                currentPathIndex = 0;
                reSetPathTrigger = false;
                work = false; //퇴근
            }else if (this.transform.position == BuildingNum.transform.position && GameManager.instance.isDaytime)
            {
                work = true;//출근
            }
        }
    }
    //창고지기 
    private bool WorkStart = false;
    void CargoClass()
    {
        dayTimeResetPath();

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
        

    private bool treeCuting = false;
    void WoodCutter()
    {
        Transform tree = null;
        dayTimeResetPath();
        
        if (work)
        {
            if (!treeCuting)//나무탐색
            {
                Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("tree"))
                    {
                        tree = collider.transform;
                        ResetPath(this.transform, tree);
                        currentPathIndex = 0;
                        treeCuting = true;
                        break;
                    }
                }
            }
            else
            {
                if (this.transform.position == tree.position)
                {
                    StartCoroutine(CuttingTree(3f, tree));
                }
            }
            
        }
        Move();
    }
    private IEnumerator CuttingTree(float delay, Transform tree)
    {
        yield return new WaitForSeconds(delay);
        Destroy(tree);
        treeCuting = false;//나무자르기 완료
    }

    private bool hunting = false;
    void Hunter()
    {
        dayTimeResetPath();
        Transform animal = null;
        if (!hunting)//나무탐색
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Animal"))
                {
                    animal = collider.transform;
                    ResetPath(this.transform, animal);
                    currentPathIndex = 0;
                    hunting = true;
                    break;
                }
            }
        }
        else
        {
            if (this.transform.position == animal.position)
            {
                StartCoroutine(HuntingAnimal(3f, animal));
            }
        }
        Move();
    }
    private IEnumerator HuntingAnimal(float delay, Transform animal)
    {
        yield return new WaitForSeconds(delay);
        Destroy(animal);
        hunting = false;//동물 사냥 완료
    }
    
    private bool mining = false;
    void Farmer()
    {
        dayTimeResetPath();
        Move();
    }
    void StoneMiner()
    {
        dayTimeResetPath();
        Transform stone = null;
        if (!mining)//광물탐색
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("stone"))
                {
                    stone = collider.transform;
                    ResetPath(this.transform, stone);
                    currentPathIndex = 0;
                    mining = true;
                    break;
                }
            }
        }
        else
        {
            if (this.transform.position == stone.position)
            {
                StartCoroutine(MiningStone(3f, stone));
            }
        }
        Move();
    }
    private IEnumerator MiningStone(float delay, Transform stone)
    {
        yield return new WaitForSeconds(delay);
        Destroy(stone);
        mining = false;//바위 채광 완료
    }

    void ironMiner()
    {
        dayTimeResetPath();
        Transform iron = null;
        if (!mining)//광물탐색
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("stone"))
                {
                    iron = collider.transform;
                    ResetPath(this.transform, iron);
                    currentPathIndex = 0;
                    mining = true;
                    break;
                }
            }
        }
        else
        {
            if (this.transform.position == iron.position)
            {
                StartCoroutine(MiningIron(3f, iron));
            }
        }
        Move();
    }
    private IEnumerator MiningIron(float delay, Transform iron)
    {
        yield return new WaitForSeconds(delay);
        Destroy(iron);
        mining = false;//바위 채광 완료
    }
    void Smith()
    {
        dayTimeResetPath();
        /*대장장이구현*/
        Move();
    }

    void Cheese()//치즈장인
    {
        dayTimeResetPath();
        Move();
    }

    void Ham()//햄장인
    {
        dayTimeResetPath();
        Move();
    }
    
    void Carpenter()//목수
    {
        dayTimeResetPath();
        if (work)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
            //1. 건설대기 건물 찾기
            //2. 손상된 건물 찾기
        }
        Move();
    }

    void fabric()//옷감장인
    {
        dayTimeResetPath();
        Move();
    }
}
