using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricHouse : MonoBehaviour
{
    BuildingSetting buildingSetting;
    BuildingColider buildingColider;

    private float increaseInterval = 5f;
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
            ChangeCloth();
        }
    }

    public void ChangeCloth()
    {
        if (GameManager.instance.Fleece > 0 && buildingSetting.store < buildingSetting.storeMax)
        {
            if (timer >= increaseInterval)
            {
                timer = 0;
                GameManager.instance.Fleece--;
                buildingSetting.store++;
                GameManager.instance.Cloth++;
            }
        }
    }
}
