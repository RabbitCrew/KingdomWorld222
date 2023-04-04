using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cornfield : MonoBehaviour
{
    private int wheat = 0;
    private bool cultureCheck = false;

    private float increaseInterval = 10f;
    private float timer = 0f;

    private float decreaseProbability = 0.2f;
    private float decreaseRatio = 0.2f;
    private float increaseProbability = 0.2f;
    private float increaseRatio = 0.2f;


    BuildingSetting buildingSetting;

    public void WheatProduction()
    {
        if(timer >= increaseInterval)
        {
            timer = 0f;
            wheat = 10;
            RandomEvent();
            cultureCheck = true;
        }
    }

    public void RandomEvent()
    {
        float randomValue = Random.value;

        if(randomValue < decreaseProbability)
        {
            wheat = (int)(wheat * (1 - decreaseRatio));
            Debug.Log("20% 하락");
        }
        else if(randomValue < decreaseProbability + increaseProbability)
        {
            wheat = (int)(wheat * (1 + increaseRatio));
            Debug.Log("20% 증가");
        }
        else
        {
            return;
        }
    }


    private void Update()
    {
        timer += Time.deltaTime;

        if(cultureCheck == false)
        {
            WheatProduction();
        }
    }
}
