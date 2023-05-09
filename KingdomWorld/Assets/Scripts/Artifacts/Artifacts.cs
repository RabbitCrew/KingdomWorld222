using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Artifacts : MonoBehaviour
{
    //유물 구매 스크립트
    public NPC npcState;

    public AnExchange AnExchangeUI;

    [SerializeField] int ArtifactNums = 10;
    int AtNum;
    public int[] TodayArtifact = new int[3];
    public int[] ArtifactPrice;


    public string[] ArtifactInfo; //유물 효과 설명
    public string[] ArtifactName;

    public GameObject[] TodayArtifactSet = new GameObject[3];
    public GameObject Smasege;

    public TextMeshProUGUI PriceText;
    public TextMeshProUGUI InfoText;
    public TextMeshProUGUI[] NameText;

    [SerializeField] Button NegoB;

    private void Update()
    {
        SetTodayArtifact();
    }

    private void Start()
    {
        for (int i = 0; i < TodayArtifact.Length; i++)
        {
            TodayArtifact[i] = Random.Range(0, ArtifactNums);// 유물 목록에서 랜덤으로 유물을 가져옴

            //TodayArtifactSet[i].GetComponent<SpriteRenderer>().sprite = Inventory.instance.ArtifactImage[TodayArtifact[i]];

            if(i > 0)
            {
                for (int j = 1; j < i; j++)
                {
                    while(TodayArtifact[i] == TodayArtifact[j])
                    {
                        TodayArtifact[i] = Random.Range(0, ArtifactNums);
                    }
                }
            }

            NameText[i].text = ArtifactName[TodayArtifact[i]].ToString();
        }

        NegoB.interactable = false;
    }

    void SetTodayArtifact()//소지하고 있는 유물이 일정 갯수 이상이면 안뜨게 수정해야됨.
    {
        if (GameManager.instance.dayNightRatio == 0f || GameManager.instance.dayNightRatio == 1f)
        {
            if (AnExchangeUI.IsOpen == true)// 거래소 문을 열면 
            {
                for (int i = 0; i < TodayArtifact.Length; i++)
                {
                    TodayArtifact[i] = Random.Range(0, ArtifactNums);// 유물 목록에서 랜덤으로 유물을 가져옴

                    while(Inventory.instance.HasArtifact[TodayArtifact[i]] == Inventory.instance.ArtifactLimit[TodayArtifact[i]])
                    {
                        TodayArtifact[i] = Random.Range(0, ArtifactNums);// 유물 목록에서 랜덤으로 유물을 가져옴
                    }

                    if (i > 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            while (TodayArtifact[i] == TodayArtifact[j])
                            {
                                TodayArtifact[i] = Random.Range(0, ArtifactNums);
                            }
                        }
                    }

                    TodayArtifactSet[i].GetComponent<SpriteRenderer>().sprite = Inventory.instance.ArtifactImage[TodayArtifact[i]];

                    NameText[i].text = ArtifactName[TodayArtifact[i]].ToString();
                }
            }
        }

        PriceText.text = "가격 : " + (ArtifactPrice[TodayArtifact[AtNum]] * AnExchangeUI.ExchangeRate).ToString() + " 골드";
    }

    public void ShowArtifactInfo(int ANum) //거래소에서 유물 설명 표기
    {
        AtNum = ANum;

        PriceText.text = "가격 : " + (ArtifactPrice[TodayArtifact[ANum]] * AnExchangeUI.ExchangeRate).ToString() + " 골드";
        InfoText.text = ArtifactInfo[TodayArtifact[ANum]].ToString();
        NameText[ANum].text = ArtifactName[TodayArtifact[ANum]].ToString();

        NegoB.interactable = true;
    }

    public void BuyArtifact()//돈이 있으면 구매되고 아니면 거부되게.
    {
        if ((ArtifactPrice[TodayArtifact[AtNum]] * AnExchangeUI.ExchangeRate) <= GameManager.instance.Gold)
        {
            Debug.Log(ArtifactPrice[TodayArtifact[AtNum]] * AnExchangeUI.ExchangeRate);
            Debug.Log(GameManager.instance.Gold);

            GameManager.instance.Gold -= ArtifactPrice[TodayArtifact[AtNum]];

            Smasege.SendMessage("MessageQ", "다음에 또 이용해주세요!");

            TodayArtifactSet[AtNum].GetComponent<Button>().interactable = false;

            Inventory.instance.HasArtifact[TodayArtifact[AtNum]] += 1;
        }
        else
        {
            Smasege.SendMessage("MessageQ", "금화가 부족합니다");
        }
    }
}