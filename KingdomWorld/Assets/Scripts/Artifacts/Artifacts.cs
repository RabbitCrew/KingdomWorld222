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

    [SerializeField] int ArtifactNums = 30;
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

    private void Update()
    {
        SetTodayArtifact();
    }

    void SetTodayArtifact()//�����ϰ� �ִ� ������ ���� ���� �̻��̸� �ȶ߰� �����ؾߵ�. ���� �������̴ϱ� �ǵ�� ���� ���� ��ü �����ϰ� ����ž�
    {
        if (GameManager.instance.dayNightRatio == 0f || GameManager.instance.dayNightRatio == 1f)
        {
            if (AnExchangeUI.IsOpen == true)// �ŷ��� ���� ���� 
            {
                for (int i = 0; i < TodayArtifact.Length; i++)
                {
                    TodayArtifact[i] = Random.Range(0, ArtifactNums);// ���� ��Ͽ��� �������� ������ ������

                    TodayArtifactSet[i].GetComponent<SpriteRenderer>().sprite = Inventory.instance.ArtifactImage[TodayArtifact[i]];
                }
            }
        }
    }

    public void ShowArtifactInfo(int ANum) //�ŷ��ҿ��� ���� ���� ǥ��
    {
        AtNum = ANum;

        PriceText.text = (ArtifactPrice[TodayArtifact[ANum]] * AnExchangeUI.ExchangeRate).ToString();
        InfoText.text = ArtifactInfo[TodayArtifact[ANum]];
        NameText[ANum].text = ArtifactName[TodayArtifact[ANum]];
    }

    public void BuyArtifact()//���� ������ ���ŵǰ� �ƴϸ� �źεǰ�.
    {
        if (ArtifactPrice[AtNum] * AnExchangeUI.ExchangeRate >= GameManager.instance.Gold)
        {
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