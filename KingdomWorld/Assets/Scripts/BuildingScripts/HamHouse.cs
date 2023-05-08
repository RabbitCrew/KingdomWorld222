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

        GameManager.instance.Meat = meat;
        GameManager.instance.Ham = ham;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        hamMax = buildingSetting.storeMax;

        if(buildingColider.isSettingComplete == true && buildingSetting.isWork)
        {
            ChangeHam();
        }
    }


    public void ChangeHam()
    {
        if (GameManager.instance.Meat > 0)
        {
            if (timer >= increaseInterval)
            {
                timer = 0;
                GameManager.instance.Meat--;

                ham++;
                GameManager.instance.Ham++;
                buildingSetting.store++;
            }
        }
    }



}
