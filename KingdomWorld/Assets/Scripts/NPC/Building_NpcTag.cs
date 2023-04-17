using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Building_NpcTag : MonoBehaviour
{
    public NPC Citizen;

    private float distance = 50f;
    public GameObject NPcButton;
    public GameObject RemoveNPcButton;
    public GameObject NPcButtonOther;
    public GameObject JobPanel;
    public GameObject[] NPCPanel;
    public GameObject SMassage;
    GameObject JobBuilding;
    public GameObject JobAddB;

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

    int IsSetcount;
    int NPCCount;

    int NpcValue = 0;

    Vector3 JobAddBPos;

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
                NPCPanel[NpcValue].SetActive(true);

                NPCPanel[NpcValue].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    GameManager.instance.AllHuman[j].gameObject.name + "\n" + GameManager.instance.AllHuman[j].gameObject.tag;

                JobAddB.transform.localPosition =
                    JobAddBPos - new Vector3(0, 100 * (NpcValue + 1), 0);

                NpcValue++;
            }
            else
            {
                return;
            }
        }

        NpcValue = 0;
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

                        IsSetcount = 0;
                        NPCCount = 0;

                        if (hits[i].collider.gameObject.tag.Equals("Storage"))
                        {
                            Job = "StorageNPC";

                            IsOther = false;

                            NPCCount = 0;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("WoodCutter_house"))
                        {
                            Job = "WoodCutter";

                            IsOther = false;

                            NPCCount = 1;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Carpenter_house"))
                        {
                            Job = "CarpenterNPC";

                            NPCCount = 2;

                            IsOther = false;

                            JobText.text = InputJobText[NPCCount];
                        }
                        else if (hits[i].collider.gameObject.tag.Equals("Hunter_house"))
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
            SMassage.SendMessage("MessageQ", "현재 배당 가능한 시민이 없습니다.");
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
            SMassage.SendMessage("MessageQ", "이미 인부의 수가 한계에 도달했습니다.");
        }
    }

    public GameObject MotherBuilding;

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