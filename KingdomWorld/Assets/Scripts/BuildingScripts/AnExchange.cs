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

    int RandomOpen;

    private void Start()
    {
        gM = gM.GetComponent<GameManager>();
    }

    private void Update()
    {
        OpenState();
    }

    void OpenState()
    {
        if (gM.isDaytime == true)
        {
            RandomOpen = Random.Range(0, 2);

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
        if (IsOpen == true)
        {
            AnExchangeUI.SetActive(true);
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
                Resource.Resource_Instance.Wheat -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                break;
            case 1:
                Resource.Resource_Instance.Food -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                break;
            case 2:
                Resource.Resource_Instance.Wood -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                break;
            case 3:
                Resource.Resource_Instance.Meat -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                break;
            case 4:
                Resource.Resource_Instance.Leather -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                break;
            case 5:
                Resource.Resource_Instance.Itronstone -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                break;
            case 6:
                Resource.Resource_Instance.CastIron -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                break;
            case 7:
                Resource.Resource_Instance.Cow -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                break;
            case 8:
                Resource.Resource_Instance.Sheep -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                break;
            case 9:
                Resource.Resource_Instance.Cheese -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                break;
            case 10:
                Resource.Resource_Instance.Fleece -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                break;
            case 11:
                Resource.Resource_Instance.Cloth -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                break;
            default:
                break;
        }
    }
}