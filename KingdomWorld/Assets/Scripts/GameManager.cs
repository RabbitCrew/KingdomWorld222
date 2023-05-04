using ObjectNS;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : Resource
{
    [SerializeField] private PerlinNoiseMapMaker perlinNoise;
    [SerializeField] private HumanListPanel humanListPanel;
    private float startTime;
    private float timeElapsed;
    public float dayNightRatio { get; private set; }

    public bool isDaytime { get; private set; }//true 낮, false 밤
    public float uiSizeX { get; private set; }
    public float uiSizeY { get; private set; }
    public float timeSpeed { get; set; }
    public bool isWinterComing { get; set; }
    public bool isRain { get; set; }
    public bool GameStop = false;
    /// <summary>
    public float DayTime { get; set; }
    /// <summary>
    /// 현재 건물 개수에 따른 할당 가능한 직업의 빈자리
    /// </summary>
    public Dictionary<JobNum, int> jobCountDic = new Dictionary<JobNum, int>();

    public List<GameObject> RestHuman = new List<GameObject>();
    public List<GameObject> WheatList = new List<GameObject>();
    public List<GameObject> WaitingBuildingList = new List<GameObject>();
    public List<GameObject> StorageList = new List<GameObject>();
    public List<GameObject> FullResourceBuildingList = new List<GameObject>();
    public float dayLength; 
    public static GameManager instance;
    private void Awake()
    {
        uiSizeX = 1920;
        uiSizeY = 1080;
        timeSpeed = 1;
        dayLength = 180f;
        instance = this;
        isWinterComing = false;
        isRain = false;
        for (int i = 0; i < System.Enum.GetValues(typeof(ObjectTypeNum)).Length; i++)
        DayTime = 2f / 3f;
        for (int i = 0; i < System.Enum.GetValues(typeof(ObjectTypeNum)).Length; i++)
        {
            if (i == 4 || i == 0) { jobCountDic.Add((JobNum)i, 10000); }
            else
            {
                jobCountDic.Add((JobNum)i, 0);
            }
        }
    }
    void Start()
    {
        //InitializeGrid(500, 500);
        Food = 10;
        Wood = 50;
        perlinNoise.InitStart();
        Invoke("InitGrid", Time.deltaTime * 3f);
        //startTime = Time.realtimeSinceStartup;
        isDaytime = true;
    }

    private void expensed()
    {
        Food -= AllHuman.Count * 10;
    }
    private void winterExpensed()
    {
        if(isWinterComing)
            Wood -= (AllHuman.Count*2);
    }
    private void endListener()
    {
        if(AllHuman.Count >= 1000)
        {
            Debug.Log("게임클리어");
        }
    }
    private void InitGrid()
    {
        InitializeGrid(500, 500);
    }
    public void AddAllHumanList(GameObject obj)
    {
        AllHuman.Add(obj);
        //humanListPanel.UpdateHumanList();
    }


    void Update()
    {
        if (GameStop) { return; }

        //timeElapsed = Time.realtimeSinceStartup - startTime;
        dayNightRatio += (Time.deltaTime / dayLength) * timeSpeed; // 180 seconds = 3 minutes
        if (dayNightRatio >= 1f)
        {
            //startTime = Time.realtimeSinceStartup;
            dayNightRatio = 0f;
            expensed();
            winterExpensed();
            isDaytime = true;
        }
        else if (dayNightRatio >= DayTime)
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