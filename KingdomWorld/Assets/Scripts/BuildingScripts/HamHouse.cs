using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamHouse : MonoBehaviour
{
    private BuildingSetting buildingSetting;
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

        if(buildingColider.isSettingComplete == true && buildingSetting.isWork)
        {
            ChangeHam();
        }
    }


    public void ChangeHam()
    {
        if (GameManager.instance.Meat > 0 && buildingSetting.store < buildingSetting.storeMax)
        {
            if (timer >= increaseInterval)
            {
                timer = 0;
                GameManager.instance.Meat--;
                GameManager.instance.Ham++;
                buildingSetting.store++;
            }
        }
    }



}
