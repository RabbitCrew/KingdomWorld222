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

    private void Start()
    {
        InvokeRepeating("SpawnintgCitizen", SpawnTime, SpawnTime);
    }

    void HouseCount()
    {
        HouseNum = 0;

        for (int i = 0; i < BuildingParent.transform.childCount; i++)
        {
            if(BuildingParent.transform.GetChild(i).tag == "House")
            {
                HouseNum++;
            }
        }
    }

    void SpawnintgCitizen()
    {
        HouseCount();

        if (CitizenList.Count < CitizenNum * HouseNum)
        {
            GameObject CSpawn = Instantiate(Citizen);
            CSpawn.transform.parent = SpawnPoint.transform;

            CSpawn.GetComponent<SpriteRenderer>().sprite = CtSpriteList[RandomSprite()];

            CitizenList.Add(CSpawn);
        }
    }

    int RandomSprite()
    {
        int Count = 0;

        Count = Random.Range(0, CtSpriteList.Length - 1);

        return Count;
    }
}
