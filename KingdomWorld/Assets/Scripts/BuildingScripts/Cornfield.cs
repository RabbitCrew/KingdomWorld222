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

    public static float increaseInterval { get; set; }
    private float timer = 0f;

    public static float decreaseProbability { get; set; }
    private float decreaseRatio = 0.1f;
    public static float increaseProbability { get; set; }
    private float increaseRatio = 0.2f;

    BuildingSetting buildingSetting;
    //WinterIsComing winterIsComing;
    BuildingColider buildingColider;

    private void Awake()
    {
        decreaseProbability = 0.2f;
        increaseProbability = 0.2f;
        increaseInterval = 5f;
    }

    void Start()
    {
        buildingSetting = GetComponent<BuildingSetting>();
        buildingColider = GetComponent<BuildingColider>();
        //winterIsComing = GetComponent<WinterIsComing>();

        GameManager.instance.Wheat = wheat;
    }

     void Update()
    {
        if (cultureCheck == false)
        {
            timer += Time.deltaTime;
            WheatProduction();
        }
    }

    public void WheatProduction()
    {
        if(buildingColider.isSettingComplete == true)
        {
            if(GameManager.instance.isWinterComing == false)
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
