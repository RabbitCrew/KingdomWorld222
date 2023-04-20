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

    [SerializeField] int ArtifactNums = 30;
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

    private void Update()
    {
        SetTodayArtifact();
    }

    void SetTodayArtifact()//소지하고 있는 유물이 일정 갯수 이상ㅇ이면 안뜨게 수정해야됨. 아직 수정중이니까 건들면 문닷 유물 전체 구현하고 만들거양
    {
        if (GameManager.instance.dayNightRatio == 0f || GameManager.instance.dayNightRatio == 1f)
        {
            if (AnExchangeUI.IsOpen == true)// 거래소 문을 열면 
            {
                for (int i = 0; i < TodayArtifact.Length; i++)
                {
                    TodayArtifact[i] = Random.Range(0, ArtifactNums);// 유물 목록에서 랜덤으로 유물을 가져옴

                    TodayArtifactSet[i].GetComponent<SpriteRenderer>().sprite = Inventory.instance.ArtifactImage[TodayArtifact[i]];
                }
            }
        }
    }

    public void ShowArtifactInfo(int ANum) //거래소에서 유물 설명 표기
    {
        AtNum = ANum;

        PriceText.text = (ArtifactPrice[TodayArtifact[ANum]] * AnExchangeUI.ExchangeRate).ToString();
        InfoText.text = ArtifactInfo[TodayArtifact[ANum]];
        NameText[ANum].text = ArtifactName[TodayArtifact[ANum]];
    }

    public void BuyArtifact()//돈이 있으면 구매되고 아니면 거부되게.
    {
        if (ArtifactPrice[AtNum] * AnExchangeUI.ExchangeRate >= GameManager.instance.Gold)
        {
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