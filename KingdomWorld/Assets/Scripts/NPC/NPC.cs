using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : NPCScrip
{
    public GameObject TestBuildingTransform;
    private bool work = false;//��� üũ ����
    public string NPCClass;
    public delegate void MyDelegate(Transform start, Transform end);

    public MyDelegate myDelegate = null;

    private bool reSetPathTrigger = false;//update���� Astar�� �۵����� �ʰ� ���ִ� bool��

    private void Start()
    {
        BuildingNum = TestBuildingTransform;
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
        else if (gameObject.CompareTag("MineWorker"))
        {
            Miner();
        }
    }
    void dayTimeResetPath()
    {
        //�ǹ��� �����Ǿ����� ��μ���
        if (NPCBUildTrigger)
        {
            ResetPath(this.transform, BuildingNum.transform);
            currentPathIndex = 0;
            NPCBUildTrigger = false;
        }
        if (BuildingNum != null)
        {
            //�������� �ٲ� �ѹ��� ��μ���
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
                work = false; //���
            }else if (this.transform.position == BuildingNum.transform.position && GameManager.instance.isDaytime)
            {
                work = true;//���
            }
        }

    }
    //â������ 
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
        

    private bool treeCuting = false;
    void WoodCutter()
    {
        Transform tree = null;
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
    void Hunter()
    {
        dayTimeResetPath();
        Transform animal = null;
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
    
    private bool mining = false;
    void Farmer()
    {
        dayTimeResetPath();
        Move();
    }
    void Miner()
    {
        dayTimeResetPath();
        Transform stone = null;
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
}
