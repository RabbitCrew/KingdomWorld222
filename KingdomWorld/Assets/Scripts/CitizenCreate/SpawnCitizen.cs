using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCitizen : MonoBehaviour
{
    public GameObject Citizen;
    public GameObject BuildingParent;

    public GameObject SpawnPoint;

    public List<GameObject> CitizenList = new List<GameObject>();
    public Sprite[] CtSpriteList;

    public float SpawnTime = 10f;

    public int CitizenNum = 3;

    public int HouseNum = 0;


    private Dictionary<int, GameObject> houseDic = new Dictionary<int, GameObject>();

    private void Start()
    {
        InvokeRepeating("SpawnintgCitizen", SpawnTime, SpawnTime);
    }

    void HouseCount()//������ �ǹ� �� ������ �� ī��Ʈ.
    {
        HouseNum = 0;
        houseDic.Clear();
        int n = 0;

        for (int i = 0; i < BuildingParent.transform.childCount; i++)
        {
            if(BuildingParent.transform.GetChild(i).tag == "House")
            {
                houseDic.Add(n, BuildingParent.transform.GetChild(i).gameObject);
                n++;
                HouseNum++;
            }
        }
    }

    void SpawnintgCitizen()
    {
        HouseCount();

        for (int i = 0; i < houseDic.Count; i++)
        {
            if (CitizenList.Count < CitizenNum * HouseNum) // ������ �� ��� �α� ������ ���� �α� ���� ���� �� �α� ����.
            {
                GameObject CSpawn = Instantiate(Citizen);
                CSpawn.transform.parent = SpawnPoint.transform;

                if (houseDic.ContainsKey(i))
                {
                    CSpawn.transform.position = houseDic[i].transform.position;
                }

                CSpawn.GetComponent<SpriteRenderer>().sprite = CtSpriteList[RandomSprite()];

                CitizenList.Add(CSpawn); //�ù� ���� �� ����Ʈ�� ����.

                GameManager.instance.RestHuman.Add(CSpawn);
            }
        }
    }

    int RandomSprite() //�Ϲ� �ù� ��������Ʈ ���� ����
    {
        int Count = 0;

        Count = Random.Range(0, CtSpriteList.Length - 1);

        return Count;
    }
}
