using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseHouse : MonoBehaviour
{

    BuildingSetting buildingSetting;
    BuildingColider buildingColider;

    private float increaseInterval = 3f;
    private float timer = 0f;

    private void Start()
    {
        buildingSetting = GetComponent<BuildingSetting>();
        buildingColider = GetComponent<BuildingColider>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (buildingColider.isSettingComplete == true && buildingSetting.isWork)
        {
            ChangeCheese();
        }
    }

    public void ChangeCheese()
    {
        if (GameManager.instance.Milk > 0 && buildingSetting.store < buildingSetting.storeMax)
        {
            if (timer >= increaseInterval)
            {
                timer = 0;
                GameManager.instance.Milk--;
                buildingSetting.store++;
                GameManager.instance.Cheese++;
            }
        }
    }
}
