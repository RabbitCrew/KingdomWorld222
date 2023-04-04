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
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
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
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
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
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
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
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
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
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
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
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
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
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
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
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
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
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
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
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
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
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
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
                    SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
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
        //RaycastResult : BaseRaycastModule에서의 히트 결과.
        List<RaycastResult> results = new List<RaycastResult>();
        //EventSystem.current은 최근에 발생한 이벤트 시스템을 반환한다.
        //첫번째 인자값 : 현재 포인터 데이터.
        //두번째 인자값 : List of 'hits' to populate.
        //RaycastAll : 모두 설정된 BaseRaycaster를 사용을 통한 해당 씬으로의 레이 캐스팅.
        // -> 겹쳐있는 오브젝트들이 있다면 겹쳐있는 수로 results의 카운트가 바뀜
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        //Debug.Log(results.Count);
        return results.Count > 0;
    }
}