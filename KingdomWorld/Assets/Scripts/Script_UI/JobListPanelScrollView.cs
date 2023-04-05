using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobListPanelScrollView : MonoBehaviour
{
    [SerializeField] private RectTransform contentsTrans;
    [SerializeField] private GameObject[] jobButtonPoolingArr;

    private JobListButn[] jobListButnArr;
    private string[] jobArr = new string[7]
    { "Citizen", "Woodcutter", "Carpenter", "Hunter", "Farmer", "Pastoralist", "Warehouse Keeper" };

    private Vector3 startVec;
    private Vector3 endVec;

    private int heightButn = 50;
    private int visibleButnCountInContent = 6;
    private int visibleButnNum;
    void Awake()
    {

        //contentsTrans.localPosition = new Vector3(-126.5f, 150f, 0f);

        startVec = contentsTrans.anchoredPosition3D;
        // = startVec;

        //endVec = startVec + new Vector3(0f ,50f * (20 - 6) ,0f);

        jobListButnArr = new JobListButn[jobButtonPoolingArr.Length];
        contentsTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20 * 50f);

        visibleButnNum = -1;

        for (int i = 0; i < jobButtonPoolingArr.Length; i++)
		{
            jobListButnArr[i] = jobButtonPoolingArr[i].transform.GetComponent<JobListButn>();
            jobListButnArr[i].SetButn(i + 1);
            jobButtonPoolingArr[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, (20*50f - 525f) - (i * 50f), 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (visibleButnNum != ((int)(contentsTrans.anchoredPosition3D.y - startVec.y) / 50))
        {
            visibleButnNum = ((int)(contentsTrans.anchoredPosition3D.y - startVec.y) / 50);
            Pooling();
        }
    }

    private void Pooling()
	{
        for (int i = 0; i < jobButtonPoolingArr.Length; i++)
        {
            if (jobListButnArr[i].butnNum <= visibleButnNum)
            {
                jobButtonPoolingArr[i].GetComponent<RectTransform>().anchoredPosition3D += new Vector3(0f, 50f * jobButtonPoolingArr.Length, 0f);
                jobListButnArr[i].SetButn(jobListButnArr[i].butnNum + jobButtonPoolingArr.Length);
            }
            else if (jobListButnArr[i].butnNum > visibleButnNum + jobButtonPoolingArr.Length)
            {
                jobButtonPoolingArr[i].GetComponent<RectTransform>().anchoredPosition3D -= new Vector3(0f, 50f * jobButtonPoolingArr.Length, 0f);
                jobListButnArr[i].SetButn(jobListButnArr[i].butnNum - jobButtonPoolingArr.Length);
            }
        }
    }
}
