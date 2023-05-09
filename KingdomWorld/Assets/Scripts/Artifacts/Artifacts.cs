using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Artifacts : MonoBehaviour
{
    //���� ���� ��ũ��Ʈ
    public NPC npcState;

    public AnExchange AnExchangeUI;

    [SerializeField] int ArtifactNums = 10;
    int AtNum;
    public int[] TodayArtifact = new int[3];
    public int[] ArtifactPrice;


    public string[] ArtifactInfo; //���� ȿ�� ����
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
            TodayArtifact[i] = Random.Range(0, ArtifactNums);// ���� ��Ͽ��� �������� ������ ������

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

    void SetTodayArtifact()//�����ϰ� �ִ� ������ ���� ���� �̻��̸� �ȶ߰� �����ؾߵ�.
    {
        if (GameManager.instance.dayNightRatio == 0f || GameManager.instance.dayNightRatio == 1f)
        {
            if (AnExchangeUI.IsOpen == true)// �ŷ��� ���� ���� 
            {
                for (int i = 0; i < TodayArtifact.Length; i++)
                {
                    TodayArtifact[i] = Random.Range(0, ArtifactNums);// ���� ��Ͽ��� �������� ������ ������

                    while(Inventory.instance.HasArtifact[TodayArtifact[i]] == Inventory.instance.ArtifactLimit[TodayArtifact[i]])
                    {
                        TodayArtifact[i] = Random.Range(0, ArtifactNums);// ���� ��Ͽ��� �������� ������ ������
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

        PriceText.text = "���� : " + (ArtifactPrice[TodayArtifact[AtNum]] * AnExchangeUI.ExchangeRate).ToString() + " ���";
    }

    public void ShowArtifactInfo(int ANum) //�ŷ��ҿ��� ���� ���� ǥ��
    {
        AtNum = ANum;

        PriceText.text = "���� : " + (ArtifactPrice[TodayArtifact[ANum]] * AnExchangeUI.ExchangeRate).ToString() + " ���";
        InfoText.text = ArtifactInfo[TodayArtifact[ANum]].ToString();
        NameText[ANum].text = ArtifactName[TodayArtifact[ANum]].ToString();

        NegoB.interactable = true;
    }

    public void BuyArtifact()//���� ������ ���ŵǰ� �ƴϸ� �źεǰ�.
    {
        if ((ArtifactPrice[TodayArtifact[AtNum]] * AnExchangeUI.ExchangeRate) <= GameManager.instance.Gold)
        {
            Debug.Log(ArtifactPrice[TodayArtifact[AtNum]] * AnExchangeUI.ExchangeRate);
            Debug.Log(GameManager.instance.Gold);

            GameManager.instance.Gold -= ArtifactPrice[TodayArtifact[AtNum]];

            Smasege.SendMessage("MessageQ", "������ �� �̿����ּ���!");

            TodayArtifactSet[AtNum].GetComponent<Button>().interactable = false;

            Inventory.instance.HasArtifact[TodayArtifact[AtNum]] += 1;
        }
        else
        {
            Smasege.SendMessage("MessageQ", "��ȭ�� �����մϴ�");
        }
    }
}