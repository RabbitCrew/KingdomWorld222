using ObjectNS;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : Resource
{
    [SerializeField] private PerlinNoiseMapMaker perlinNoise;
    [SerializeField] private HumanListPanel humanListPanel;
    [SerializeField] private TutorialPanel tutorialPanel;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private DayPanel dayPanel;
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
    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }

        uiSizeX = 1920;
        uiSizeY = 1080;
        timeSpeed = 1;
        dayLength = 180f;

        isWinterComing = false;
        isRain = false;
        for (int i = 0; i < System.Enum.GetValues(typeof(ObjectTypeNum)).Length; i++)
        DayTime = 2f / 3f;
        for (int i = 0; i < System.Enum.GetValues(typeof(ObjectTypeNum)).Length; i++)
        {
            if (i == 4 || i == 0 || i == 7) { jobCountDic.Add((JobNum)i, 10000); }
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
        tutorialPanel.UnLockAllButton();
        tutorialPanel.unLockSpawn = true;
        //if (!tutorialPanel.StartTutorial)
        //{
        //    GameManager.instance.GameStop = true;
        //    uiManager.SetAcitveTutoPanel(true);
        //    tutorialPanel.StartTuto(); 
        //}
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
        if(AllHuman.Count >= 500)
        {
            Debug.Log("게임클리어");
        }
    }
    private void InitGrid()
    {
        InitializeGrid(1000, 1000);
    }
    public void AddAllHumanList(GameObject obj)
    {
        AllHuman.Add(obj);
        //humanListPanel.UpdateHumanList();
    }

    public TutorialPanel ReturnTutorialPanel()
	{
        return tutorialPanel;
	}

    void Update()
    {
        if(FullResourceBuildingList.Count > 0 && FullResourceBuildingList[0] == null)
        {
            FullResourceBuildingList.RemoveAt(0);
        }
        if (GameStop) { return; }

        //timeElapsed = Time.realtimeSinceStartup - startTime;
        dayNightRatio += (Time.deltaTime / dayLength) * timeSpeed; // 180 seconds = 3 minutes
        if (dayNightRatio >= 1f)
        {
            //startTime = Time.realtimeSinceStartup;
            dayNightRatio = 0f;
            dayPanel.CountDay();
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