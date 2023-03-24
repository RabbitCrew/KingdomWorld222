using UnityEngine;

public class GameManager : Resource
{
    private float startTime;
    private float timeElapsed;
    private float dayNightRatio;
    private bool isDaytime;//true�϶� ��, false�϶� ��

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
                Debug.Log("��");
                isDaytime = false;
        }
        else if (dayNightRatio >= 0f)
        {
            Debug.Log("��");
            isDaytime = true;
        }
    }
}