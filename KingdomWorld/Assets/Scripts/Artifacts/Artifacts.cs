using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Artifacts : NPCParameter
{
    //유물 구매
    public AnExchange AnExchangeUI;

    int ArtifactNums = 30;

    public int[] TodayArtifact = new int[3];
    public int[] ArtifactPrice;
    public GameObject[] TodayArtifactSet = new GameObject[3];
    public Sprite[] ArtifactImage;

    public TextMeshProUGUI PriceText;

    private void Update()
    {
        SetTodayArtifact();
    }

    void SetTodayArtifact()
    {
        if (GameManager.instance.dayNightRatio == 0f || GameManager.instance.dayNightRatio == 1f)
        {
            if (AnExchangeUI.IsOpen == true)
            {
                for (int i = 0; i < TodayArtifact.Length; i++)
                {
                    TodayArtifact[i] = Random.Range(0, ArtifactNums);

                    TodayArtifactSet[i].GetComponent <SpriteRenderer>().sprite = ArtifactImage[TodayArtifact[i]];
                }
            }
        }
    }

    public void BuyArtifact(int ANum)
    {
        PriceText.text = ArtifactPrice[TodayArtifact[ANum]].ToString();
    }
}