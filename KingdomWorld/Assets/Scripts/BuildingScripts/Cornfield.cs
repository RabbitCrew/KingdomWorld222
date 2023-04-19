using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cornfield : MonoBehaviour
{
    // . . . NPC 수확 ㅡ 수확하면 clone제거 해야됨

    public GameObject wheatPrefab;
    public GameObject clone;//instantiate 오브젝트 clone제거 스크립트 NPC에존재.
    public int wheat = 0;
    public bool cultureCheck = false;

    private float increaseInterval = 5f;
    private float timer = 0f;

    private float decreaseProbability = 0.2f;
    private float decreaseRatio = 0.1f;
    private float increaseProbability = 0.2f;
    private float increaseRatio = 0.2f;

    BuildingSetting buildingSetting;
    WinterIsComing winterIsComing;
    BuildingColider buildingColider;

    void Start()
    {
    }

     void Update()
    {
        buildingSetting = GetComponent<BuildingSetting>();
        buildingColider = GetComponent<BuildingColider>();
        winterIsComing = GetComponent<WinterIsComing>();

        if (cultureCheck == false)
        {
            timer += Time.deltaTime;
            WheatProduction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       /*if(collision.tag == "NPC")
        {
            // . . . NPC가 수확하면 wheatPrefab 제거
        }*/
    }

    public void WheatProduction()
    {
        if(buildingColider.isSettingComplete == true)
        {
            if(winterIsComing.isWinter == false)
            {
                if (timer >= increaseInterval)
                {
                    timer = 0f;
                    wheat = 10;
                    RandomEvent();
                    cultureCheck = true;
                    AddPrefab();
                }
            }
        }
    }

    public void RandomEvent()
    {
        float randomValue = Random.value;

        if(randomValue < decreaseProbability)
        {
            wheat = (int)(wheat * (1 - decreaseRatio));
            Debug.Log("20% 하락");
        }
        else if(randomValue < decreaseProbability + increaseProbability)
        {
            wheat = (int)(wheat * (1 + increaseRatio));
            Debug.Log("20% 증가");
        }
        else
        {
            return;
        }
    }

    public void AddPrefab()
    {
        Vector3 fieldPosition = new Vector3(this.transform.position.x, 0, this.transform.position.z + 0.4f);
        clone = wheatPrefab;
        clone.transform.position = fieldPosition;
        clone = Instantiate(wheatPrefab);
        clone.transform.SetParent(this.transform);
        GameManager.instance.WheatList.Add(clone);//NPCDictionary에 밀추가
    }
}
