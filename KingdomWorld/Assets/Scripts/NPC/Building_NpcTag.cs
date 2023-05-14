using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Building_NpcTag : MonoBehaviour
{
    public NPC citizenNPC;

    [SerializeField] GameObject RemoveNPcButton;
    [SerializeField] private GameObject JobPanel;
    [SerializeField] private GameObject[] NPCPanel;
    [SerializeField] private TextMeshProUGUI[] NPCPanelText;
    [SerializeField] private SpriteManager spriteManager;
    [SerializeField] GameObject SMassage;
    [SerializeField] private GameObject jobAddButton;
    [SerializeField] GameObject MotherBuilding;
    [SerializeField] string[] InputJobText;

    private List<GameObject> jobNPCList = new List<GameObject>();   // ���� �ǹ��� �����Ǿ� �ִ� �ù��� ����Ʈ
    private GameObject JobBuilding;

    private int jobCount;   // ���� ���� �ǹ��� �Ҵ�� �ù� ��
    private int jobMaxCount;    // ���� ���� �ǹ��� �Ҵ�� �� �ִ� �ִ� �ù� ��
    private int jobCode;    // �������� ������ ��ȣ
    private float distance = 50f;

    Vector3 jobAddBPos;

    private void Update()
    {
        BuildingCheck();
    }

    private void Start()
    {
        jobAddBPos = jobAddButton.transform.localPosition;
    }

    void NPCFound()
    {
        jobNPCList.Clear();
        jobNPCList = GameManager.instance.AllHuman.FindAll(a => a.GetComponent<NPC>().BuildingNum == JobBuilding);

        for (int i =0; i < NPCPanel.Length; i++)
		{
            NPCPanel[i].SetActive(false);
		}


        for (int i = 0; i < jobNPCList.Count; i++)
		{
            NPCPanel[i].SetActive(true);
            NPCPanelText[i].text = jobNPCList[i].name + "\n" + TagCheck(jobNPCList[i].gameObject);
		}

        JobAddButtonPosition(jobNPCList.Count * (-1));
    }

    private void JobAddButtonPosition(int num)
	{
        jobAddButton.GetComponent<RectTransform>().anchoredPosition = jobAddBPos + new Vector3(0, 100 * num, 0);
	}

    void BuildingCheck()
    {
        if (GameManager.instance.ReturnTutorialPanel().TutorialProceeding) { return; }

        if (Input.GetMouseButtonUp(0) && !IsPointerOverUIObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);// ray�� ���콺 ������ �� ���콺 ��ġ �޾ƿ�

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray, distance);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.GetComponent<CitizenInfoPanel>() != null)
                {
                    return;
                }
            }

            for (int i = 0; i < hits.Length; i++) // ���̷� Ŭ���� �κ��� ������Ʈ ������
            {
                if (hits[i].collider.GetComponent<BuildingColider>() != null) // �ǹ��� �Ҵ��� �ݶ��̴��� ���� ��
                {
                    if (hits[i].collider.GetComponent<BuildingColider>().isSettingComplete)
                    {
                        JobBuilding = hits[i].collider.gameObject;

                        JobPanel.SetActive(true);

                        if (JobBuilding.GetComponent<BuildingSetting>() != null)
                        {
                            jobMaxCount = JobBuilding.GetComponent<BuildingSetting>().npcCount;
                        }

                        for (int j = 0; j < NPCPanel.Length; j++)
                        {
                            NPCPanel[j].SetActive(false);
                        }

                        NPCFound();

                        if (hits[i].collider.gameObject.tag.Equals("Storage")) { jobCode = 6; }
                        else if (JobBuilding.tag.Equals("WoodCutter_house")) { jobCode = 1; }
                        else if (JobBuilding.tag.Equals("Carpenter_house")) { jobCode = 2; }
                        else if (JobBuilding.tag.Equals("Hunter_house")) { jobCode = 3; }
                        else if (JobBuilding.tag.Equals("Farm_house")) { jobCode = 5; }
                        else if (JobBuilding.tag.Equals("WheatField")) { jobCode = -1; }
                        else if (JobBuilding.tag.Equals("Mine_house")) { jobCode = 8; }
                        else if (JobBuilding.tag.Equals("Ham_house")) { jobCode = 9; }
                        else if (JobBuilding.tag.Equals("Cheese_house")) { jobCode = 10; }
                        else if (JobBuilding.tag.Equals("Cloth_house")) { jobCode = 11; }
                        else if (JobBuilding.tag.Equals("Smith_house")) { jobCode = 12; }
                        else { JobPanel.SetActive(false); }
                    }
                }
            }
        }
        NPCFound();

        //if (JobBuilding != null)
        //{
        //    if (JobBuilding.GetComponent<BuildingSetting>() != null)
        //    {
        //        if (jobCount != JobBuilding.GetComponent<BuildingSetting>().npcs.Count)
        //        {
        //            jobCount = JobBuilding.GetComponent<BuildingSetting>().npcs.Count;
        //        }
        //    }
        //}
    }

    string TagCheck(GameObject ToCheckNpc)
    {
        if (ToCheckNpc.tag.Equals("StorageNPC")) { return InputJobText[0]; }
        else if (ToCheckNpc.tag.Equals("WoodCutter")) { return InputJobText[1]; }
        else if (ToCheckNpc.tag.Equals("CarpenterNPC")) { return InputJobText[2]; }
        else if (ToCheckNpc.tag.Equals("Hunter")) { return InputJobText[3]; }
        else if (ToCheckNpc.tag.Equals("Pastoralist")) { return InputJobText[4]; }
        else if (ToCheckNpc.tag.Equals("FarmNPC")) { return InputJobText[5]; }
        else if (ToCheckNpc.tag.Equals("StoneMineWorker")) { return InputJobText[6]; }
        else if (ToCheckNpc.tag.Equals("IronMineWorker")) { return InputJobText[7]; }
        else if (ToCheckNpc.tag.Equals("HamNPC")) { return InputJobText[8]; }
        else if (ToCheckNpc.tag.Equals("CheeseNPC")) { return InputJobText[9]; }
        else if (ToCheckNpc.tag.Equals("FabricNPC")) { return InputJobText[10]; }
        else if (ToCheckNpc.tag.Equals("Smith")) { return InputJobText[11]; }
        else { return null; }
    }

    public void AddJobButton()
    {
        // ���������� �ο��� ���� ������ �� �̻� ���� �Ұ���.
        if (jobNPCList.Count >= jobMaxCount) { return; }

        int index = GameManager.instance.AllHuman.FindIndex(a => a.tag.Equals("NPC"));

        // ���� ��� �ִ� �ù��� ������ ���� �Ұ���
        if (index == -1) { return; }

        // �ùο��� �ش� ���� ����
        GameManager.instance.AllHuman[index].GetComponent<CitizenInfoPanel>().WareClothes(spriteManager.GetCitizenSprArr(jobCode-1),jobCode);
        GameManager.instance.jobCountDic[GameManager.instance.AllHuman[index].GetComponent<CitizenInfoPanel>().jobNumEnum]--;
        GameManager.instance.AllHuman[index].GetComponent<BuildingNPCSet>().SetBNPC(jobCode);
        citizenNPC = GameManager.instance.AllHuman[index].gameObject.GetComponent<NPC>();

        JobBuilding.GetComponent<BuildingSetting>().AddNPCs(citizenNPC.gameObject);
        citizenNPC.BuildingNum = JobBuilding;
        citizenNPC.NPCBUildTrigger = true;

        jobNPCList.Add(GameManager.instance.AllHuman[index]);

        int index2 = GameManager.instance.RestHuman.FindIndex(a => a.Equals(GameManager.instance.AllHuman[index]));
        if (index2 != -1) { GameManager.instance.RestHuman.Remove(GameManager.instance.AllHuman[index]); }

        if (jobNPCList.Count >= jobMaxCount)
		{
            GameManager.instance.EmptyNPCBuilding.Remove(JobBuilding);
		}

        NPCFound();
    }

    public void RemoveJobButton(int value)
    {
        int index = GameManager.instance.AllHuman.FindIndex(a => a.GetComponent<NPC>().BuildingNum == JobBuilding);
        Debug.Log(index);
        if (index == -1) { return; }
        GameManager.instance.jobCountDic[GameManager.instance.AllHuman[index].GetComponent<CitizenInfoPanel>().jobNumEnum]++;
        GameManager.instance.AllHuman[index].GetComponent<CitizenInfoPanel>().WareClothes(null, 0);
        GameManager.instance.jobCountDic[GameManager.instance.AllHuman[index].GetComponent<CitizenInfoPanel>().jobNumEnum]--;
        GameManager.instance.AllHuman[index].GetComponent<BuildingNPCSet>().SetBNPC(0);
        GameManager.instance.AllHuman[index].GetComponent<NPC>().BuildingNum.GetComponent<BuildingSetting>().npcs.Remove(GameManager.instance.AllHuman[index]);
        GameManager.instance.AllHuman[index].GetComponent<NPC>().BuildingNum = null;
        GameManager.instance.AllHuman[index].GetComponent<NPC>().searchMyBuilding();

        if (GameManager.instance.RestHuman.FindIndex(a => a.Equals(GameManager.instance.AllHuman[index])) == -1)
        {
            GameManager.instance.RestHuman.Add(GameManager.instance.AllHuman[index]);
        }
        jobNPCList.Remove(GameManager.instance.AllHuman[index]);

        if (jobNPCList.Count < jobMaxCount && GameManager.instance.EmptyNPCBuilding.FindIndex(a => a.Equals(JobBuilding)) == -1)
		{
            GameManager.instance.EmptyNPCBuilding.Add(JobBuilding);
		}


        NPCFound();

        JobAddButtonPosition(jobNPCList.Count);
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
        NPCFound();

        JobPanel.SetActive(false);

    }
}