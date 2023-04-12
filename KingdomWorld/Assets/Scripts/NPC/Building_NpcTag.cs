using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building_NpcTag : MonoBehaviour
{
    public SpawnCitizen npc;
    
    private float distance = 50f;
    public GameObject NPcButton;
    public GameObject RemoveNPcButton;
    public GameObject NPcButtonOther;
    public GameObject RemoveNPcButtonOther;

    string Job = null;
    string OtherJob = null;

    private void Update()
    {
        BuildingCheck();
    }

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
                        if (hits[i].collider.gameObject.tag.Equals("Storage"))
                        {
                            NPcButton.SetActive(true);
                            RemoveNPcButton.SetActive(true);
                            NPcButtonOther.SetActive(false);
                            RemoveNPcButtonOther.SetActive(false);

                            Job = "StorageNPC";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("WoodCutter_house"))
                        {
                            NPcButton.SetActive(true);
                            RemoveNPcButton.SetActive(true);
                            NPcButtonOther.SetActive(false);
                            RemoveNPcButtonOther.SetActive(false);

                            Job = "WoodCutter";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Carpenter_house"))
                        {
                            NPcButton.SetActive(true);
                            RemoveNPcButton.SetActive(true);
                            NPcButtonOther.SetActive(false);
                            RemoveNPcButtonOther.SetActive(false);

                            Job = "CarpenterNPC";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Hunter_house"))
                        {
                            NPcButton.SetActive(true);
                            RemoveNPcButton.SetActive(true);
                            NPcButtonOther.SetActive(false);
                            RemoveNPcButtonOther.SetActive(false);

                            Job = "Hunter";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Farm_house"))
                        {
                            Job = "FarmNPC";

                            NPcButton.SetActive(true);
                            RemoveNPcButton.SetActive(true);
                            NPcButtonOther.SetActive(true);
                            RemoveNPcButtonOther.SetActive(true);

                            OtherJob = "Pastoralist";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Mine_house"))
                        {
                            Job = "StoneMineWorker";

                            NPcButton.SetActive(true);
                            RemoveNPcButton.SetActive(true);
                            NPcButtonOther.SetActive(true);
                            RemoveNPcButtonOther.SetActive(true);

                            OtherJob = "IronMineWorker";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Ham_house"))
                        {
                            NPcButton.SetActive(true);
                            RemoveNPcButton.SetActive(true);
                            NPcButtonOther.SetActive(false);
                            RemoveNPcButtonOther.SetActive(false);

                            Job = "HamNPC";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Cheese_house"))
                        {
                            NPcButton.SetActive(true);
                            RemoveNPcButton.SetActive(true);
                            NPcButtonOther.SetActive(false);
                            RemoveNPcButtonOther.SetActive(false);

                            Job = "CheeseNPC";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Cloth_house"))
                        {
                            NPcButton.SetActive(true);
                            RemoveNPcButton.SetActive(true);
                            NPcButtonOther.SetActive(false);
                            RemoveNPcButtonOther.SetActive(false);

                            Job = "FabricNPC";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Smith_house"))
                        {
                            NPcButton.SetActive(true);
                            RemoveNPcButton.SetActive(true);
                            NPcButtonOther.SetActive(false);
                            RemoveNPcButtonOther.SetActive(false);

                            Job = "Smith";
                        }
                    }
                }
            }
        }
    }

    public void jobButton(bool value)
    {
        for (int i = 0; i < npc.CitizenList.Count; i++)
        {
            if(npc.CitizenList[i].tag == "NPC")
            {
                if (value == true)
                {
                    npc.CitizenList[i].tag = Job;
                }
                else if(value == false)
                {
                    npc.CitizenList[i].tag = OtherJob;
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
                }
            }
            else if(value == false)
            {
                if (npc.CitizenList[i].tag == OtherJob)
                {
                    npc.CitizenList[i].tag = "NPC";
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
        NPcButton.SetActive(false);
    }
}