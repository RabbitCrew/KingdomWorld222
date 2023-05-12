using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectNS;

public class JobListPanelScrollView : JobStringArr
{
    [SerializeField] private RectTransform contentsTrans;
    [SerializeField] private GameObject[] jobButtonPoolingArr;
    [SerializeField] private SpriteManager spriteManager;
    [SerializeField] private GameObject contentObj;
    public CitizenInfoPanel citizenInfoPanel { get; set; }

    private JobListButn[] jobListButnArr;
    //private string[] jobArr = new string[13]
    //{ "Citizen", "Woodcutter", "Carpenter", "Hunter", "Farmer", "Pastoralist", "Warehouse Keeper", "Iron Miner", "Stone Miner", "Ham Npc", "Cheese Npc", "Cloth Npc", "Smith" };

    private Vector3 startVec;

    private int heightButn = 50;
    private int butnCount = 20;
    private int visibleButnNum;
    void Awake()
    {

        //startVec = contentsTrans.anchoredPosition3D;
        startVec = new Vector3(-126.5f, 150f, 0f);

        jobListButnArr = new JobListButn[jobButtonPoolingArr.Length];
        contentsTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, jobArr.Length * heightButn);

        visibleButnNum = 0;

        InitButnInfo();

    }
    
    public void InitButnInfo()
    {
        contentObj.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-126.5f, 150f, 0f);

        for (int i = 0; i < jobButtonPoolingArr.Length; i++)
        {
            jobListButnArr[i] = jobButtonPoolingArr[i].transform.GetComponent<JobListButn>();
            jobListButnArr[i].SetButn(i);

            if (jobArr.Length > i) { jobListButnArr[i].SetText(jobArr[i]); }
            else { jobListButnArr[i].SetText(null); }

            jobButtonPoolingArr[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, (jobArr.Length * heightButn - (25 + jobArr.Length * 25)) - (i * heightButn), 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (visibleButnNum != ((int)(contentsTrans.anchoredPosition3D.y - startVec.y) / heightButn))
        {
            visibleButnNum = ((int)(contentsTrans.anchoredPosition3D.y - startVec.y) / heightButn);
            Debug.Log(visibleButnNum);

            Pooling();
        }
    }

    private void Pooling()
	{
        for (int i = 0; i < jobButtonPoolingArr.Length; i++)
        {
            if (jobListButnArr[i].butnNum < visibleButnNum)
            {
                jobButtonPoolingArr[i].GetComponent<RectTransform>().anchoredPosition3D -= new Vector3(0f, heightButn * jobButtonPoolingArr.Length, 0f);
                jobListButnArr[i].SetButn(jobListButnArr[i].butnNum + jobButtonPoolingArr.Length);
                
                if (jobArr.Length > jobListButnArr[i].butnNum) { jobListButnArr[i].SetText(jobArr[jobListButnArr[i].butnNum]); }
                else { jobListButnArr[i].SetText(null); }
            }
            else if (jobListButnArr[i].butnNum > visibleButnNum + jobButtonPoolingArr.Length - 1)
            {
                jobButtonPoolingArr[i].GetComponent<RectTransform>().anchoredPosition3D += new Vector3(0f, heightButn * jobButtonPoolingArr.Length, 0f);
                jobListButnArr[i].SetButn(jobListButnArr[i].butnNum - jobButtonPoolingArr.Length);
                Debug.Log(jobListButnArr[i].butnNum);
                if (jobArr.Length > jobListButnArr[i].butnNum) { jobListButnArr[i].SetText(jobArr[jobListButnArr[i].butnNum]); }
                else { jobListButnArr[i].SetText(null); }
            }
        }
    }

    public void ClickJobButton(int index)
    {
        if (GameManager.instance.ReturnTutorialPanel().StartTutorial && index != 2) { return; }

        if (GameManager.instance.ReturnTutorialPanel().StartTutorial) { GameManager.instance.ReturnTutorialPanel().StartTuto(); }

        if (GameManager.instance.isDaytime)
        {
            if (citizenInfoPanel != null)
            {
                if (jobListButnArr.Length > index && index > -1)
                {
                    Debug.Log((JobNum)(jobListButnArr[index].butnNum));
                    if (GameManager.instance.jobCountDic[(JobNum)(jobListButnArr[index].butnNum)] > 0)
                    {
                        GameManager.instance.jobCountDic[citizenInfoPanel.jobNumEnum]++;
                        citizenInfoPanel.WareClothes(spriteManager.GetCitizenSprArr(jobListButnArr[index].butnNum - 1), jobListButnArr[index].butnNum);
                        citizenInfoPanel.gameObject.GetComponent<BuildingNPCSet>().SetBNPC(jobListButnArr[index].butnNum);


                        if (citizenInfoPanel.gameObject.GetComponent<NPC>().BuildingNum != null)
                        {
                            citizenInfoPanel.gameObject.GetComponent<NPC>().BuildingNum.GetComponent<BuildingSetting>().npcs.Remove(citizenInfoPanel.gameObject);

                            if (jobListButnArr[index].butnNum == 0)
                            {
                                citizenInfoPanel.gameObject.GetComponent<NPC>().BuildingNum = null;
                            }
                        }
                        citizenInfoPanel.gameObject.GetComponent<NPC>().searchMyBuilding();
                        //Debug.Log(GameManager.instance.jobCountDic[(JobNum)(jobListButnArr[index].butnNum)] + " ㅁㅁㅁ 전");
                        GameManager.instance.jobCountDic[(JobNum)(jobListButnArr[index].butnNum)]--;
                        int index2 = GameManager.instance.RestHuman.FindIndex(a => a.Equals(this.citizenInfoPanel.gameObject));
                        if (index2 != -1) { GameManager.instance.RestHuman.RemoveAt(index2); }
                        //Debug.Log(GameManager.instance.jobCountDic[(JobNum)(jobListButnArr[index].butnNum)] + " ㅁㅁㅁ 후");
                    }
                    else
                    {
                        //Debug.Log((JobNum)(jobListButnArr[index].butnNum) + " 빈 자리가 없습니다.");
                    }
                }
            }
        }
        else
        {
            //Debug.Log("밤입니다.");
        }
    }
}
