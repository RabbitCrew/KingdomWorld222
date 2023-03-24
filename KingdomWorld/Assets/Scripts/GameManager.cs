using UnityEngine;

public class GameManager : Resource
{
    private float startTime;
    private float timeElapsed;
    private float dayNightRatio;
    private bool isDaytime;//true老锭 撤, false老锭 广

    void Start()
    {
        startTime = Time.realtimeSinceStartup;
        isDaytime = true;
    }

    void Update()
    {
        timeElapsed = Time.realtimeSinceStartup - startTime;
        dayNightRatio = timeElapsed / 3f; // 180 seconds = 3 minutes

        //Debug.Log(dayNightRatio);
        if (dayNightRatio >= 1f)
        {
            startTime = Time.realtimeSinceStartup;
            dayNightRatio = 0f;
            isDaytime = true;
        }
        else if (dayNightRatio >= 2f / 3f)
        {
                Debug.Log("广");
                isDaytime = false;
        }
        else if (dayNightRatio >= 0f)
        {
            Debug.Log("撤");
            isDaytime = true;
        }
    }
}