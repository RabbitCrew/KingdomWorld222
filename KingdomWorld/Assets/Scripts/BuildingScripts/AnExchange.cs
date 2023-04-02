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
                GameManager.instance.Wheat -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 1:
                GameManager.instance.Food -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 2:
                GameManager.instance.Wood -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 3:
                GameManager.instance.Meat -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 4:
                GameManager.instance.Leather -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 5:
                GameManager.instance.Itronstone -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 6:
                GameManager.instance.CastIron -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 7:
                GameManager.instance.Cow -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 8:
                GameManager.instance.Sheep -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 9:
                GameManager.instance.Cheese -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 10:
                GameManager.instance.Fleece -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 11:
                GameManager.instance.Cloth -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            default:
                break;
        }
    }
}