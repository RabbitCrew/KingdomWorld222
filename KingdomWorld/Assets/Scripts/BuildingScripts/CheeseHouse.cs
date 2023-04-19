using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseHouse : MonoBehaviour
{

    BuildingSetting buildingSetting;
    BuildingColider buildingColider;

    public int milk = 0;
    public int cheese = 0;
    private int cheeseMax = 0;

    private float increaseInterval = 3f;
    private float timer = 0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "MineWorker")
        {
            //buildingSetting.AddItem("itronstone", npc°¡ °¡Áö°íÀÖ´Â Ã¶±¤¼® ¼ö);
        }
    }

    private void Start()
    {
        buildingSetting = GetComponent<BuildingSetting>();
        buildingColider = GetComponent<BuildingColider>();
    }

    private void Update()
    {
        //GameManager.instance.Milk = milk;
        GameManager.instance.Cheese = cheese;

        cheeseMax = buildingSetting.storeMax;

        timer += Time.deltaTime;

        if (buildingColider.isSettingComplete == true)
        {
            ChangeCheese();
        }
    }

    public void ChangeCheese()
    {
        if (milk > 0 && cheese < cheeseMax)
        {
            if (timer >= increaseInterval)
            {
                timer = 0;
                milk--;
                buildingSetting.store--;

                cheese++;
                buildingSetting.AddItem("cheese", 1);
            }
        }
    }
}
