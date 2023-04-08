using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenButtonListPanel : MonoBehaviour
{
    [SerializeField] private GameObject jobListObj;
    [SerializeField] private RectTransform numberOfEmployeesPanelUI;
    
    private RectTransform jobListPanelUI;
    public bool isOpenJobListPanel { get; set; }

    private Vector3 openJobListPanel;
    private Vector3 closeJobListPanel;

    private Vector3 openNumberOfEmployeesPanel;
    private Vector3 closeNumberOfEmployeesPanel;

    // Start is called before the first frame update
    void Awake()
    {
        jobListPanelUI = jobListObj.GetComponent<RectTransform>();

        openJobListPanel = new Vector3(75,100,0);
        closeJobListPanel = new Vector3(-175, 100, 0);

        openNumberOfEmployeesPanel = new Vector3(75, -150, 0);
        closeNumberOfEmployeesPanel = new Vector3(75, -350, 0);
    }

    public void ClickButn()
    {
        if (isOpenJobListPanel)
        {
            isOpenJobListPanel = false;
        }
        else
        {
            isOpenJobListPanel = true;
        }
    }

    public void InitButnInfo()
    {
        jobListObj.GetComponent<JobListPanelScrollView>().InitButnInfo();
    }

    public void SetCitizenInfoPanel(CitizenInfoPanel citizenInfo)
    {
        jobListObj.GetComponent<JobListPanelScrollView>().citizenInfoPanel = citizenInfo;

    }


    // Update is called once per frame
    void Update()
    {
        if (isOpenJobListPanel)
        {
            jobListPanelUI.localPosition = Vector3.Lerp(jobListPanelUI.localPosition, openJobListPanel, Time.deltaTime * 5f);
            numberOfEmployeesPanelUI.localPosition = Vector3.Lerp(numberOfEmployeesPanelUI.localPosition, openNumberOfEmployeesPanel, Time.deltaTime * 5f);
        }
        else
        {
            jobListPanelUI.localPosition = Vector3.Lerp(jobListPanelUI.localPosition, closeJobListPanel, Time.deltaTime * 5f);
            numberOfEmployeesPanelUI.localPosition = Vector3.Lerp(numberOfEmployeesPanelUI.localPosition, closeNumberOfEmployeesPanel, Time.deltaTime * 5f);
        }
    }
}
