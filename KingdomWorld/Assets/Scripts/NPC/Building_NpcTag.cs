using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building_NpcTag : MonoBehaviour
{
    public SpawnCitizen npc;
    
    private float distance = 50f;
    public GameObject NPcButton;
    public GameObject NPcButtonOther;

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
                        NPcButton.SetActive(true);
                        NPcButtonOther.SetActive(false);

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

                            NPcButtonOther.SetActive(true);

                            OtherJob = "Pastoralist";
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Mine_house"))
                        {
                            Job = "StoneMineWorker";

                            NPcButtonOther.SetActive(true);

                            OtherJob = "IronMineWorker";
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

    public void jobButton()
    {
        for (int i = 0; i < npc.CitizenList.Count; i++)
        {
            if(npc.CitizenList[i].tag == "NPC")
            {
                npc.CitizenList[i].tag = Job;
            }
        }
    }

    public void OtherJobButton()
    {
        for (int i = 0; i < npc.CitizenList.Count; i++)
        {
            if (npc.CitizenList[i].tag == "NPC")
            {
                npc.CitizenList[i].tag = OtherJob;
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
}