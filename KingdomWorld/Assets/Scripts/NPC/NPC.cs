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
        }else if (this.CompareTag("NPC"))
        {
            ResetPath(this.transform, HouseTr);
            currentPathIndex = 0;
            work = false;
            Move();
        }
    }
    //public bool SearchMyBuildingTrigger = false;
    public void searchMyBuilding()
    {
        
        if(this.GetComponent<CitizenInfoPanel>().jobNumEnum == ObjectNS.JobNum.WAREHOUSEKEEPER)
        {
            
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
        }
        else if(this.GetComponent<CitizenInfoPanel>().jobNumEnum == ObjectNS.JobNum.PASTORALIST)
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
                if (collider.GetComponent<BuildingSetting>().npcs.Count < collider.GetComponent<BuildingSetting>().npcCount && GameManager.instance.isDaytime)//3명이하 건물탐색
                {
                    collider.GetComponent<BuildingSetting>().AddNPCs(this.gameObject);
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
        {//밀을 창고에 넣으려 가고있을때 퇴근을 하면 다음날 아침이 되면 길찾아주는 함수가 필요함
            //낮과밤이 바뀔때 한번만 경로수정
            if ((GameManager.instance.isDaytime && !reSetPathTrigger) || (NPCBUildTrigger && GameManager.instance.isDaytime))//출근시작
            {
                Debug.Log("출근");
                if (this.CompareTag("FarmNPC"))
                    work = true;
                else
                {
                    ResetPath(this.transform, BuildingNum.transform);
                    currentPathIndex = 0;
                }
                reSetPathTrigger = true;
                NPCBUildTrigger = false;
            }
            else if (!GameManager.instance.isDaytime && reSetPathTrigger && work /*&& !Farmerwork*/)//퇴근
            {
                Debug.Log("퇴근");
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
        if (GameManager.instance.isDaytime && !isCargoWorkStart)
        {
            if(GameManager.instance.FullResourceBuildingList.Count > 0)
            {
                isCargoWorkStart = true;
                fullbuilding = GameManager.instance.FullResourceBuildingList[0].transform;
                GameManager.instance.FullResourceBuildingList.RemoveAt(0);
                ResetPath(this.transform, fullbuilding);
                currentPathIndex = 0;
            }
        }
        if (GameManager.instance.isDaytime && (this.transform.position == fullbuilding.position) && isCargoWorkStart)
        {
            /*건물에서 무슨자원인지 알아야함 자원꺼내기*/
        }
        dayTimeResetPath();
        Move();
    }
        

    private bool treeCuting = false;
    Transform Tree = null;
    private bool isCarryTree = false;
    void WoodCutter()
    {
        if (work)
        {
            if (!treeCuting)//나무탐색
            {
                if (!isCarryTree && HavedWood == 0)
                {
                    Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
                    foreach (Collider collider in colliders)
                    {
                        if (collider.CompareTag("tree"))
                        {
                            Tree = collider.transform;
                            ResetPath(this.transform, Tree);
                            currentPathIndex = 0;
                            treeCuting = true;
                            break;
                        }
                    }
                }else if (!isCarryTree && HavedWood > 0)
                {
                    ResetPath(this.transform, BuildingNum.transform);
                    currentPathIndex = 0;
                    isCarryTree = true;
                }
            }
        }
        dayTimeResetPath();
        Move();
    }


    private bool hunting = false;
    Transform Animal = null;
    private bool isAnimalCarry = false;
    void Hunter()
    {
        if (!hunting)//동물탐색
        {
            if (!isAnimalCarry && HavedAnimal > 0)
            {
                ResetPath(this.transform, BuildingNum.transform);
                currentPathIndex = 0;
                isAnimalCarry = true;
            }else if(!isAnimalCarry && HavedAnimal == 0)
            {
                Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Animal"))
                    {
                        Animal = collider.transform;
                        ResetPath(this.transform, Animal);
                        currentPathIndex = 0;
                        hunting = true;
                        break;
                    }
                }
            }
        }
        dayTimeResetPath();
        Move();
    }



    public bool isWeatStart = false;
    public bool isWeatCarry = false;
    public GameObject WheatfieldGameObject = null;
    void Farmer()
    {
        BuildingNum = HouseTr.gameObject;
        dayTimeResetPath();
        if (work)
        {
            if (!isWeatStart && GameManager.instance.isDaytime)//밀탐색
            {
                
                if (GameManager.instance.WheatList.Count > 0 && HavedWheat == 0)//밀이 있고 밀을 갖고있지않으면
                {
                    isWeatStart = true;
                    work = false;
                    WheatfieldGameObject = GameManager.instance.WheatList[0].transform.parent.gameObject;//wheatfield저장
                    GameManager.instance.WheatList.RemoveAt(0);
                    ResetPath(this.transform, WheatfieldGameObject.transform);
                    currentPathIndex = 0;
                }
                else if (GameManager.instance.StorageList.Count > 0 && !isWeatCarry && HavedWheat > 0)//창고가 있고 밀을 가지고 있으면
                {
                    Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
                    foreach (var collider in colliders)
                    {
                        if (collider.CompareTag("Storage"))
                        {
                            ResetPath(this.transform, collider.transform);
                            currentPathIndex = 0;
                            isWeatCarry = true;
                            work = false;
                            break;
                        }
                    }
                }
            }
        }
        
        
        Move();
    }

    private bool mining = false;
    Transform stone = null;
    void StoneMiner()
    {
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
        dayTimeResetPath();
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
        dayTimeResetPath();
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
    GameObject Building = null;
    float currentBuildingGauge = 0f;
    void Carpenter()//목수
    {
        if (work)
        {
            if (!isBuilingStart)
            {
                if (GameManager.instance.WaitingBuildingList.Count > 0)
                {
                    Building = GameManager.instance.WaitingBuildingList[0];
                    GameManager.instance.WaitingBuildingList.RemoveAt(0);
                    isBuilingStart = true;
                    ResetPath(this.transform, Building.transform);
                    currentPathIndex = 0;
                }
            }
        }
        dayTimeResetPath();
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
                yield break;
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
            if (other.tag == BuildingNum.tag && !work && GameManager.instance.isDaytime)
            {
                work = true;
            }
            else if (this.CompareTag("CarpenterNPC") && isBuilingStart && other.CompareTag("WaitingBuilding") && other.transform == Building.transform)//목수NPC 건설
            {
                StartCoroutine(Build(0.1f, other));
                //Debug.Log("여기는 몇번 찍힙니까?");
            }else if (this.CompareTag("FarmNPC") && isWeatStart)//농부NPC 밀수확
            {
                if(other.transform == WheatfieldGameObject.transform)
                {
                    StartCoroutine(Wheat(3f, WheatfieldGameObject));//3초뒤 밀수확
                }
            }else if(this.CompareTag("FarmNPC") && !isWeatStart && isWeatCarry && HavedWheat > 0)//농부NPC 창고가서 밀넣기
            {
                if (other.CompareTag("Storage"))
                {
                    Invoke("farmNPCpushWheat", 3f);
                }
            }else if (this.CompareTag("WoodCutter") && other.CompareTag("tree") && other.transform == Tree)//나무꾼
            {
                StartCoroutine(CuttingTree(3, Tree.transform));
            }else if (this.CompareTag("WoodCutter") && other.CompareTag("WoodCutter_house") && isCarryTree)//나무꾼 나무꾼건물에 나무넣기
            {
                GameManager.instance.Wood += HavedWood;
                HavedWood = 0;
                isCarryTree = false;
            }
            else if (this.CompareTag("StorageNPC") && other.CompareTag(fullbuilding.tag))
            {
                //fullbuilding자원 꺼내기
            }else if(this.CompareTag("Hunter") && Animal.transform == other.transform && hunting)
            {
                StartCoroutine(HuntingAnimal(3f, Animal));
            }else if(this.CompareTag("Hunter") && HavedAnimal > 0 && isAnimalCarry && other.transform == BuildingNum.transform)
            {
                GameManager.instance.Meat += HavedAnimal;
                HavedAnimal = 0;
                isAnimalCarry = false;
            }
        }
    }
    void farmNPCpushWheat()
    {
        GameManager.instance.Wheat += HavedWheat;
        HavedWheat = 0;
        isWeatCarry = false;
        work = true;
    }
    IEnumerator Wheat(float delay, GameObject wheatfield)//밀수확 코루틴
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("밀파괴NPC코루틴");
        Destroy(wheatfield.GetComponent<Cornfield>().clone.gameObject);
        wheatfield.GetComponent<Cornfield>().cultureCheck = false;
        HavedWheat += 1;
        isWeatStart = false;
        WheatfieldGameObject = null;
        work = true;
    }
    IEnumerator Build(float delay, Collider building)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            if (building == null)
            {
                // 3중 나생문
                yield return null;
                StopCoroutine("Build");
                yield break;
            }

            building.GetComponent<WaitingBuilding>().building.GetComponent<BuildingSetting>().BuildingHp += 1;
            //Debug.Log("무한으로 즐겨요");
            if (building.GetComponent<WaitingBuilding>().building.GetComponent<BuildingSetting>().BuildingHp
                >= building.GetComponent<WaitingBuilding>().building.GetComponent<BuildingSetting>().MaxBuildingHp)
            {
                isBuilingStart = false;
                ResetPath(this.transform, BuildingNum.transform);
                currentPathIndex = 0;
                Building = null;
                yield break;
            }
        }
    }
    private IEnumerator CuttingTree(float delay, Transform tree)
    {
        yield return new WaitForSeconds(delay);
        Destroy(tree);
        Tree = null;
        treeCuting = false;//나무자르기 완료
        yield break;
    }
    private IEnumerator HuntingAnimal(float delay, Transform animal)
    {
        yield return new WaitForSeconds(delay);
        Destroy(animal);
        Animal = null;
        hunting = false;//동물 사냥 완료
    }
}
