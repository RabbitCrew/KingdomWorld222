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

    public int ResourceCount;
    private float distance = 50f;

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
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray, distance);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.GetComponent<BuildingColider>() != null)
                {
                    if (hits[i].collider.gameObject.tag.Equals("AnExchange") && hits[i].transform.GetComponent<BuildingColider>().isSettingComplete)
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