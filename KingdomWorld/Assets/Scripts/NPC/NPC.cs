using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : NPCScrip
{
    public bool work = false;//��� üũ ����
    private bool reSetPathTrigger = false;//update���� Astar�� �۵����� �ʰ� ���ִ� bool��
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
        }/*else if(this.GetComponent<CitizenInfoPanel>().jobNumEnum == ObjectNS.JobNum.FARMER)
        {
            SearchMyBuilding("Wheat");
        }*/
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
                
                /*if (this.CompareTag("FarmNPC") && GameManager.instance.isDaytime)//WheatŽ��
                {
                    BuildingNum = collider.transform.parent.gameObject;
                    isWeatStart = true;
                    NPCBUildTrigger = true;
                    break;
                }*/
                if (collider.GetComponent<BuildingSetting>().npcCount <= 3 && GameManager.instance.isDaytime)//3������ �ǹ�Ž��
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
            //NPCBUildTrigger�� true�Ͻ� ��μ���
            if (NPCBUildTrigger && GameManager.instance.isDaytime)//�߰��� NPC����������
            {
                Debug.Log("�̵�����");
                ResetPath(this.transform, BuildingNum.transform);
                currentPathIndex = 0;
                NPCBUildTrigger = false;
                work = false;
            }
        
            //�������� �ٲ� �ѹ��� ��μ���
            if (GameManager.instance.isDaytime && !reSetPathTrigger && !work)//��ٽ���
            {
                ResetPath(this.transform, BuildingNum.transform);
                currentPathIndex = 0;
                reSetPathTrigger = true;
            }
            else if (!GameManager.instance.isDaytime && reSetPathTrigger && work)//���
            {
                ResetPath(this.transform, HouseTr);
                currentPathIndex = 0;
                reSetPathTrigger = false;
                work = false;
            }
        }
    }
    //â������ 
    private bool isCargoWorkStart = false;
    Transform fullbuilding = null;
    void CargoClass()
    {
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
                        isCargoWorkStart = true;//��ݽ���
                        ResetPath(this.transform, fullbuilding);
                        currentPathIndex = 0;
                        break;
                    }
                }
            }
        }
        if (GameManager.instance.isDaytime && (this.transform.position == fullbuilding.position) && isCargoWorkStart)
        {

            /*�ǹ����� �����ڿ����� �˾ƾ��� �ڿ�������*/
        }
        dayTimeResetPath();
        Move();
    }
        

    private bool treeCuting = false;
    Transform tree = null;
    void WoodCutter()
    {
        if (work)
        {
            if (!treeCuting)//����Ž��
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
        dayTimeResetPath();
        Move();
    }
    private IEnumerator CuttingTree(float delay, Transform tree)
    {
        yield return new WaitForSeconds(delay);
        Destroy(tree);
        treeCuting = false;//�����ڸ��� �Ϸ�
    }

    private bool hunting = false;
    Transform animal = null;
    void Hunter()
    {
        if (!hunting)//����Ž��
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
        dayTimeResetPath();
        Move();
    }

    private IEnumerator HuntingAnimal(float delay, Transform animal)
    {
        yield return new WaitForSeconds(delay);
        Destroy(animal);
        hunting = false;//���� ��� �Ϸ�
    }

    bool isWeatStart = false;
    void Farmer()
    {
        if (!isWeatStart)//��Ž��
        {
            if (GameManager.instance.WheatList.Count > 0 && HavedWheat == 0)
            {
                BuildingNum = GameManager.instance.WheatList[0].transform.parent.gameObject;
                GameManager.instance.WheatList.RemoveAt(0);
                isWeatStart = true;
                NPCBUildTrigger = true;
            }
            else if(GameManager.instance.StorageList.Count > 0)
            {
                Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
                foreach (var collider in colliders)
                {
                    if (collider.CompareTag("Storage"))
                    {
                        BuildingNum = collider.gameObject;
                        NPCBUildTrigger = true;
                        break;
                    }
                }
            }

        }
        
        dayTimeResetPath();
        Move();
    }

    private bool mining = false;
    Transform stone = null;
    void StoneMiner()
    {
        if (!mining)//����Ž��
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
        mining = false;//���� ä�� �Ϸ�
    }
    Transform iron = null;
    void ironMiner()
    {
        if (!mining)//����Ž��
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
        mining = false;//���� ä�� �Ϸ�
    }
    void Smith()
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
                if (GameManager.instance.WaitingBuildingList.Count > 0)
                {
                    Building = GameManager.instance.WaitingBuildingList[0];
                    GameManager.instance.WaitingBuildingList.RemoveAt(0);
                    isBuilingStart = true;
                    ResetPath(this.transform, Building.transform);
                    currentPathIndex = 0;
                }
                /*Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
                //1. �Ǽ���� �ǹ� ã�� 
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("WaitingBuilding"))
                    {
                        isBuilingStart = true;
                        Building = collider.transform;
                        ResetPath(this.transform, Building);
                        currentPathIndex = 0;
                        break;
                    }
                    else if (collider.GetComponent<BuildingSetting>() != null)//�ջ�� �ǹ� ã��
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
                }*/
            /*}
            if(Building != null)
            {
                if (Building.position == transform.position && isBuilingStart && Building != null)//�Ǽ�
                {
                    currentBuildingGauge += BuildingSpeed * Time.deltaTime;
                    if (currentBuildingGauge >= Building.GetComponent<BuildingSetting>().BuildingTime)
                    {
                        //�Ǽ��Ϸ�
                        isBuilingStart = false;
                        Building = null;
                    }
                }
                else if (Building.position == transform.position && isRepairStart && Building != null)//�����
                {
                    StartCoroutine(Repair(1f, Building));
                }*/
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
    
    private void OnTriggerEnter(Collider other)//������ ������ �Ͻ���
    {
        if(BuildingNum != null)
        {
            if (other.tag == BuildingNum.tag && !work)
            {
                work = true;
            }
            else if (this.CompareTag("CarpenterNPC") && isBuilingStart && other.CompareTag("WaitingBuilding"))//���NPC �Ǽ�
            {
                StartCoroutine(Build(0.1f, other));
                //Debug.Log("����� ��� �����ϱ�?");
            }else if (this.CompareTag("FarmNPC") && isWeatStart)//���NPC �м�Ȯ
            {
                if(other.transform == BuildingNum.transform)//wheat��ã�ƿ°��� wheatfield�� ã�ƿ°Ծƴ�
                {
                    StartCoroutine(Wheat(3f, other));//3�ʵ� �м�Ȯ
                }
            }else if(this.CompareTag("FarmNPC") && !isWeatStart)//���NPC â���� �гֱ�
            {
                if (other.CompareTag("Storage") && other.transform == BuildingNum.transform)
                {
                    GameManager.instance.Wheat += HavedWheat;
                    HavedWheat = 0;
                }
            }
        }
    }
    IEnumerator Wheat(float delay, Collider wheatfield)//�м�Ȯ �ڷ�ƾ
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("���ı�NPC�ڷ�ƾ");
        Destroy(wheatfield.GetComponent<Cornfield>().clone.gameObject);
        wheatfield.GetComponent<Cornfield>().cultureCheck = false;
        HavedWheat += 1;
        isWeatStart = false;
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
                StopCoroutine("Build");
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
                //StopCoroutine("Build");
                yield break;
            }
        }
    }
}
