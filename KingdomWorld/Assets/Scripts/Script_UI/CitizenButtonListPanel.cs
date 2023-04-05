using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenButtonListPanel : MonoBehaviour
{
    [SerializeField] private RectTransform jobListPanelUI;

    public bool isOpenJobListPanel { get; private set; }

    private Vector3 openJobListPanel;
    private Vector3 closeJobListPanel;

    // Start is called before the first frame update
    void Awake()
    {
        openJobListPanel = new Vector3(75,100,0);
        closeJobListPanel = new Vector3(-175, 100, 0);
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


    // Update is called once per frame
    void Update()
    {
        if (isOpenJobListPanel)
        {
            jobListPanelUI.localPosition = Vector3.Lerp(jobListPanelUI.localPosition, openJobListPanel, Time.deltaTime * 5f);
        }
        else
        {
            jobListPanelUI.localPosition = Vector3.Lerp(jobListPanelUI.localPosition, closeJobListPanel, Time.deltaTime * 5f);
        }
    }
}
