using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class AnExchange : MonoBehaviour
{
    public GameObject AnExchangeUI;
    public Transform AnExchangeBP;
    public GameObject AnExchangeB;

    public TMP_Dropdown CellThings;
    private float distance = 50f;

    public int ResourceCount;

    public bool IsOpen;

    public GameObject SMassage;

    public int RandomOpen;

    private void Update()
    {
        OpenState();

        RIsOpen();

        BuildFound();

        ClickCheck();
    }

    void ClickCheck()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray, distance);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.GetComponent<BuildingColider>() != null)
                {
                    if (hits[i].collider.gameObject.tag.Equals("AnExchange") && hits[i].collider.GetComponent<BuildingColider>().isSettingComplete)
                    {
                        if (IsOpen == true)
                        {
                            AnExchangeUI.SetActive(true);

                            SMassage.SendMessage("MessageQ", "���� ���Ƚ��ϴ�");
                        }
                        else
                        {
                            AnExchangeUI.SetActive(false);

                            SMassage.SendMessage("MessageQ", "������ ������ ���� ���Դϴ�");
                        }
                    }
                }
            }
        }
    }

    void BuildFound()
    {
        for (int i = 0; i < AnExchangeBP.transform.childCount; i++)
        {
            if(AnExchangeBP.transform.GetChild(i).tag.Equals("AnExchange"))
            {
                AnExchangeB = AnExchangeBP.transform.GetChild(i).gameObject;
            }
        }
    }

    void RIsOpen()
    {
        if (GameManager.instance.dayNightRatio == 1f || GameManager.instance.dayNightRatio == 0f)
        {
            RandomOpen = Random.Range(0, 2);
        }
    }

    void OpenState()
    {
        if (GameManager.instance.isDaytime == true)
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
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
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
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
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
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
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
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
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
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
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
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
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
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
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
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
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
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
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
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
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
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
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
                    SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
                }
                break;
            default:
                break;
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //RaycastResult : BaseRaycastModule������ ��Ʈ ���.
        List<RaycastResult> results = new List<RaycastResult>();
        //EventSystem.current�� �ֱٿ� �߻��� �̺�Ʈ �ý����� ��ȯ�Ѵ�.
        //ù��° ���ڰ� : ���� ������ ������.
        //�ι�° ���ڰ� : List of 'hits' to populate.
        //RaycastAll : ��� ������ BaseRaycaster�� ����� ���� �ش� �������� ���� ĳ����.
        // -> �����ִ� ������Ʈ���� �ִٸ� �����ִ� ���� results�� ī��Ʈ�� �ٲ�
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        //Debug.Log(results.Count);
        return results.Count > 0;
    }
}