using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneClickAllNPCWork : MonoBehaviour
{
    [SerializeField]
    private SpriteManager spriteManager;

    private int jobCode;
    private void Update()
    {
        OneClickNPCWork();
    }
    void OneClickNPCWork()
    {
        if(GameManager.instance.EmptyNPCBuilding.Count > 0 && GameManager.instance.isDaytime && GameManager.instance.RestHuman.Count > 0)
        {
            if (GameManager.instance.EmptyNPCBuilding[0].tag.Equals("Storage")) { jobCode = 6; }
            else if (GameManager.instance.EmptyNPCBuilding[0].tag.Equals("WoodCutter_house")) { jobCode = 1; }
            else if (GameManager.instance.EmptyNPCBuilding[0].tag.Equals("Carpenter_house")) { jobCode = 2; }
            else if (GameManager.instance.EmptyNPCBuilding[0].tag.Equals("Hunter_house")) { jobCode = 3; }
            else if (GameManager.instance.EmptyNPCBuilding[0].tag.Equals("Farm_house")) { jobCode = 5; }
            else if (GameManager.instance.EmptyNPCBuilding[0].tag.Equals("Mine_house")) { jobCode = 8; }
            else if (GameManager.instance.EmptyNPCBuilding[0].tag.Equals("Ham_house")) { jobCode = 9; }
            else if (GameManager.instance.EmptyNPCBuilding[0].tag.Equals("Cheese_house")) { jobCode = 10; }
            else if (GameManager.instance.EmptyNPCBuilding[0].tag.Equals("Cloth_house")) { jobCode = 11; }
            else if (GameManager.instance.EmptyNPCBuilding[0].tag.Equals("Smith_house")) { jobCode = 12; }

            GameManager.instance.EmptyNPCBuilding[0].GetComponent<BuildingSetting>().npcs.Add(GameManager.instance.RestHuman[0]);//건물에 시민리스트에 시민추가
            GameManager.instance.RestHuman[0].GetComponent<CitizenInfoPanel>().WareClothes(spriteManager.GetCitizenSprArr(jobCode - 1), jobCode);
            GameManager.instance.RestHuman[0].GetComponent<BuildingNPCSet>().SetBNPC(jobCode);//시민 태그부여
            GameManager.instance.jobCountDic[GameManager.instance.RestHuman[0].GetComponent<CitizenInfoPanel>().jobNumEnum]--;
            GameManager.instance.RestHuman[0].GetComponent<NPC>().BuildingNum = GameManager.instance.EmptyNPCBuilding[0];
            GameManager.instance.RestHuman[0].GetComponent<NPC>().NPCBUildTrigger = true;
            GameManager.instance.RestHuman.RemoveAt(0);
            GameManager.instance.EmptyNPCBuilding.RemoveAt(0);
        }
    }
}
