using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCitizen : MonoBehaviour
{
    public GameObject Citizen;
    public GameObject BuildingParent;

    public GameObject SpawnPoint;

    //public List<GameObject> CitizenList = new List<GameObject>();

    public float FirstSTime = 1f;

    public int CitizenNum = 3;

    public int HouseNum = 0;
    private List<GameObject> houseTrList = new List<GameObject>();

    private void Start()
    {
        InvokeRepeating("SpawnintgCitizen", FirstSTime, Inventory.instance.SpawnTime);
    }

    void HouseCount()//������ �ǹ� �� ������ �� ī��Ʈ.
    {
        HouseNum = 0;
        Inventory.instance.houseDic.Clear();
        int n = 0;

        for (int i = 0; i < BuildingParent.transform.childCount; i++)
        {
            if(BuildingParent.transform.GetChild(i).tag == "House" && BuildingParent.transform.GetChild(i).gameObject.activeSelf)
            {
                Inventory.instance.houseDic.Add(n, BuildingParent.transform.GetChild(i).gameObject);
                n++;
                HouseNum++;
            }
        }
    }

    void SpawnintgCitizen()
    {
        if (!GameManager.instance.ReturnTutorialPanel().unLockSpawn) { return; }

        HouseCount();

        for (int i = 0; i < Inventory.instance.houseDic.Count; i++)
        {
            if (GameManager.instance.AllHuman.Count < CitizenNum * HouseNum) // ������ �� ��� �α� ������ ���� �α� ���� ���� �� �α� ����.
            {

                if (Inventory.instance.houseDic.ContainsKey(i))
                {
                    houseTrList = GameManager.instance.AllHuman.FindAll(a => a.GetComponent<NPC>().HouseTr.position.Equals(Inventory.instance.houseDic[i].transform.position));
                    //Debug.Log(houseTrList.Count);
                    if (houseTrList.Count < CitizenNum)
                    {
                        GameObject CSpawn = Instantiate(Citizen);
                        CSpawn.transform.parent = SpawnPoint.transform;
                        //Debug.Log(houseTrList.Count);
                        CSpawn.transform.position = Inventory.instance.houseDic[i].transform.position;
                        CSpawn.GetComponent<NPC>().HouseTr = Inventory.instance.houseDic[i].transform;

                        CSpawn.GetComponent<SpriteRenderer>().sprite = Inventory.instance.CtSpriteList[RandomSprite()];

                        //CitizenList.Add(CSpawn); //�ù� ���� �� ����Ʈ�� ����.
                        GameManager.instance.AllHuman.Add(CSpawn);

                        CSpawn.SendMessage("SetPAni", Count);

                        GameManager.instance.RestHuman.Add(CSpawn);
                    }
                }


            }
        }
    }

    int Count = 0;

    public int RandomSprite() //�Ϲ� �ù� ��������Ʈ ���� ����
    {
        Count = Random.Range(0, Inventory.instance.CtSpriteList.Length - 1);
        //Debug.Log(Count);
        return Count;
    }
}