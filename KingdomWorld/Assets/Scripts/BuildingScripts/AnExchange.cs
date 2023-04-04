using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnExchange : MonoBehaviour
{
    public GameObject AnExchangeUI;
    public Transform AnExchangeBP;
    public GameObject AnExchangeB;

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

        BuildFound();

        ClickCheck();
    }

    void ClickCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.name == "An exchange")
                {
                    if (IsOpen == true)
                    {
                        AnExchangeUI.SetActive(true);

                        SMassage.SendMessage("MessageQ", "문이 열렸습니다");
                    }
                    else
                    {
                        AnExchangeUI.SetActive(false);

                        SMassage.SendMessage("MessageQ", "오늘은 상인이 없는 날입니다");
                    }
                }
            }
        }
    }

    void BuildFound()
    {
        for (int i = 0; i < AnExchangeBP.transform.childCount; i++)
        {
            if(AnExchangeBP.transform.GetChild(i).name == "An exchange")
            {
                AnExchangeB = AnExchangeBP.transform.GetChild(i).gameObject;
            }
        }
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
                if (GameManager.instance.Wheat >= ResourceCount)
                {
                    GameManager.instance.Wheat -= ResourceCount;
                    GameManager.instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 1:
                if (GameManager.instance.Food >= ResourceCount)
                {
                    GameManager.instance.Food -= ResourceCount;
                    GameManager.instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 2:
                if (GameManager.instance.Wood >= ResourceCount)
                {
                    GameManager.instance.Wood -= ResourceCount;
                    GameManager.instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 3:
                if (GameManager.instance.Meat >= ResourceCount)
                {
                    GameManager.instance.Meat -= ResourceCount;
                    GameManager.instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 4:
                if (GameManager.instance.Leather >= ResourceCount)
                {
                    GameManager.instance.Leather -= ResourceCount;
                    GameManager.instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 5:
                if (GameManager.instance.Itronstone >= ResourceCount)
                {
                    GameManager.instance.Itronstone -= ResourceCount;
                    GameManager.instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 6:
                if (GameManager.instance.CastIron >= ResourceCount)
                {
                    GameManager.instance.CastIron -= ResourceCount;
                    GameManager.instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 7:
                if (GameManager.instance.Cow >= ResourceCount)
                {
                    GameManager.instance.Cow -= ResourceCount;
                    GameManager.instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 8:
                if (GameManager.instance.Sheep >= ResourceCount)
                {
                    GameManager.instance.Sheep -= ResourceCount;
                    GameManager.instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 9:
                if (GameManager.instance.Cheese >= ResourceCount)
                {
                    GameManager.instance.Cheese -= ResourceCount;
                    GameManager.instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 10:
                if (GameManager.instance.Fleece >= ResourceCount)
                {
                    GameManager.instance.Fleece -= ResourceCount;
                    GameManager.instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 11:
                if (GameManager.instance.Cloth >= ResourceCount)
                {
                    GameManager.instance.Cloth -= ResourceCount;
                    GameManager.instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            default:
                break;
        }
    }
}