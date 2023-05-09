using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : NPCScrip
{
    public bool OneCycle = false; //NPC�� �̵������� üũ�ϴ� ����
    public bool work = false;//��� üũ ����
    private bool reSetPathTrigger = false;//update���� Astar�� �۵����� �ʰ� ���ִ� bool��
    public bool Sleep = false;
    private void Start()
    {
        Maxhp = 100;
        HP = 100;
        Grid = GameManager.instance.GetComponent<Setgrid>();
        Speed = 3f;
        cargoclasshaveitem.Add("Wood", 0);
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
        }else if (gameObject.CompareTag("Pastoralist"))
        {
            Pastoralist();
        }
        else if (this.CompareTag("NPC"))
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
            SearchMyIronBuilding("IronMine");
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
    private void SearchMyIronBuilding(string Building)
    {
        ResetParameter();
        float i = 1f;
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, i);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag(Building))
                {
                    if (collider.GetComponent<BuildingSetting>().npcs.Count < collider.GetComponent<BuildingSetting>().npcCount && GameManager.instance.isDaytime)//1��̸� �ǹ�Ž��
                    {
                        collider.GetComponent<BuildingSetting>().AddNPCs(this.gameObject);
                        BuildingNum = collider.gameObject;
                        NPCBUildTrigger = true;
                        return;
                    }
                }
            }
            if (i >= 1000)
            {
                Debug.Log("������ �����ϴ�");
                return;
            }
            i++;
        }
    }
    private void SearchMyBuilding(string Building)
    {
        ResetParameter();
        float i = 1f;
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, i);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag(Building))
                {
                    if (collider.GetComponent<BuildingSetting>().npcs.Count < collider.GetComponent<BuildingSetting>().npcCount && GameManager.instance.isDaytime)//1��̸� �ǹ�Ž��
                    {
                        collider.GetComponent<BuildingSetting>().AddNPCs(this.gameObject);
                        BuildingNum = collider.gameObject;
                        NPCBUildTrigger = true;
                        return;
                    }
                }
            }
            if (i >= 1000)
            {
                Debug.Log("�ǹ��� �����ϴ�");
                return;
            }
            i++;
        }
    }
    void dayTimeResetPath()
    {
        if (BuildingNum != null)
        {
            //�������� �ٲ� �ѹ��� ��μ���
            if ((GameManager.instance.isDaytime && !reSetPathTrigger) || (NPCBUildTrigger && GameManager.instance.isDaytime))//��ٽ���
            {
                Debug.Log("���");
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
            else if (!GameManager.instance.isDaytime && reSetPathTrigger && work && !OneCycle/*&& !Farmerwork*/)//���
            {
                Debug.Log("���");
                ResetPath(this.transform, HouseTr);
                currentPathIndex = 0;
                reSetPathTrigger = false;
                work = false;
            }
        }
    }
    //â������ 
    private bool isCargoWorkStart = false;//��ǥ�� �̵��ߴ��� üũ
    Transform fullbuilding = null;//���差�� ������ �ǹ�
    private bool isreturntocargo = false;//��ǥ�ǹ����� �ٽ� â��� �����ϴ��� üũ
    void CargoClass()
    {
        if (GameManager.instance.isDaytime && !isCargoWorkStart && !isreturntocargo && work)//���忡����������(���� : ��)
        {
            if (GameManager.instance.FullResourceBuildingList.Count > 0 && fullbuilding == null)
            {
                isCargoWorkStart = true;
                fullbuilding = GameManager.instance.FullResourceBuildingList[0].transform;
                GameManager.instance.FullResourceBuildingList.RemoveAt(0);
                ResetPath(this.transform, fullbuilding);
                currentPathIndex = 0;
                OneCycle = true;
            }
        }
        
        if(!OneCycle)
            dayTimeResetPath();
        Move();
    }
        

    Transform Tree = null;
    bool allwork = false;
    void WoodCutter()
    {
        if (!allwork && GameManager.instance.isDaytime)
        {
            dayTimeResetPath();
            allwork = true;
        }else if(!allwork && !GameManager.instance.isDaytime)
        {
            dayTimeResetPath();
        }
        if (BuildingNum.GetComponent<BuildingSetting>().EmptyTrigger && work)
        {
            searchWood();
            BuildingNum.GetComponent<BuildingSetting>().EmptyTrigger = false;
        }
        Move();
    }


    private bool hunting = false;
    Transform Animal = null;
    bool isReturntohunterhouse = false;
    void Hunter()
    {
        if (work)
        {
            if (!hunting && !isReturntohunterhouse)
            {
                float i = 1f;
                while (true)
                {
                    Collider[] colliders = Physics.OverlapSphere(this.transform.position, i);
                    foreach (Collider collider in colliders)
                    {
                        if (collider.CompareTag("Animal"))
                        {
                            Animal = collider.transform;
                            ResetPath(this.transform, Animal);
                            currentPathIndex = 0;
                            hunting = true;
                            OneCycle = true;
                            break;
                        }
                    }
                    i++;
                    if(Animal != null)
                    {
                        break;
                    }
                }
            }
        }

        dayTimeResetPath();
        Move();
    }

    void Pastoralist()
    {
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
            if (!isWeatStart && GameManager.instance.isDaytime)//��Ž��
            {
                
                if (GameManager.instance.WheatList.Count > 0 && HavedWheat == 0)//���� �ְ� ���� ��������������
                {
                    isWeatStart = true;
                    work = false;
                    WheatfieldGameObject = GameManager.instance.WheatList[0].transform.parent.gameObject;//wheatfield����
                    GameManager.instance.WheatList.RemoveAt(0);
                    ResetPath(this.transform, WheatfieldGameObject.transform);
                    currentPathIndex = 0;
                }
                else if (GameManager.instance.StorageList.Count > 0 && !isWeatCarry && HavedWheat > 0)//â�� �ְ� ���� ������ ������
                {
                    int i = 1;
                    while (true)
                    {
                        Collider[] colliders = Physics.OverlapSphere(this.transform.position, i);
                        foreach (var collider in colliders)
                        {
                            if (collider.CompareTag("Storage"))
                            {
                                ResetPath(this.transform, collider.transform);
                                currentPathIndex = 0;
                                isWeatCarry = true;
                                work = false;
                                return;
                            }
                        }
                        i++;
                    }
                }
            }
        }
        Move();
    }
    
    Transform Stone = null;
    void StoneMiner()
    {
        if (!OneCycle && GameManager.instance.isDaytime)
        {
            dayTimeResetPath();
            OneCycle = true;
        }
        else if (!OneCycle && !GameManager.instance.isDaytime)
        {
            dayTimeResetPath();
        }
        if (BuildingNum.GetComponent<BuildingSetting>().EmptyTrigger && work)
        {
            searchStone();
            BuildingNum.GetComponent<BuildingSetting>().EmptyTrigger = false;
        }
        dayTimeResetPath();
        Move();
    }

    void ironMiner()
    {
        dayTimeResetPath();
        Move();
    }

    void Smith()//��������
    {
        dayTimeResetPath();
        /*�������̱���*/
        Move();
    }

    void Cheese()//ġ������
    {
        dayTimeResetPath();
        Move();
    }

    void Ham()//������
    {
        dayTimeResetPath();
        Move();
    }
    bool isBuilingStart = false;
    bool isRepairStart = false;
    GameObject Building = null;
    float currentBuildingGauge = 0f;
    void Carpenter()//���
    {
        if (work)
        {
            if (!isBuilingStart)
            {
                if (GameManager.instance.WaitingBuildingList.Count > 0 && !OneCycle)
                {
                    Building = GameManager.instance.WaitingBuildingList[0];
                    GameManager.instance.WaitingBuildingList.RemoveAt(0);
                    isBuilingStart = true;
                    ResetPath(this.transform, Building.transform);
                    currentPathIndex = 0;
                    OneCycle = true;
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
                isRepairStart = false;//���� �Ϸ�
                yield break;
            }
        }
    }
    void fabric()//�ʰ�����
    {
        dayTimeResetPath();
        Move();
    }

    private void OnTriggerStay(Collider other)
    {
        if (BuildingNum != null)
            if (other.tag == BuildingNum.tag && !work && GameManager.instance.isDaytime)
            {
                work = true;
            }
    }
    private void OnTriggerEnter(Collider other)//������ ������ �Ͻ���
    {
        if(BuildingNum != null)
        {
            if (other.tag == BuildingNum.tag && !work && GameManager.instance.isDaytime)
            {
                work = true;
            }
            if (this.CompareTag("StorageNPC"))
            {
                if (isCargoWorkStart)//��ǥ�ǹ��� ���� �̵��� (���� : ��, ��)
                {
                    if (other.transform == fullbuilding.transform)//��ǥ�ǹ��� ����
                    {
                        if (other.CompareTag("Farm_house"))
                        {
                            other.GetComponent<BuildingSetting>().store = 0;
                            other.GetComponent<BuildingSetting>().milk = 0;
                            other.GetComponent<BuildingSetting>().fleece = 0;
                            
                        }
                        else
                        {
                            other.GetComponent<BuildingSetting>().store = 0;
                        }
                        other.GetComponent<BuildingSetting>().EmptyTrigger = true;
                        isCargoWorkStart = false;
                        isreturntocargo = true;
                        ResetPath(this.transform, BuildingNum.transform);//����
                        currentPathIndex = 0;
                    }
                }else if (isreturntocargo)
                {
                    if(other.transform == BuildingNum.transform)
                    {
                        isreturntocargo = false;
                        OneCycle = false;
                        fullbuilding = null;
                    }
                }
            }
            else if (this.CompareTag("CarpenterNPC") && isBuilingStart && other.CompareTag("WaitingBuilding") && other.transform == Building.transform)//���NPC �Ǽ�
            {
                StartCoroutine(Build(0.1f, other));
            } else if (this.CompareTag("FarmNPC") && isWeatStart)//���NPC �м�Ȯ
            {
                if (other.transform == WheatfieldGameObject.transform)
                {
                    StartCoroutine(Wheat(3f, WheatfieldGameObject));//3�ʵ� �м�Ȯ
                }
            } else if (this.CompareTag("FarmNPC") && !isWeatStart && isWeatCarry && HavedWheat > 0)//���NPC â���� �гֱ�
            {
                if (other.CompareTag("Storage"))
                {
                    Invoke("farmNPCpushWheat", 3f);
                }
            } else if (this.CompareTag("WoodCutter") && other.transform == BuildingNum.transform && HavedResource == 0 && other.GetComponent<BuildingSetting>().store < other.GetComponent<BuildingSetting>().storeMax)//��ٽ� ����Ž��
            {
                searchWood();
            }
            else if(this.CompareTag("WoodCutter") && Tree != null)//������ ������ �����ڸ���
            {
                if(other.transform == Tree)
                    StartCoroutine(CuttingTree(3));
            }else if(this.CompareTag("WoodCutter") && other.transform == BuildingNum.transform && HavedResource > 0)
            {
                StartCoroutine(PutWood(1f, other));
                /*else
                {
                    allwork = false;//�ϳ�
                }*/
            }
            else if(this.CompareTag("Hunter") && Animal.transform == other.transform && hunting)
            {
                StartCoroutine(HuntingAnimal(3f, Animal));
            }else if(this.CompareTag("Hunter") && HavedAnimal > 0 && isReturntohunterhouse && other.transform == BuildingNum.transform)
            {
                BuildingNum.GetComponent<BuildingSetting>().store += HavedAnimal;
                HavedAnimal = 0;
                isReturntohunterhouse = false;
                OneCycle = false;
            }else if (this.CompareTag("Pastoralist") && other.transform == BuildingNum.transform)
            {
                other.GetComponent<BuildingSetting>().isWork = true;
            }else if (this.CompareTag("HamNPC") && other.transform == BuildingNum.transform)
            {
                other.GetComponent<BuildingSetting>().isWork = true;
            }
            else if (this.CompareTag("CheeseNPC") && other.transform == BuildingNum.transform)
            {
                other.GetComponent<BuildingSetting>().isWork = true;
            }
            else if(this.CompareTag("IronMineWorker") && other.transform == BuildingNum.transform)
            {
                other.GetComponent<BuildingSetting>().isWork = true;
            }
            else if (this.CompareTag("FabricNPC") && other.transform == BuildingNum.transform)
            {
                other.GetComponent<BuildingSetting>().isWork = true;
            }else if (this.CompareTag("StoneMineWorker") && other.transform == BuildingNum.transform && HavedResource == 0 && other.GetComponent<BuildingSetting>().store < other.GetComponent<BuildingSetting>().storeMax)
            {
                searchStone();
            }
            else if (this.CompareTag("StoneMineWorker") && Stone != null)//������ ������ �����ڸ���
            {
                if (other.transform == Stone)
                    StartCoroutine(CuttingStone(3));
            }
            else if (this.CompareTag("StoneMineWorker") && other.transform == BuildingNum.transform && HavedResource > 0)
            {
                StartCoroutine(PutStone(1f, other));
            }else if (other.CompareTag("Street"))
            {
                Speed = 30f;
            }else if (other.CompareTag("Walkable"))
            {
                Speed = 3f;
            }
        }
    }

    void searchStone()
    {
        float i = 1f;
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, i);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Stone"))
                {
                    Debug.Log("�� Ž��");
                    Stone = collider.transform;
                    ResetPath(this.transform, Stone);
                    currentPathIndex = 0;
                    return;
                }
            }
            if (i >= 1000)
            {
                Debug.Log("���� �����ϴ�");
                return;
            }
            i++;
        }
    }
    IEnumerator PutStone(float delay, Collider other)
    {
        yield return new WaitForSeconds(delay);
        other.GetComponent<BuildingSetting>().store += HavedResource;
        GameManager.instance.Stone += HavedResource;
        HavedResource = 0;
        OneCycle = false;
        if (GameManager.instance.isDaytime && other.GetComponent<BuildingSetting>().store < other.GetComponent<BuildingSetting>().storeMax)
        {
            searchStone();
        }
    }
    private IEnumerator CuttingStone(float delay)
    {
        yield return new WaitForSeconds(delay);
        HavedResource += 1;
        Stone = null;
        ResetPath(this.transform, BuildingNum.transform);
        currentPathIndex = 0;
        yield break;
    }
    IEnumerator PutWood(float delay, Collider other)
    {
        yield return new WaitForSeconds(delay);
        other.GetComponent<BuildingSetting>().store += HavedResource;
        GameManager.instance.Wood += HavedResource;
        HavedResource = 0;
        allwork = false;
        if (GameManager.instance.isDaytime && other.GetComponent<BuildingSetting>().store < other.GetComponent<BuildingSetting>().storeMax)
        {
            searchWood();
        }
    }
    void searchWood()
    {
        float i = 1f;
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, i);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("tree") && !collider.GetComponent<NatureObject>().Slave)
                {
                    Debug.Log("���� Ž��");
                    Tree = collider.transform;
                    Tree.GetComponent<NatureObject>().Slave = true;
                    ResetPath(this.transform, Tree);
                    currentPathIndex = 0;
                    return;
                }
            }
            if(i >= 1000)
            {
                Debug.Log("������ �����ϴ�");
                return;
            }
            i++;
        }
    }
    void farmNPCpushWheat()
    {
        GameManager.instance.Wheat += HavedWheat;
        HavedWheat = 0;
        isWeatCarry = false;
        work = true;
    }
    IEnumerator Wheat(float delay, GameObject wheatfield)//�м�Ȯ �ڷ�ƾ
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("���ı�NPC�ڷ�ƾ");
        Destroy(wheatfield.GetComponent<Cornfield>().clone.gameObject);
        wheatfield.GetComponent<Cornfield>().cultureCheck = false;
        HavedWheat += 2;
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
                // 3�� ������
                yield return null;
                StopCoroutine("Build");
                yield break;
            }

            building.GetComponent<WaitingBuilding>().building.GetComponent<BuildingSetting>().BuildingHp += 1;
            //Debug.Log("�������� ��ܿ�");
            if (building.GetComponent<WaitingBuilding>().building.GetComponent<BuildingSetting>().BuildingHp
                >= building.GetComponent<WaitingBuilding>().building.GetComponent<BuildingSetting>().MaxBuildingHp)
            {
                isBuilingStart = false;
                ResetPath(this.transform, BuildingNum.transform);
                currentPathIndex = 0;
                Building = null;
                OneCycle = false;
                yield break;
            }
        }
    }
    private IEnumerator CuttingTree(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(Tree != null)
            Destroy(Tree.gameObject);
        HavedResource += 3;
        Tree = null;
        ResetPath(this.transform, BuildingNum.transform);
        currentPathIndex = 0;
        yield break;
    }
    private IEnumerator HuntingAnimal(float delay, Transform animal)//���� ��� �Ϸ�
    {
        yield return new WaitForSeconds(delay);
        if(animal != null)
            Destroy(animal.gameObject);
        HavedAnimal += 1;
        Animal = null;
        hunting = false;
        isReturntohunterhouse = true;
        ResetPath(this.transform, BuildingNum.transform);
        currentPathIndex = 0;
        yield break;
    }
    public void ResetParameter()
    {
        OneCycle = false;
        isBuilingStart = false;//���
        Building = null;//���
        isCargoWorkStart = false;//â������
        fullbuilding = null;//â������
        isreturntocargo = false;//â������
        Animal = null;//��ɲ�
        isReturntohunterhouse = false;//��ɲ�
        Tree = null;//������
        allwork = false;//������
        Stone = null;//������
        HavedResource = 0;
    }
}
