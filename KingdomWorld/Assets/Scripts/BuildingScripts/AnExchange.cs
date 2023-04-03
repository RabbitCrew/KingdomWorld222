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
            if(AnExchangeBP.transform.GetChild(i).name == "Anexchange")
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
                Resource.Resource_Instance.Wheat -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                if (Resource.Resource_Instance.Wheat >= ResourceCount)
                {
                    Resource.Resource_Instance.Wheat -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 1:
                Resource.Resource_Instance.Food -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                if (Resource.Resource_Instance.Food >= ResourceCount)
                {
                    Resource.Resource_Instance.Food -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 2:
                Resource.Resource_Instance.Wood -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                if (Resource.Resource_Instance.Wood >= ResourceCount)
                {
                    Resource.Resource_Instance.Wood -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 3:
                Resource.Resource_Instance.Meat -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                if (Resource.Resource_Instance.Meat >= ResourceCount)
                {
                    Resource.Resource_Instance.Meat -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 4:
                Resource.Resource_Instance.Leather -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                if (Resource.Resource_Instance.Leather >= ResourceCount)
                {
                    Resource.Resource_Instance.Leather -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 5:
                Resource.Resource_Instance.Itronstone -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                if (Resource.Resource_Instance.Itronstone >= ResourceCount)
                {
                    Resource.Resource_Instance.Itronstone -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 6:
                Resource.Resource_Instance.CastIron -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                if (Resource.Resource_Instance.CastIron >= ResourceCount)
                {
                    Resource.Resource_Instance.CastIron -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 7:
                Resource.Resource_Instance.Cow -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                if (Resource.Resource_Instance.Cow >= ResourceCount)
                {
                    Resource.Resource_Instance.Cow -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 8:
                Resource.Resource_Instance.Sheep -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                if (Resource.Resource_Instance.Sheep >= ResourceCount)
                {
                    Resource.Resource_Instance.Sheep -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 9:
                Resource.Resource_Instance.Cheese -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                if (Resource.Resource_Instance.Cheese >= ResourceCount)
                {
                    Resource.Resource_Instance.Cheese -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 10:
                Resource.Resource_Instance.Fleece -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                if (Resource.Resource_Instance.Fleece >= ResourceCount)
                {
                    Resource.Resource_Instance.Fleece -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
                }
                else
                {
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
                }
                break;
            case 11:
                Resource.Resource_Instance.Cloth -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                if (Resource.Resource_Instance.Cloth >= ResourceCount)
                {
                    Resource.Resource_Instance.Cloth -= ResourceCount;
                    Resource.Resource_Instance.Gold += ResourceCount;
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
}                Resource.Resource_Instance.Wheat -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                GameManager.instance.Wheat -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 1:
                Resource.Resource_Instance.Food -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                GameManager.instance.Food -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 2:
                Resource.Resource_Instance.Wood -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                GameManager.instance.Wood -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 3:
                Resource.Resource_Instance.Meat -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                GameManager.instance.Meat -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 4:
                Resource.Resource_Instance.Leather -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                GameManager.instance.Leather -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 5:
                Resource.Resource_Instance.Itronstone -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                GameManager.instance.Itronstone -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 6:
                Resource.Resource_Instance.CastIron -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                GameManager.instance.CastIron -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 7:
                Resource.Resource_Instance.Cow -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                GameManager.instance.Cow -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 8:
                Resource.Resource_Instance.Sheep -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                GameManager.instance.Sheep -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 9:
                Resource.Resource_Instance.Cheese -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                GameManager.instance.Cheese -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 10:
                Resource.Resource_Instance.Fleece -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                GameManager.instance.Fleece -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            case 11:
                Resource.Resource_Instance.Cloth -= ResourceCount;
                Resource.Resource_Instance.Gold += ResourceCount;
                GameManager.instance.Cloth -= ResourceCount;
                GameManager.instance.Gold += ResourceCount;
                break;
            default:
                break;
        }
    }
}