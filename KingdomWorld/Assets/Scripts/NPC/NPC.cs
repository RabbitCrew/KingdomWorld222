using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : NPCScrip
{
    public bool work = false;//��� üũ ����
    private bool reSetPathTrigger = false;//update���� Astar�� �۵����� �ʰ� ���ִ� bool��
    public GameObject Testbuilding;
    private void Start()
    {
        BuildingNum = Testbuilding;
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
        if (BuildingNum != null)
        {
            //�ǹ��� �����Ǿ����� ��μ���
            if (NPCBUildTrigger && GameManager.instance.isDaytime)//�߰��� NPC����������
            {
                ResetPath(this.transform, BuildingNum.transform);
                currentPathIndex = 0;
                NPCBUildTrigger = false;
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
            }else if (this.transform.position == BuildingNum.transform.position && GameManager.instance.isDaytime)//���
            {
                work = true;
            }
        }
        else
        {
            NPCBUildTrigger = false;
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
                        isCargoWorkStart = true;
                        ResetPath(this.transform, fullbuilding);
                        currentPathIndex = 0;
                        break;
                    }
                }
            }
        }
        else if (GameManager.instance.isDaytime && (this.transform.position == fullbuilding.position) && isCargoWorkStart)
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
                    }else if(collider.GetComponent<BuildingSetting>().BuildingHp < collider.GetComponent<BuildingSetting>().MaxBuildingHp)
                    {
                        isRepairStart = true;
                        Building = collider.transform;
                        ResetPath(this.transform, Building);
                        currentPathIndex = 0;
                        break;
                    }
                }
            }
            if(Building.position == transform.position && isBuilingStart && Building != null)
            {
                currentBuildingGauge += BuildingSpeed * Time.deltaTime;
                if(currentBuildingGauge >= Building.GetComponent<BuildingSetting>().BuildingTime)
                {
                    //�Ǽ��Ϸ�
                    isBuilingStart = false;
                    Building = null;
                }
            }else if(Building.position == transform.position && isRepairStart && Building != null)
            {
                StartCoroutine(Repair(3f, Building));
            }
            //2. �ջ�� �ǹ� ã��
        }
        Move();
    }
    private IEnumerator Repair(float delay, Transform building)
    {
        yield return new WaitForSeconds(delay);
        building.GetComponent<BuildingSetting>().BuildingHp += 1;
        if(building.GetComponent<BuildingSetting>().BuildingHp == building.GetComponent<BuildingSetting>().MaxBuildingHp)
        {
            isRepairStart = false;//���� �Ϸ�
        }
        
    }
    void fabric()//�ʰ�����
    {
        dayTimeResetPath();
        Move();
    }
}