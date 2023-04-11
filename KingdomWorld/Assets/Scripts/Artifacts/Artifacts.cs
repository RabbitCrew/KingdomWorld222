using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Artifacts : NPCParameter
{
    //유물 구매 스크립트
    public AnExchange AnExchangeUI;

    [SerializeField]int ArtifactNums = 30;
    int AtNum;
    public int[] TodayArtifact = new int[3];
    public int[] ArtifactPrice;
    public int[] HasArtifact = new int[30];

    public string[] ArtifactInfo;
    public string[] ArtifactName;

    public GameObject[] TodayArtifactSet = new GameObject[3];
    public GameObject Smasege;
    public GameObject Inventory;

    public Sprite[] ArtifactImage;

    public TextMeshProUGUI PriceText;
    public TextMeshProUGUI InfoText;
    public TextMeshProUGUI NameText;

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

                    TodayArtifactSet[i].GetComponent<SpriteRenderer>().sprite = ArtifactImage[TodayArtifact[i]];
                }
            }
        }
    }

    public void ShowArtifactInfo(int ANum)
    {
        AtNum = ANum;

        PriceText.text = ArtifactPrice[TodayArtifact[ANum]].ToString();
        InfoText.text = ArtifactInfo[TodayArtifact[ANum]];
        NameText.text = ArtifactName[TodayArtifact[ANum]];
    }

    public void BuyArtifact()
    {
        if(ArtifactPrice[AtNum] * AnExchangeUI.ExchangeRate >= GameManager.instance.Gold)
        {
            GameManager.instance.Gold -= ArtifactPrice[TodayArtifact[AtNum]];

            Smasege.SendMessage("MessageQ", "구매해주셔서 감사합니다!");

            TodayArtifactSet[AtNum].GetComponent<Button>().interactable = false;

            HasArtifact[TodayArtifact[AtNum]] += 1;
        }
        else
        {
            Smasege.SendMessage("MessageQ", "금화가 부족합니다");
        }
    }

    public void InventoryOn()
    {
        int count = 0;

        if (Inventory != null)
        {
            Inventory.SetActive(true);

            for (int i = 0; i < HasArtifact.Length; i++)
            {
                if (HasArtifact[i] >= 1)
                {
                    Inventory.transform.GetChild(count).gameObject.SetActive(true);

                    Inventory.transform.GetChild(count).gameObject.
                        GetComponent<SpriteRenderer>().sprite = ArtifactImage[i];

                    count++;
                }
            }
        }
    }
}