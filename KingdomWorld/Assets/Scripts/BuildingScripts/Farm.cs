using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    private BuildingSetting buildingSetting;

    public int cow = 2;
    private int cowMax =10;
    public int sheep = 2;
    private int sheepMax = 10;

    private int milk = 0;
    private int milkMax = 10;
    private int fleece = 0;
    private int fleeceMax = 10;
    private bool isCheck = false;

    private float increaseInterval = 50f;
    private float timer = 0f;
    private float cooltime = 10f;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void AddCowSheep()
    {
        if(timer >= increaseInterval)
        {
            cow++;
            sheep++;
        }
    }

   /* public void Collection()
    {
        if(timer >= cooltime)
        {
            
        }
    }*/
}
