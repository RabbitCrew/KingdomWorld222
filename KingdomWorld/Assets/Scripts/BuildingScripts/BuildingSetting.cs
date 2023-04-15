using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSetting : MonoBehaviour
{
    // . . . 건물 제한 ( 거래소 / 연구소는 제한 1 / 나머지는 임의로 지정 
    public int buildingShield { get; set; }
    public int maxBuildingShield { get; set; }
    public int BuildingHp { get; set; }
    public int MaxBuildingHp { get; set; }

    public GameObject npc;
    public int BuildingNum = 0;
    
    public float BuildingTime { get; set; }
    public bool carpenternCheck = false;

    public int npcCount = 0;
    public GameObject[] npcs;
    private int arrayIndex = 0;

    public int storeMax = 50;
    public int store = 0;       // . . . 저장량
    public Dictionary<string, int> items = new Dictionary<string, int>();


    // Start is called before the first frame update
    private void Awake()
    {
        BuildingTime = 10f;
        MaxBuildingHp = 10;
        BuildingHp = 10;
        buildingShield = 0;
        maxBuildingShield = 100;
        //GameManager.instance.InitializeGrid(500, 500);
    }
    void Start()
    {
        ItemSetting();
    }

    // Update is called once per frame
    void Update()
    {
        npcs = new GameObject[npcCount];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "carpenter")
        {
            carpenternCheck = true;
        }

        if (other.tag == "NPC")
        {
            if (arrayIndex < npcs.Length)
            {
                npcs[arrayIndex] = other.gameObject;
                arrayIndex++;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "NPC")
        {
            //npc가 가지고 있는 자원
            AddItem("Wood", 1);
        }
    }


    private void OnTriggerExit2D(Collider2D other)          //트리거에서 나간 오브젝트가 배열에 있으면 제거
    {
    }

    private bool ArrayContains(GameObject[] array, GameObject item)     //item체크 . . . citizen 배열 체크
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == item)
            {
                return true;
            }
        }
        return false;
    }

    private void AddToArray(ref GameObject[] array, GameObject item)        // item 추가
    {
        /*GameObject[] newArray = new GameObject[array.Length +1];
        array.CopyTo(newArray, 0);
        newArray[array.Length] = item;
        array = newArray;*/
    }

    private void RemoveFromArray(ref GameObject[] array, GameObject item)        // item 제거
    {
        int index = -1;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == item)
            {
                index = i;
                break;
            }
        }
        if (index >= 0)
        {
            GameObject[] newArray = new GameObject[array.Length - 1];
            for (int i = 0, j = 0; i < array.Length; i++)
            {
                if (i != index)
                {
                    newArray[j] = array[i];
                    j++;
                }
            }
            array = newArray;
        }
    }

    public void AddItem(string itemName, int amount)
    {
        if (items.ContainsKey(itemName))
        {
            for (int i = 0; i <= amount; i++)
            {
                if(store == storeMax)
                {
                    break;
                }
                store++;
            }
        }
    }

    public void ItemSetting()
    {
        string name;

        name = "wheat";
        items.Add(name, 1);

        name = "wood";
        items.Add(name, 1);

        name = "meat";
        items.Add(name, 1);

        name = "leather";
        items.Add(name, 1);

        name = "itronstone";
        items.Add(name, 1);

        name = "castIron";
        items.Add(name, 1);

        name = "milk";
        items.Add(name, 1);

        name = "cheese";
        items.Add(name, 1);

        name = "fleece";
        items.Add(name, 1);

        name = "cloth";
        items.Add(name, 1);
    }

}
