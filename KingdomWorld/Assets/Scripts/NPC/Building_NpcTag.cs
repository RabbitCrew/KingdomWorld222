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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);// ray�� ���콺 ������ �� ���콺 ��ġ �޾ƿ�

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray, distance);

            for (int i = 0; i < hits.Length; i++) // ���̷� Ŭ���� �κ��� ������Ʈ ������
            {
                if (hits[i].collider.GetComponent<BuildingColider>() != null) // �ǹ��� �Ҵ��� �ݶ��̴��� ���� ��
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

    public void ExitButton()
    {
        JobPanel.SetActive(false);
    }
}