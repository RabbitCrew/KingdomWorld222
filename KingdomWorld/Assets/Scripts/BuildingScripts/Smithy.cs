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
            //buildingSetting.AddItem("itronstone", npc°¡ °¡Áö°íÀÖ´Â Ã¶±¤¼® ¼ö);
        }
    }

    private void Start()
    {
        buildingSetting = GetComponent<BuildingSetting>();
        buildingColider = GetComponent<BuildingColider>();

        GameManager.instance.Itronstone = itronstone;
        GameManager.instance.CastIron = castIron;
    }

    private void Update()
    { 
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
                GameManager.instance.Itronstone--;
                buildingSetting.store--;


                castIron++;
                GameManager.instance.CastIron++;
                buildingSetting.AddItem("castIron", 1);
            }
        }
    }
}
