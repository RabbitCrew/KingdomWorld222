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
            SMassage.SendMessage("MessageQ", "�̹� �κ��� ���� �Ѱ迡 �����߽��ϴ�.");
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

        for (int j = count - 1; j >= 0; j++)
        {
            NPCPanel[j].SetActive(false);
        }
    }
}