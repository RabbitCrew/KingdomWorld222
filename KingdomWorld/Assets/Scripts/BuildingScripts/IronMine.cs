using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronMine : MonoBehaviour
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
        if (!GameManager.instance.isDaytime)
        {
            buildingSetting.isWork = false;
        }
        if (buildingSetting.isWork)
        {
            Ironmining();
        }
    }

    public void Ironmining()
    {
        if (buildingSetting.store < buildingSetting.storeMax)
        {
            if (timer >= increaseInterval)
            {
                timer = 0;
                buildingSetting.store++;
                GameManager.instance.Itronstone++;
            }
        }
    }
}
