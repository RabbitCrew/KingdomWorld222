using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AniList : MonoBehaviour
{
    public Image[] AniImage;

    public Animation[] AniLists;

    public AnimationClip AniClip;

    int AniCount = 0;

    bool IsStart = false;

    float AniTime = 1.24f;
    float DefaultAniTime = 1.24f;
    float AniTimeSpace = 0.05f;

    private void Update()
    {
        if (IsStart == true)
        {
            AniTime -= Time.deltaTime;
            if (AniTime <= 0)
            {
                AniStart();

                AniTime = AniTimeSpace;
            }
        }
    }

    public void IsAniOn(bool value)
    {
        for (int i = 0; i < AniImage.Length; i++)
        {
            AniImage[i].fillAmount = 0;
        }

        IsStart = value;
        AniCount = 0;

        AniTime = DefaultAniTime;
    }

    void AniStart()
    {
        AniLists[AniCount].clip = AniClip;
        AniLists[AniCount].Play();

        if (AniCount < AniLists.Length - 1)
        {
            AniCount++;
        }
        else
        {
            AniCount = 0;
            IsStart = false;

            AniTime = DefaultAniTime;
        }
    }
}