using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSetting : MonoBehaviour
{
    public GameObject npc;
    public int BuildingNum = 0;
    public int MaxBuildingHp = 100;
    public int BuildingHp = 100;
    public float BuildingTime;
    public bool carpenternCheck = false;

    public int npcCount = 0;
    public GameObject[] npcs;
    private int arrayIndex = 0;

    public int storeMax = 50;
    public int store = 0;       // . . . ���差
    public Dictionary<string, int> items = new Dictionary<string, int>();

    private Resource resource;

    // Start is called before the first frame update
    void Start()
    {
        string name;

        name = "wood";
        items.Add(name, 1);
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
            //npc�� ������ �ִ� �ڿ�
            AddItem("wood", 1);
        }
    }


    private void OnTriggerExit2D(Collider2D other)          //Ʈ���ſ��� ���� ������Ʈ�� �迭�� ������ ����
    {
    }

    private bool ArrayContains(GameObject[] array, GameObject item)     //itemüũ . . . citizen �迭 üũ
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

    private void AddToArray(ref GameObject[] array, GameObject item)        // item �߰�
    {
        /*GameObject[] newArray = new GameObject[array.Length +1];
        array.CopyTo(newArray, 0);
        newArray[array.Length] = item;
        array = newArray;*/
    }

    private void RemoveFromArray(ref GameObject[] array, GameObject item)        // item ����
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
                store++;
                if(store == storeMax)
                {
                    break;
                }
            }
        }
    }

}