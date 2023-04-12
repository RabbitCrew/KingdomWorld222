using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : NPCScrip
{
    public bool work = false;//출근 체크 변수
    private bool reSetPathTrigger = false;//update마다 Astar가 작동하지 않게 해주는 bool값
    public bool Sleep = false;
    //public GameObject Testbuilding;
    private void Start()
    {
        //BuildingNum = Testbuilding;
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
    //public bool SearchMyBuildingTrigger = false;
    public void searchMyBuilding()
    {
        
        if(this.GetComponent<CitizenInfoPanel>().jobNumEnum == ObjectNS.JobNum.WAREHOUSEKEEPER)
        {
            Debug.Log("건물검색실행");
            SearchMyBuilding("Storage");
        }
        else if(this.GetComponent<CitizenInfoPanel>().jobNumEnum == ObjectNS.JobNum.WOODCUTTER)
        {
            SearchMyBuilding("WoodCutter_house");
        }else if (this.GetComponent<CitizenInfoPanel>().jobNumEnum == ObjectNS.JobNum.CARPENTER)
        {
            SearchMyBuilding("Carpenter_house");
        }else if(this.GetComponent<CitizenInfoPanel>().jobNumEnum == ObjectNS.JobNum.HUNTER)
        {
            SearchMyBuilding("Hunter_house");
        }else if(this.GetComponent<CitizenInfoPanel>().jobNumEnum == ObjectNS.JobNum.FARMER)
        {
            SearchMyBuilding("Wheat");
        }else if(this.GetComponent<CitizenInfoPanel>().jobNumEnum == ObjectNS.JobNum.PASTORALIST)
        {
            SearchMyBuilding("Farm_house");
        }
        else if (this.GetComponent<CitizenInfoPanel>().jobNumEnum == ObjectNS.JobNum.IRONMINER)
        {
            SearchMyBuilding("Mine_house");
        }
        else if (this.GetComponent<CitizenInfoPanel>().jobNumEnum == ObjectNS.JobNum.STONEMINER)
        {
            SearchMyBuilding("Mine_house");
        }
        else if (this.GetComponent<CitizenInfoPanel>().jobNumEnum == ObjectNS.JobNum.HAMNPC)
        {
            SearchMyBuilding("Ham_house");
        }
        else if (this.GetComponent<CitizenInfoPanel>().jobNumEnum == ObjectNS.JobNum.CHEESENPC)
        {
            SearchMyBuilding("Cheese_house");
        }
        else if (this.GetComponent<CitizenInfoPanel>().jobNumEnum == ObjectNS.JobNum.CLOTHNPC)
        {
            SearchMyBuilding("Cloth_house");
        }
        else if (this.GetComponent<CitizenInfoPanel>().jobNumEnum == ObjectNS.JobNum.SMITH)
        {
            SearchMyBuilding("Smith_house");
        }
    }
    private void SearchMyBuilding(string Building)
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag(Building))
            {
                if (collider.GetComponent<BuildingSetting>().npcCount <= 3 && GameManager.instance.isDaytime)
                {
                    BuildingNum = collider.gameObject;
                    NPCBUildTrigger = true;
                    break;
                }
            }
        }
    }
    void dayTimeResetPath()
    {
        if (BuildingNum != null)
        {
            //건물에 배정되었을때 경로수정
            if (NPCBUildTrigger && GameManager.instance.isDaytime)//중간에 NPC배정했을시
            {
                Debug.Log("이동시작");
                ResetPath(this.transform, BuildingNum.transform);
                currentPathIndex = 0;
                NPCBUildTrigger = false;
                work = false;
            }
        
            //낮과밤이 바뀔때 한번만 경로수정
            if (GameManager.instance.isDaytime && !reSetPathTrigger && !work)//출근시작
            {
                ResetPath(this.transform, BuildingNum.transform);
                currentPathIndex = 0;
                reSetPathTrigger = true;
            }
            else if (!GameManager.instance.isDaytime && reSetPathTrigger && work)//퇴근
            {
                ResetPath(this.transform, HouseTr);
                currentPathIndex = 0;
                reSetPathTrigger = false;
                work = false;
            }
        }
    }
    //창고지기 
    private bool isCargoWorkStart = false;
    Transform fullbuilding = null;
    void CargoClass()
    {
        dayTimeResetPath();
        if (GameManager.instance.isDaytime && !isCargoWorkStart)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
            foreach (Collider collider in colliders)
            {
                fullbuilding = collider.transform;
                if (fullbuilding.GetComponent<BuildingSetting>() != null)
                {
                    if (fullbuilding.GetComponent<BuildingSetting>().store == fullbuilding.GetComponent<BuildingSetting>().storeMax)
                    {
                        isCargoWorkStart = true;//운반시작
                        ResetPath(this.transform, fullbuilding);
                        currentPathIndex = 0;
                        break;
                    }
                }
            }
        }
        if (GameManager.instance.isDaytime && (this.transform.position == fullbuilding.position) && isCargoWorkStart)
        {
            
            /*건물에서 무슨자원인지 알아야함 자원꺼내기*/
        }
        Move();
    }
        

    private bool treeCuting = false;
    Transform tree = null;
    void WoodCutter()
    {
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
    Transform animal = null;
    void Hunter()
    {
        dayTimeResetPath();
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

    void Farmer()
    {
        dayTimeResetPath();
        Move();
    }

    private bool mining = false;
    Transform stone = null;
    void StoneMiner()
    {
        dayTimeResetPath();
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
    Transform iron = null;
    void ironMiner()
    {
        dayTimeResetPath();
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
    bool isBuilingStart = false;
    bool isRepairStart = false;
    Transform Building = null;
    float currentBuildingGauge = 0f;
    void Carpenter()//목수
    {
        dayTimeResetPath();
        if (work)
        {
            if(!isBuilingStart && Building == null)
            {
                Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
                //1. 건설대기 건물 찾기 
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("WaitingBuilding"))
                    {
                        isBuilingStart = true;
                        Building = collider.transform;
                        ResetPath(this.transform, Building);
                        currentPathIndex = 0;
                        break;
                    }else if(collider.GetComponent<BuildingSetting>() != null)//손상된 건물 찾기
                    {
                        if (collider.GetComponent<BuildingSetting>().BuildingHp < collider.GetComponent<BuildingSetting>().MaxBuildingHp && !isRepairStart)
                        {
                            isRepairStart = true;
                            Building = collider.transform;
                            ResetPath(this.transform, Building);
                            currentPathIndex = 0;
                            break;
                        }
                    }
                }
            }
            if(Building.position == transform.position && isBuilingStart && Building != null)//건설
            {
                currentBuildingGauge += BuildingSpeed * Time.deltaTime;
                if(currentBuildingGauge >= Building.GetComponent<BuildingSetting>().BuildingTime)
                {
                    //건설완료
                    isBuilingStart = false;
                    Building = null;
                }
            }else if(Building.position == transform.position && isRepairStart && Building != null)//리페어
            {
                StartCoroutine(Repair(1f, Building));
            }
        }
        Move();
    }
    private IEnumerator Repair(float delay, Transform building)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            building.GetComponent<BuildingSetting>().BuildingHp += 1;
            if (building.GetComponent<BuildingSetting>().BuildingHp >= building.GetComponent<BuildingSetting>().MaxBuildingHp)
            {
                Building = null;
                isRepairStart = false;//수리 완료
                break;
            }
        }
    }
    void fabric()//옷감장인
    {
        dayTimeResetPath();
        Move();
    }
    
    private void OnTriggerEnter(Collider other)//목적지 도착시 일시작
    {
        if(BuildingNum != null)
        {
            if (other.tag == BuildingNum.tag && !work)
            {
                work = true;
            }
        }
    }
}
