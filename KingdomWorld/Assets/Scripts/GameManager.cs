using UnityEngine;
using System.Collections.Generic;
public class GameManager : Resource
{
    private float startTime;
    private float timeElapsed;
    public float dayNightRatio { get; private set; }
    public bool isDaytime { get; private set; }//true 낮, false 밤
    public float uiSizeX { get; private set; }
    public float uiSizeY { get; private set; }
    public List<GameObject> RestHuman = new List<GameObject>();
    public List<GameObject> WheatList = new List<GameObject>();
    public List<GameObject> WaitingBuildingList = new List<GameObject>();
    public List<GameObject> StorageList = new List<GameObject>();
    public static GameManager instance;

    private void Awake()
    {
        uiSizeX = 1920;
        uiSizeY = 1080;

        instance = this;
        InitializeGrid(500, 500);
    }
    void Start()
    {
        startTime = Time.realtimeSinceStartup;
        isDaytime = true;
    }

    void Update()
    {
        timeElapsed = Time.realtimeSinceStartup - startTime;
        dayNightRatio = timeElapsed / 10f; // 180 seconds = 3 minutes

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