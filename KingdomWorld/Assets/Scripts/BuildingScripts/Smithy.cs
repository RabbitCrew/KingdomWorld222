using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smithy : MonoBehaviour
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

        if(buildingColider.isSettingComplete == true && buildingSetting.isWork)
        {
            ChangeCastIron();
        }
    }

    public void ChangeCastIron()
    {
        if(GameManager.instance.Itronstone > 0 && buildingSetting.store < buildingSetting.storeMax)
        {
            if(timer >= increaseInterval)
            {
                timer = 0;
                GameManager.instance.Itronstone--;
                GameManager.instance.CastIron++;
                buildingSetting.store++;
            }
        }
    }
}
