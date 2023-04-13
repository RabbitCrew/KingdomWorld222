using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterIsComing : MonoBehaviour
{
    private int winterCount;
    private bool isOneDay;
    // Start is called before the first frame update
    void Start()
    {
        isOneDay = false;
        winterCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isDaytime && !isOneDay)
        {
            winterCount -= 1;
            isOneDay = true;
        }
        else if (!GameManager.instance.isDaytime)
        {
            isOneDay = false;
        }

        if (winterCount == 0) 
        {
            Time.timeScale = 0;
            Debug.Log("겨울이 온다..."); 
        }
    }
}
