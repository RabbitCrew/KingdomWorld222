using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectNS;

public class JobListPanelScrollView : MonoBehaviour
{
    [SerializeField] private RectTransform contentsTrans;
    [SerializeField] private GameObject[] jobButtonPoolingArr;
    [SerializeField] private SpriteManager spriteManager;
    [SerializeField] private GameObject contentObj;

    public CitizenInfoPanel citizenInfoPanel { get; set; }

    private JobListButn[] jobListButnArr;
    private string[] jobArr = new string[7]
    { "Citizen", "Woodcutter", "Carpenter", "Hunter", "Farmer", "Pastoralist", "Warehouse Keeper" };

    private Vector3 startVec;

    private int heightButn = 50;
    private int butnCount = 20;
    private int visibleButnNum;
    void Awake()
    {

        startVec = contentsTrans.anchoredPosition3D;

        jobListButnArr = new JobListButn[jobButtonPoolingArr.Length];
        contentsTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, butnCount * 50f);

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

            jobButtonPoolingArr[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, (butnCount * heightButn - 525f) - (i * heightButn), 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (visibleButnNum != ((int)(contentsTrans.anchoredPosition3D.y - startVec.y) / heightButn))
        {
            //Debug.Log(visibleButnNum);
            visibleButnNum = ((int)(contentsTrans.anchoredPosition3D.y - startVec.y) / heightButn);
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
               
                if (jobArr.Length > jobListButnArr[i].butnNum) { jobListButnArr[i].SetText(jobArr[jobListButnArr[i].butnNum]); }
                else { jobListButnArr[i].SetText(null); }
            }
        }
    }

    public void ClickJobButton(int index)
    {
        if (citizenInfoPanel != null)
        {
            if (jobListButnArr.Length > index && index > -1)
            {
                citizenInfoPanel.WareClothes(spriteManager.GetCitizenSprArr(jobListButnArr[index].butnNum - 1), jobListButnArr[index].butnNum);
            }
        }
    }
}
