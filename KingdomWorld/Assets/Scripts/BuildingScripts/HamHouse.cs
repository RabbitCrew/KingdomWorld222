using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamHouse : MonoBehaviour
{
    private BuildingSetting buildingSetting;
    BuildingColider buildingColider;

    private float increaseInterval = 3f;
    private float timer = 0f;

    public int meat = 0;
    public int ham = 0;
    private int hamMax = 0;

    private void Start()
    {
        buildingSetting = GetComponent<BuildingSetting>();
        buildingColider = GetComponent<BuildingColider>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        GameManager.instance.Meat = meat;
        GameManager.instance.Ham = ham;

        hamMax = buildingSetting.storeMax;
        if(buildingColider.isSettingComplete == true)
        {
            ChangeHam();
        }
    }


    public void ChangeHam()
    {
        if (meat > 0 && ham < hamMax)
        {
            if (timer >= increaseInterval)
            {
                timer = 0;
                meat--;
                buildingSetting.store--;

                ham++;
                buildingSetting.store++;
            }
        }
    }



}
