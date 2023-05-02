using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : JobStringArr
{
    [SerializeField] private RectTransform jobChangeUI;
    [SerializeField] private CitizenButtonListPanel citizenButtonListPanel;
    [SerializeField] private GameObject hpAndShieldBarUIObj;
    [SerializeField] private GameObject npcHpAndShieldBarUIObj;
    [SerializeField] private Image hpBarMask;
    [SerializeField] private Image shieldBarMask;
    [SerializeField] private Image npcHpBarMask;
    [SerializeField] private Image npcShieldBarMask;
    [SerializeField] private GameObject humanListPanel;
    [SerializeField] private TextMeshProUGUI buildingText;
    public bool isOpenCitizenPanel { get; private set; }

    private Vector3 openJobChangeUIVec;
    private Vector3 closeJobChangeUIVec;
    // Start is called before the first frame update
    void Awake()
    {
        isOpenCitizenPanel = false;

        openJobChangeUIVec = new Vector3(-740f, 100f, 0f);
        closeJobChangeUIVec = new Vector3(-1180f, 100f, 0f);
    }

    public void SetIsHpAndShieldBarUIObj(bool bo, int hp, int shield, int maxHp, int maxShield, int buildingNumber)
	{
        hpAndShieldBarUIObj.SetActive(bo);
        if (!bo) { return; }

        buildingText.text = buildingArr[buildingNumber];
        if (float.IsNaN((float)hp / (float)maxHp))
        {
            hpBarMask.fillAmount = 0f;
        }
        else
        {
            hpBarMask.fillAmount = (float)hp / (float)maxHp;
        }

        if (float.IsNaN((float)shield / (float)maxShield))
        {
            shieldBarMask.fillAmount = 0f;

        }
        else
        {
            shieldBarMask.fillAmount = (float)shield / (float)maxShield;
        }
    }

    public void SetIsNPCHpAndShieldBarUIObj(bool bo, int hp, int maxHp)
    {
        npcHpAndShieldBarUIObj.SetActive(bo);
        if (!bo) { return; }

        if (float.IsNaN((float)hp / (float)maxHp))
        {
            npcHpBarMask.fillAmount = 0f;
        }
        else
        {
            npcHpBarMask.fillAmount = (float)hp / (float)maxHp;
        }

        //if (float.IsNaN((float)shield/ (float)maxShield))
        //{
        //    npcShieldBarMask.fillAmount = 0f;
        //}
        //else
        //{
        //    npcShieldBarMask.fillAmount = (float)shield / (float)maxShield;
        //}
    }

    public void SetIsOpenCitizenPanel(bool bo, CitizenInfoPanel citizenInfo)
	{
        isOpenCitizenPanel = bo;
        citizenButtonListPanel.SetCitizenInfoPanel(citizenInfo);

        if (!bo)
        { 
            citizenButtonListPanel.isOpenJobListPanel = false;
            citizenButtonListPanel.SetCitizenInfoPanel(null);
            citizenButtonListPanel.InitButnInfo();
        }

	}
    public void SetActiveHumanListPanel()
    {
        if (humanListPanel.activeSelf)
        {
            humanListPanel.SetActive(false);
        }
        else
        {
            humanListPanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpenCitizenPanel && openJobChangeUIVec.x != jobChangeUI.localPosition.x)
        {
            jobChangeUI.localPosition = Vector3.Lerp(jobChangeUI.localPosition, openJobChangeUIVec, Time.deltaTime * 5f);
            if (openJobChangeUIVec.x - jobChangeUI.localPosition.x < 1f)
            {
                jobChangeUI.localPosition = openJobChangeUIVec;
            }
        }
        else if (!isOpenCitizenPanel && closeJobChangeUIVec.x != jobChangeUI.localPosition.x)
        {
            jobChangeUI.localPosition = Vector3.Lerp(jobChangeUI.localPosition, closeJobChangeUIVec, Time.deltaTime * 5f);
            if (jobChangeUI.localPosition.x - closeJobChangeUIVec.x < 1f)
            {
                jobChangeUI.localPosition = closeJobChangeUIVec;
            }
        }
    }
}
