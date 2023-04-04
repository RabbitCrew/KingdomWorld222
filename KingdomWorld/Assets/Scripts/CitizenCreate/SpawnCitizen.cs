using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCitizen : MonoBehaviour
{
    public SpriteRenderer SRender;

    public GameObject Citizen;
    public GameObject BuildingParent;

    public GameObject SpawnPoint;

    public List<GameObject> CitizenList = new List<GameObject>();
    public Sprite[] CitizenSpriteList;

    public float SpawnTime = 10f;

    public int CitizenNum = 3;

    public int HouseNum = 0;

    public Image Cimg;

    private void Start()
    {
        SRender = GetComponent<SpriteRenderer>();

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

    int Count;

    void SpawnintgCitizen()
    {
        HouseCount();

        if (CitizenList.Count < CitizenNum * HouseNum)
        {
            GameObject CSpawn = Instantiate(Citizen);
            CSpawn.transform.parent = SpawnPoint.transform;

            SpriteChange();

            CSpawn.GetComponent<SpriteRenderer>().sprite = CitizenSpriteList[Count];

            CitizenList.Add(CSpawn);
        }
    }

    void SpriteChange()
    {
        Count = Random.Range(0, CitizenSpriteList.Length);

        Debug.Log(Count);
    }
}