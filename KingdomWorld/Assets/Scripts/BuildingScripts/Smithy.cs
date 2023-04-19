using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smithy : MonoBehaviour
{

    BuildingSetting buildingSetting;
    BuildingColider buildingColider;

    public int itronstone = 0;
    public int castIron = 0;
    private int castIronMax = 0;

    private float increaseInterval = 3f;
    private float timer = 0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "MineWorker")
        {
            //buildingSetting.AddItem("itronstone", npc�� �������ִ� ö���� ��);
        }
    }

    private void Start()
    {
        buildingSetting = GetComponent<BuildingSetting>();
        buildingColider = GetComponent<BuildingColider>();
    }

    private void Update()
    {
        GameManager.instance.Itronstone = itronstone;
        GameManager.instance.CastIron = castIron;

        castIronMax = buildingSetting.storeMax;

        timer += Time.deltaTime;

        if(buildingColider.isSettingComplete == true)
        {
            ChangeCastIron();
        }
    }

    public void ChangeCastIron()
    {
        if(itronstone > 0 && castIron < castIronMax)
        {
            if(timer >= increaseInterval)
            {
                timer = 0;
                itronstone--;
                buildingSetting.store--;

                castIron++;
                buildingSetting.AddItem("castIron", 1);
            }
        }
    }
}
