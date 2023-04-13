using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Building_NpcTag : MonoBehaviour
{
    public SpawnCitizen npc;
    public NPC Citizen;

    private float distance = 50f;
    public GameObject NPcButton;
    public GameObject RemoveNPcButton;
    public GameObject NPcButtonOther;
    public GameObject JobPanel;
    public GameObject[] NPCPanel;
    public GameObject SMassage;
    GameObject JobBuilding;
    GameObject[] IsSettedNPC;

    [SerializeField] string[] InputJobText;

    string Job = null;
    string OtherJob = null;

    bool IsOther = false;

    [SerializeField] TextMeshProUGUI JobText;
    [SerializeField] TextMeshProUGUI OtherJobText;

    private void Update()
    {
        BuildingCheck();

        NPCFound();
    }

    int count;
    int NPCCount;

    void NPCFound()
    {
        for (int j = 0; j < npc.CitizenList.Count; j++)
        {
            if (npc.CitizenList[j].GetComponent<NPC>().BuildingNum == JobBuilding)
            {
                NPCPanel[count].SetActive(true);

                NPCPanel[count].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                    npc.CitizenList[j].GetComponent<NPC>().BuildingNum.name;

                IsSettedNPC[count] = npc.CitizenList[j].GetComponent<NPC>().BuildingNum;

                count++;
            }
        }
    }

    void BuildingCheck()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);// ray로 마우스 눌렀을 때 마우스 위치 받아옴

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray, distance);

            for (int i = 0; i < hits.Length; i++) // 레이로 클릭한 부분의 오브젝트 뒤져서
            {
                if (hits[i].collider.GetComponent<BuildingColider>() != null) // 건물에 할당한 콜라이더가 있을 시
                {
                    if (hits[i].collider.GetComponent<BuildingColider>().isSettingComplete)
                    {
                        JobBuilding = hits[i].collider.gameObject;

                        JobPanel.SetActive(true);

                        IsOther = false;

                        count = 0;
                        NPCCount = 0;

                        if (hits[i].collider.gameObject.tag.Equals("Storage"))
                        {
                            Job = "StorageNPC";

                            NPCCount = 0;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("WoodCutter_house"))
                        {
                            Job = "WoodCutter";

                            NPCCount = 1;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Carpenter_house"))
                        {
                            Job = "CarpenterNPC";

                            NPCCount = 2;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Hunter_house"))
                        {
                            Job = "Hunter";

                            NPCCount = 3;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Farm_house"))
                        {
                            Job = "Pastoralist";

                            NPCCount = 4;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("WheatField"))
                        {
                            Job = "FarmNPC";

                            NPCCount = 5;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Mine_house"))
                        {
                            Job = "StoneMineWorker";

                            OtherJob = "IronMineWorker";

                            NPCCount = 6;

                            JobText.text = InputJobText[NPCCount];
                            OtherJobText.text = InputJobText[NPCCount];

                            IsOther = true;
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Ham_house"))
                        {
                            Job = "HamNPC";

                            NPCCount = 8;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Cheese_house"))
                        {
                            Job = "CheeseNPC";

                            NPCCount = 9;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Cloth_house"))
                        {
                            Job = "FabricNPC";

                            NPCCount = 10;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Smith_house"))
                        {
                            Job = "Smith";

                            NPCCount = 11;

                            JobText.text = InputJobText[NPCCount];
                        }
                    }
                }
            }
        }
    }

    public void JobSetOn()
    {
        JobPanel.SetActive(true);

        if(IsOther == true)
        {
            NPcButtonOther.SetActive(true);
        }
        NPcButton.SetActive(true);
    }

    public void jobButton(bool value)
    {
        if (count < 3)
        {
            for (int i = 0; i < npc.CitizenList.Count; i++)
            {
                if (npc.CitizenList[i].tag == "NPC")
                {
                    Citizen = npc.CitizenList[i].GetComponent<NPC>();

                    if (value == true)
                    {
                        npc.CitizenList[i].tag = Job;

                        Citizen.BuildingNum = JobBuilding;
                        Citizen.NPCBUildTrigger = true;

                        count++;

                        break;
                    }
                    else if (value == false)
                    {
                        npc.CitizenList[i].tag = OtherJob;

                        Citizen.BuildingNum = JobBuilding;
                        Citizen.NPCBUildTrigger = true;

                        count++;

                        break;
                    }
                }
            }
        }
        else if(count >= 3)
        {
            SMassage.SendMessage("MessageQ", "이미 인부의 수가 한계에 도달했습니다.");
        }
    }

    public void RemoveJobButton()
    {
        IsSettedNPC[count].tag = "NPC";

        count--;

        Citizen = IsSettedNPC[count].GetComponent<NPC>();

        Citizen.NPCBUildTrigger = false;
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

    public void ExitButton()
    {
        JobPanel.SetActive(false);

        for (int j = count - 1; j >= 0; j++)
        {
            NPCPanel[j].SetActive(false);
        }
    }
}