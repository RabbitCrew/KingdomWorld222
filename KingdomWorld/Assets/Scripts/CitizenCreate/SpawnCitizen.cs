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

    void HouseCount()//생성된 건물 중 거주지 수 카운트.
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

        if (CitizenList.Count < CitizenNum * HouseNum) // 거주지 수 비례 인구 수보다 현재 인구 수가 적을 시 인구 생성.
        {
            GameObject CSpawn = Instantiate(Citizen);
            CSpawn.transform.parent = SpawnPoint.transform;

            CSpawn.GetComponent<SpriteRenderer>().sprite = CtSpriteList[RandomSprite()];

            CitizenList.Add(CSpawn); //시민 생성 후 리스트에 넣음.

            GameManager.instance.RestHuman.Add(CSpawn);
        }
    }

    int RandomSprite() //일반 시민 스프라이트 랜덤 지정
    {
        int Count = 0;

        Count = Random.Range(0, CtSpriteList.Length - 1);

        return Count;
    }
}
