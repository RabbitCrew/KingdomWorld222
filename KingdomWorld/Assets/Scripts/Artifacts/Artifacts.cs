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
    public int[] HasArtifact = new int[30];
    int[] ArtifactNum = new int[30];

    public string[] ArtifactInfo; //유물 효과 설명
    public string[] ArtifactName;

    public GameObject[] TodayArtifactSet = new GameObject[3];
    public GameObject Smasege;
    public GameObject Inventory;

    public Sprite[] ArtifactImage;

    public TextMeshProUGUI PriceText;
    public TextMeshProUGUI InfoText;
    public TextMeshProUGUI[] NameText;

    private void Update()
    {
        SetTodayArtifact();
    }

    void SetTodayArtifact()//소지하고 있는 유물이 일정 갯수 이상ㅇ이면 안뜨게 수정해야됨. 아직 수정중이니까 건들면 문닷
    {
        if (GameManager.instance.dayNightRatio == 0f || GameManager.instance.dayNightRatio == 1f)
        {
            if (AnExchangeUI.IsOpen == true)// 거래소 문을 열면 
            {
                for (int i = 0; i < TodayArtifact.Length; i++)
                {
                    TodayArtifact[i] = Random.Range(0, ArtifactNums);// 유물 목록에서 랜덤으로 유물을 가져옴

                    TodayArtifactSet[i].GetComponent<SpriteRenderer>().sprite = ArtifactImage[TodayArtifact[i]];
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

            HasArtifact[TodayArtifact[AtNum]] += 1;
        }
        else
        {
            Smasege.SendMessage("MessageQ", "금화가 부족합니다");
        }
    }

    public void InventoryOn() // 가진 유물을 인벤토리에 표기.
    {
        int count = 0;

        if (Inventory != null)//인벤토리 오브젝트가 비어 있지 않을 때
        {
            Inventory.SetActive(true);//버튼 클릭되면 인벤토리가 켜지게.

            for (int i = 0; i < HasArtifact.Length; i++)
            {
                if (HasArtifact[i] >= 1)//해당 유물을 한개 이상 가지고 있다는 게 확인되면
                {
                    Inventory.transform.GetChild(count).gameObject.SetActive(true);//인벤토리 하위에 있는 오브젝트 켜주기.

                    Inventory.transform.GetChild(count).gameObject.
                        GetComponent<SpriteRenderer>().sprite = ArtifactImage[i];//이미지 맞는거 넣어주고

                    Inventory.transform.GetChild(count).gameObject.SendMessage("ArtifactEffect", i);//기능 실행하도록 명령.

                    count++; //인덱스 쁠쁠
                }
            }
        }
    }
}