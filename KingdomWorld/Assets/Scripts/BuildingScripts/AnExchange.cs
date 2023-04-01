using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnExchange : MonoBehaviour
{
    public GameObject AnExchangeUI;

    public TMP_Dropdown CellThings;

    public int ResourceCount;

    public bool IsOpen;

    public GameManager gM;
    public GameObject SMassage;

    public int RandomOpen;

    private void Start()
    {
        gM = gM.GetComponent<GameManager>();
    }

    private void Update()
    {
        OpenState();

        RIsOpen();
    }

    void RIsOpen()
    {
        if (gM.dayNightRatio == 1f || gM.dayNightRatio == 0f)
        {
            RandomOpen = Random.Range(0, 2);
        }
    }

    void OpenState()
    {
        if (gM.isDaytime == true)
        {
            if (RandomOpen == 0)
            {
                IsOpen = true;
            }
            else
            {
                IsOpen = false;
            }
        }
        else
        {
            IsOpen = false;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log(IsOpen);

        if (IsOpen == true)
        {
            AnExchangeUI.SetActive(true);
        }
        else
        {
            SMassage.SendMessage("MessageQ", "������ ������ ���� ���Դϴ�");
        }
    }

    public void GetQuantity(string value)
    {
        ResourceCount = int.Parse(value);
    }

    public void CellResource()
    {
        int CellValue = CellThings.value;

        switch (CellValue)
        {
            case 0:
                if (Resource.Resource_Instance.Wheat >= ResourceCount)
                {
                    Resource.Resource_Instance.Wheat -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
                }
                break;
            case 1:
                if (Resource.Resource_Instance.Food >= ResourceCount)
                {
                    Resource.Resource_Instance.Food -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
                }
                break;
            case 2:
                if (Resource.Resource_Instance.Wood >= ResourceCount)
                {
                    Resource.Resource_Instance.Wood -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
                }
                break;
            case 3:
                if (Resource.Resource_Instance.Meat >= ResourceCount)
                {
                    Resource.Resource_Instance.Meat -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
                }
                break;
            case 4:
                if (Resource.Resource_Instance.Leather >= ResourceCount)
                {
                    Resource.Resource_Instance.Leather -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
                }
                break;
            case 5:
                if (Resource.Resource_Instance.Itronstone >= ResourceCount)
                {
                    Resource.Resource_Instance.Itronstone -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
                }
                break;
            case 6:
                if (Resource.Resource_Instance.CastIron >= ResourceCount)
                {
                    Resource.Resource_Instance.CastIron -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
                }
                break;
            case 7:
                if (Resource.Resource_Instance.Cow >= ResourceCount)
                {
                    Resource.Resource_Instance.Cow -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
                }
                break;
            case 8:
                if (Resource.Resource_Instance.Sheep >= ResourceCount)
                {
                    Resource.Resource_Instance.Sheep -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
                }
                break;
            case 9:
                if (Resource.Resource_Instance.Cheese >= ResourceCount)
                {
                    Resource.Resource_Instance.Cheese -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
                }
                break;
            case 10:
                if (Resource.Resource_Instance.Fleece >= ResourceCount)
                {
                    Resource.Resource_Instance.Fleece -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
                }
                break;
            case 11:
                if (Resource.Resource_Instance.Cloth >= ResourceCount)
                {
                    Resource.Resource_Instance.Cloth -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
                }
                break;
            default:
                break;
        }
    }
}