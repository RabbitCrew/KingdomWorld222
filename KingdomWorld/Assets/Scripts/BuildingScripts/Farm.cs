using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    // . . . 미완 ( 최대값 제한 X 

    private BuildingSetting buildingSetting;
    BuildingColider buildingColider;

    // . . . 소 / 양
    public int cow = 0;
    private int cowMax =5;
    public GameObject cowPrefab;

    public int sheep = 0;
    private int sheepMax = 5;
    public GameObject sheepPrefab;

    public int animalstore = 0;

    public GameObject animalClone_1;
    public GameObject animalClone_2;
    public Transform animalSpwner;

    // . . . 우유 / 양털
    public int milk = 0;
    private int milkMax = 10;
    public int fleece = 0;
    private int fleeceMax = 10;

    private float increaseInterval = 10f;
    private float timer = 0f;
    private float timer_1 = 0f;
    private float cooltime = 5f;
    private bool harvesting = false;

    private void Start()
    {
        buildingSetting = GetComponent<BuildingSetting>();
        buildingColider = GetComponent<BuildingColider>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timer_1 += Time.deltaTime;

        GameManager.instance.Cow = cow;
        GameManager.instance.Sheep = sheep;

        animalstore = cow + sheep;

        if(buildingColider.isSettingComplete == true)
        {
            AddCowSheep();
            Collection();
        }
    }

    public void AddCowSheep()
    {
        if(timer >= increaseInterval)
        {
            timer = 0f;
            if(cow < cowMax)
            { 
                cow++;

                animalClone_1 = cowPrefab;
                animalClone_1.transform.position = animalSpwner.transform.position;
                animalClone_1 = Instantiate(cowPrefab);
                animalClone_1.transform.SetParent(this.transform);
            }
            if(sheep < sheepMax)
            {
                sheep++;

                animalClone_2 = sheepPrefab;
                animalClone_2.transform.position = animalSpwner.transform.position;
                animalClone_2 = Instantiate(sheepPrefab);
                animalClone_2.transform.SetParent(this.transform);
            }
        }
    }

    public void Collection()
    {
        if (timer_1 >= cooltime)
        {
            timer_1 = 0f;
            for (int i = 0; i < cow; i++)
            {
                milk++;
                buildingSetting.AddItem("milk", 1);
            }
            for (int j = 0; j < sheep; j++)
            {
                fleece++;
                buildingSetting.AddItem("fleece", 1);
            }
        }
    }
}
