using UnityEngine;
using System.Collections.Generic;
public class GameManager : Resource
{
    private float startTime;
    private float timeElapsed;
    public float dayNightRatio { get; private set; }
    public bool isDaytime { get; private set; }//true�϶� ��, false�϶� ��

    List<GameObject> RestHuman= new List<GameObject>();

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        startTime = Time.realtimeSinceStartup;
        isDaytime = true;
    }

    void Update()
    {
        timeElapsed = Time.realtimeSinceStartup - startTime;
        dayNightRatio = timeElapsed / 180f; // 180 seconds = 3 minutes

        if (dayNightRatio >= 1f)
        {
            startTime = Time.realtimeSinceStartup;
            dayNightRatio = 0f;
            isDaytime = true;
        }
        else if (dayNightRatio >= 2f / 3f)
        {
                //Debug.Log("��");
                isDaytime = false;
        }
        else if (dayNightRatio >= 0f)
        {
            //Debug.Log("��");
            isDaytime = true;
        }
    }
}