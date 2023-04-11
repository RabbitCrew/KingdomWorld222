using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    // . . . 미완 ( 최대값 제한 X 

    private BuildingSetting buildingSetting;

    // . . . 소 / 양
    public int cow = 2;
    //private int cowMax =10;
    public int sheep = 2;
    //private int sheepMax = 10;
    public int animalstore = 0;

    // . . . 우유 / 양털
    private int milk = 0;
    private int milkMax = 10;
    private int fleece = 0;
    private int fleeceMax = 10;

    private float increaseInterval = 50f;
    private float timer = 0f;
    private float timer_1 = 0f;
    private float cooltime = 10f;
    private bool harvesting = false;

    private void Start()
    {
        buildingSetting = GetComponent<BuildingSetting>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timer_1 += Time.deltaTime;
        
        AddCowSheep();
        if(harvesting == false)
        {
            Collection();
        }

        animalstore = cow + sheep;
    }

    public void AddCowSheep()
    {
        if(timer >= increaseInterval)
        {
            timer = 0f;
            cow++;
            sheep++;
        }
    }

    public void Collection()
    {
        if(timer >= cooltime)
        {
            timer_1 = 0f;
            for(int i=0; i < cow; i++)
            {
                milk++;
                buildingSetting.AddItem("milk", 1);
            }
            for(int j=0; j < sheep; j++)
            {
                fleece++;
                buildingSetting.AddItem("fleece", 1);
            }
            harvesting = true;
        }
    }
}
