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
            Debug.Log("�ǹ��˻�����");
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
            //�ǹ��� �����Ǿ����� ��μ���
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
        Move();
    }
        

    private bool treeCuting = false;
    Transform tree = null;
    void WoodCutter()
    {
        dayTimeResetPath();
        
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
        dayTimeResetPath();
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
        Move();
    }

    private IEnumerator HuntingAnimal(float delay, Transform animal)
    {
        yield return new WaitForSeconds(delay);
        Destroy(animal);
        hunting = false;//���� ��� �Ϸ�
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
        dayTimeResetPath();
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
    Transform Building = null;
    float currentBuildingGauge = 0f;
    void Carpenter()//���
    {
        dayTimeResetPath();
        if (work)
        {
            if(!isBuilingStart && Building == null)
            {
                Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1000f);
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
                    }else if(collider.GetComponent<BuildingSetting>() != null)//�ջ�� �ǹ� ã��
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
            if(Building.position == transform.position && isBuilingStart && Building != null)//�Ǽ�
            {
                currentBuildingGauge += BuildingSpeed * Time.deltaTime;
                if(currentBuildingGauge >= Building.GetComponent<BuildingSetting>().BuildingTime)
                {
                    //�Ǽ��Ϸ�
                    isBuilingStart = false;
                    Building = null;
                }
            }else if(Building.position == transform.position && isRepairStart && Building != null)//�����
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
                isRepairStart = false;//���� �Ϸ�
                break;
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
        }
    }
}
