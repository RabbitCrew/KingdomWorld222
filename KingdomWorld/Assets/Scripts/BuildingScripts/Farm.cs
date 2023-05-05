using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    
    // . . . 미완 ( 최대값 제한 X 

    public BuildingSetting buildingSetting;
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
    //private int milk = 0;
    private int milkMax = 10;
    //public int fleece = 0;
    private int fleeceMax = 10;

    public int Milk 
    { 
        get { return buildingSetting.milk; } 
        set {
            if (buildingSetting.milk > milkMax) 
            {
                value = milkMax;
            }
            buildingSetting.milk = value;
            buildingSetting.store = value;
        }
    }
    public int Fleece
    {
        get { return buildingSetting.fleece; }
        set
        {
            if(buildingSetting.fleece > fleeceMax)
            {
                value = fleeceMax;
            }
            buildingSetting.fleece = value;
        }
    }
    private float increaseInterval = 10f;
    private float timer = 0f;
    private float timer_1 = 0f;
    private float cooltime = 5f;
    private bool harvesting = false;

    private void Start()
    {
        buildingSetting = GetComponent<BuildingSetting>();
        buildingColider = GetComponent<BuildingColider>();

        //GameManager.instance.Cow = cow;
        //GameManager.instance.Sheep = sheep;
        //GameManager.instance.Milk = milk;
        //GameManager.instance.Fleece = fleece;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timer_1 += Time.deltaTime;

        animalstore = cow + sheep;
        if (!GameManager.instance.isDaytime)
        {
            buildingSetting.isWork = false;
        }
        if(buildingColider.isSettingComplete == true && buildingSetting.isWork)
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
                GameManager.instance.Cow++;

                animalClone_1 = cowPrefab;
                animalClone_1.transform.position = animalSpwner.transform.position;
                animalClone_1 = Instantiate(cowPrefab);
                animalClone_1.transform.SetParent(this.transform);
            }
            if(sheep < sheepMax)
            {
                sheep++;
                GameManager.instance.Sheep++;

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
                if(Milk < milkMax)
                {
                    Milk++;
                    GameManager.instance.Milk++;
                }
                buildingSetting.AddItem("milk", 1);
            }
            for (int j = 0; j < sheep; j++)
            {
                if(Fleece < fleeceMax)
                {
                    Fleece++;
                    GameManager.instance.Fleece++;
                }
                buildingSetting.AddItem("fleece", 1);
            }
        }
    }
}
