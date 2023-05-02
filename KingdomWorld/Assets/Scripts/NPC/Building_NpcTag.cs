using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Building_NpcTag : MonoBehaviour
{
    public NPC Citizen;

    [SerializeField] GameObject NPcButton;
    [SerializeField] GameObject RemoveNPcButton;
    [SerializeField] GameObject NPcButtonOther;
    [SerializeField] GameObject JobPanel;
    [SerializeField] GameObject[] NPCPanel;
    [SerializeField] GameObject SMassage;
    [SerializeField] GameObject JobAddB;
    [SerializeField] GameObject MotherBuilding;
    GameObject JobBuilding;

    [SerializeField] string[] InputJobText;

    [SerializeField] TextMeshProUGUI JobText;
    [SerializeField] TextMeshProUGUI OtherJobText;

    string Job = null;
    string OtherJob = null;

    bool IsOther = false;

    int IsSetcount;
    int NPCCount;
    int NpcValue = 0;

    private float distance = 50f;

    Vector3 JobAddBPos;

    private void Update()
    {
        BuildingCheck();
    }

    private void Start()
    {
        JobAddBPos = JobAddB.transform.localPosition;
    }

    void NPCFound()
    {
        NpcValue = 0;

        for (int j = 0; j < GameManager.instance.AllHuman.Count; j++)
        {
            if (GameManager.instance.AllHuman[j].GetComponent<NPC>().BuildingNum == JobBuilding)
            {
                NpcValue++;

                NPCPanel[NpcValue - 1].SetActive(true);

                NPCPanel[NpcValue - 1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    GameManager.instance.AllHuman[j].gameObject.name + "\n" + TagCheck(GameManager.instance.AllHuman[j].gameObject);

                JobAddB.transform.localPosition =
                    JobAddBPos - new Vector3(0, 100 * NpcValue, 0);
            }
        }

        NpcValue = 0;
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

                        IsSetcount = 0;
                        NPCCount = 0;

                        NPcButton.SetActive(false);
                        NPcButtonOther.SetActive(false);

                        for (int j = 0; j < NPCPanel.Length; j++)
                        {
                            NPCPanel[j].SetActive(false);
                        }

                        NPCFound();

                        if (hits[i].collider.gameObject.tag == "Storage")
                        {
                            Job = "StorageNPC";

                            IsOther = false;

                            NPCCount = 0;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag == "WoodCutter_house")
                        {
                            Job = "WoodCutter";

                            IsOther = false;

                            NPCCount = 1;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag == "Carpenter_house")
                        {
                            Job = "CarpenterNPC";

                            NPCCount = 2;

                            IsOther = false;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag == "Hunter_house")
                        {
                            Job = "Hunter";

                            NPCCount = 3;

                            IsOther = false;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Farm_house"))
                        {
                            Job = "Pastoralist";

                            IsOther = false;

                            NPCCount = 4;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("WheatField"))
                        {
                            Job = "FarmNPC";

                            NPCCount = 5;

                            IsOther = false;

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

                            IsOther = false;
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Cheese_house"))
                        {
                            Job = "CheeseNPC";

                            NPCCount = 9;

                            JobText.text = InputJobText[NPCCount];

                            IsOther = false;
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Cloth_house"))
                        {
                            Job = "FabricNPC";

                            NPCCount = 10;

                            JobText.text = InputJobText[NPCCount];

                            IsOther = false;
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Smith_house"))
                        {
                            Job = "Smith";

                            NPCCount = 11;

                            JobText.text = InputJobText[NPCCount];

                            IsOther = false;
                        }
                        else
                        {
                            JobPanel.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    public void JobSetOn()
    {
        NPcButton.SetActive(true);

        if (IsOther == true)
        {
            NPcButtonOther.SetActive(true);
        }
    }

    string TagCheck(GameObject ToCheckNpc)
    {
        if (ToCheckNpc.tag.Equals("StorageNPC"))
        {
            NPCCount = 0;

            return InputJobText[NPCCount];
        }
        else if (ToCheckNpc.tag.Equals("WoodCutter"))
        {
            NPCCount = 1;

            return InputJobText[NPCCount];
        }
        else if (ToCheckNpc.tag.Equals("CarpenterNPC"))
        {
            NPCCount = 2;

            return InputJobText[NPCCount];
        }
        else if (ToCheckNpc.tag.Equals("Hunter"))
        {
            NPCCount = 3;

            return InputJobText[NPCCount];
        }
        else if (ToCheckNpc.tag.Equals("Pastoralist"))
        {
            NPCCount = 4;

            return InputJobText[NPCCount];
        }
        else if (ToCheckNpc.tag.Equals("Farmer"))
        {
            NPCCount = 5;
            return InputJobText[NPCCount];
        }
        else if (ToCheckNpc.tag.Equals("StoneMiner"))
        {
            NPCCount = 6;

            return InputJobText[NPCCount];
        }
        else if(ToCheckNpc.tag.Equals("IronMiner"))
        {
            NPCCount = 7;

            return InputJobText[NPCCount];
        }
        else if (ToCheckNpc.tag.Equals("HamNPC"))
        {
            NPCCount = 8;

            return InputJobText[NPCCount];
        }
        else if (ToCheckNpc.tag.Equals("CheeseNPC"))
        {
            NPCCount = 9;

            return InputJobText[NPCCount];
        }
        else if (ToCheckNpc.tag.Equals("FabricNPC"))
        {
            NPCCount = 10;

            return InputJobText[NPCCount];
        }
        else if (ToCheckNpc.tag.Equals("Smith"))
        {
            NPCCount = 11;

            return InputJobText[NPCCount];
        }
        else
        {
            return "���";
        }
    }

    public void jobButton(bool value)
    {
        int NPCCount = 0;

        for (int i = 0; i < GameManager.instance.AllHuman.Count; i++)
        {
            if (GameManager.instance.AllHuman[i].tag == "NPC")
            {
                GameManager.instance.RestHuman[NPCCount] = GameManager.instance.AllHuman[i];

                NPCCount++;
            }
        }

        if (NPCCount == 0)
        {
            SMassage.SendMessage("MessageQ", "���� ��� ������ �ù��� �����ϴ�.");
        }
        else if (NPCCount > 0)
        {
            if (IsSetcount < 3 && IsSetcount >= 0)
            {
                for (int i = 0; i < GameManager.instance.AllHuman.Count; i++)
                {
                    if (GameManager.instance.AllHuman[i].tag == "NPC")
                    {
                        Citizen = GameManager.instance.AllHuman[i].gameObject.GetComponent<NPC>();

                        if (value == false)
                        {
                            GameManager.instance.AllHuman[i].gameObject.tag = Job;

                            Citizen.BuildingNum = JobBuilding;
                            Citizen.NPCBUildTrigger = true;

                            IsSetcount++;

                            break; 
                        }
                        else if (value == true)
                        {
                            GameManager.instance.AllHuman[i].gameObject.tag = OtherJob;

                            Citizen.BuildingNum = JobBuilding;
                            Citizen.NPCBUildTrigger = true;

                            IsSetcount++;

                            break;
                        }
                    }
                }
            }
        }

        if (IsSetcount == 3)
        {
            SMassage.SendMessage("MessageQ", "�̹� �κ��� ���� �Ѱ迡 �����߽��ϴ�.");
        }

        NPCFound();
    }

    public void RemoveJobButton(int value)
    {
        int counts = 0;

        GameObject TargetPos = null;

        for (int i = 0; i < MotherBuilding.transform.childCount; i++)
        {
            if(MotherBuilding.transform.GetChild(i).tag == "House")
            {
                for (int j = 0; j < GameManager.instance.AllHuman.Count; j++)
                {
                    if(GameManager.instance.AllHuman[j].GetComponent<NPC>().BuildingNum == MotherBuilding.transform.GetChild(i))
                    {
                        if(counts < 3)
                        {
                            counts++;
                        }
                        else if(counts == 3)
                        {
                            continue;
                        }
                    }
                }

                if(counts < 3)
                {
                    TargetPos = MotherBuilding.transform.GetChild(i).gameObject;

                    break;
                }
            }
        }

        counts = 0;

        for (int i = 0; i < GameManager.instance.AllHuman.Count; i++)
        {
            if(GameManager.instance.AllHuman[i].GetComponent<NPC>().BuildingNum == JobBuilding)
            {
                if(counts == value)
                {
                    GameManager.instance.AllHuman[i].tag = "NPC";

                    GameManager.instance.AllHuman[i].GetComponent<NPC>().BuildingNum = TargetPos;

                    GameManager.instance.AllHuman[i].GetComponent<NPC>().NPCBUildTrigger = true;

                    NPCPanel[value].SetActive(false);

                    JobAddB.transform.localPosition =
                   JobAddBPos + new Vector3(0, 100 * (NpcValue), 0);

                    IsSetcount--;
                }
                else
                {
                    counts++;
                }
            }
        }

        NPCFound();
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

        NPCFound();
    }
}