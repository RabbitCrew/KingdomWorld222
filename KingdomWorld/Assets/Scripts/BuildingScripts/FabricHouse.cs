using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricHouse : MonoBehaviour
{
    BuildingSetting buildingSetting;
    BuildingColider buildingColider;

    public int fleece = 0;
    private int fleeceMax = 0;
    public int cloth = 0;
    private int clothMax = 0;

    private float increaseInterval = 3f;
    private float timer = 0f;

    private void Start()
    {
        buildingSetting = GetComponent<BuildingSetting>();
        buildingColider = GetComponent<BuildingColider>();

        GameManager.instance.Fleece = fleece;
        GameManager.instance.Cloth = cloth;

    }

    private void Update()
    {

        clothMax = buildingSetting.storeMax;

        timer += Time.deltaTime;

        if (buildingColider.isSettingComplete == true)
        {
            ChangeCloth();
        }
    }

    public void ChangeCloth()
    {
        if (fleece > 0 && cloth < clothMax)
        {
            if (timer >= increaseInterval)
            {
                timer = 0;
                fleece--;
                GameManager.instance.Fleece--;
                buildingSetting.store--;

                cloth++;
                GameManager.instance.Cloth--;
                buildingSetting.AddItem("cloth", 1);
            }
        }
    }
}
