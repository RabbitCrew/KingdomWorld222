using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building_NpcTag : MonoBehaviour
{
    public SpawnCitizen npc;

    public NPC Citizen;

    private float distance = 50f;
    public GameObject NPcButton;
    public GameObject RemoveNPcButton;
    public GameObject NPcButtonOther;
    public GameObject RemoveNPcButtonOther;

    public GameObject JobPanel;
    public GameObject NPCPanel;

    GameObject JobBuilding;

    string Job = null;
    string OtherJob = null;

    bool IsOther = false;

    private void Update()
    {
        BuildingCheck();
    }

    int count;

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

                        for (int j = 0; j< npc.CitizenList.Count; j++)
                        {
                            if (npc.CitizenList[j].GetComponent<NPC>().BuildingNum == JobBuilding)
                            {
                                count++;
                            }
                        }

                        for (int j = 0; j < count; j++)
                        {

                        }

                        if (hits[i].collider.gameObject.tag.Equals("Storage"))
                        {
                            Job = "StorageNPC";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("WoodCutter_house"))
                        {
                            Job = "WoodCutter";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Carpenter_house"))
                        {
                            Job = "CarpenterNPC";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Hunter_house"))
                        {

                            Job = "Hunter";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Farm_house"))
                        {
                            Job = "FarmNPC";

                            OtherJob = "Pastoralist";

                            IsOther = true;
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Mine_house"))
                        {
                            Job = "StoneMineWorker";

                            OtherJob = "IronMineWorker";

                            IsOther = true;
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Ham_house"))
                        {
                            Job = "HamNPC";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Cheese_house"))
                        {
                            Job = "CheeseNPC";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Cloth_house"))
                        {
                            Job = "FabricNPC";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Smith_house"))
                        {
                            Job = "Smith";
                        }
                    }
                }
            }
        }
    }

    public void JobSetOn()
    {
        if(IsOther == true)
        {
            NPcButtonOther.SetActive(true);
        }
        NPcButton.SetActive(true);
    }

    public void jobButton(bool value)
    {
        if (count <= 3)
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

                        break;
                    }
                    else if (value == false)
                    {
                        npc.CitizenList[i].tag = OtherJob;

                        Citizen.BuildingNum = JobBuilding;
                        Citizen.NPCBUildTrigger = true;

                        break;
                    }
                }
            }
        }
    }

    public void RemoveJobButton(bool value)
    {
        for (int i = 0; i < npc.CitizenList.Count; i++)
        {
            if(value == true)
            {
                if (npc.CitizenList[i].tag == Job)
                {
                    npc.CitizenList[i].tag = "NPC";

                    break;
                }
            }
            else if(value == false)
            {
                if (npc.CitizenList[i].tag == OtherJob)
                {
                    npc.CitizenList[i].tag = "NPC";

                    break;
                }
            }
           
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

    public void ExitButton()
    {
        JobPanel.SetActive(false);
    }
}