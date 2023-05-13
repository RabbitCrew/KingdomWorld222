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
    [SerializeField] private GameObject npcHpShieldBarUIObjPanel;
    [SerializeField] private GameObject endingPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private Image hpBarMask;
    [SerializeField] private Image shieldBarMask;
    [SerializeField] private Image npcHpBarMask;
    [SerializeField] private Image npcShieldBarMask;
    [SerializeField] private GameObject humanListPanel;
    [SerializeField] private TextMeshProUGUI buildingText;
    [SerializeField] private TextMeshProUGUI HumanCountText;
    [SerializeField] private Image endingPanelImage;
    public bool isOpenCitizenPanel { get; private set; }

    private Vector3 openJobChangeUIVec;
    private Vector3 closeJobChangeUIVec;
    private Vector3 npcHpShield;
    private bool isEnding;
    // Start is called before the first frame update
    void Awake()
    {
        isOpenCitizenPanel = false;
        isEnding = false;
        openJobChangeUIVec = new Vector3(-740f, 100f, 0f);
        closeJobChangeUIVec = new Vector3(-1180f, 100f, 0f);
    }

    public void SetAcitveTutoPanel(bool bo)
	{
        tutorialPanel.SetActive(bo);
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

    public void SetIsNPCHpAndShieldBarUIObj(bool bo, int hp, int maxHp, Transform npcTrans)
    {
        npcHpAndShieldBarUIObj.SetActive(bo);
        if (!bo) { return; }

        npcHpShield = Camera.main.WorldToViewportPoint(npcTrans.position);
        // 1920, 1080Àº Äµ¹ö½ºÀÇ width, height°ªÀÓ
        npcHpShieldBarUIObjPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(((npcHpShield.x * 1920) - 960), ((npcHpShield.y * 1080) - 540) + 50, 40f);
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
        if (!GameManager.instance.ReturnTutorialPanel().isHumanListButtonOpen) { return; }

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

        if (GameManager.instance.AllHuman.Count > 100 && !endingPanel.activeSelf && !isEnding)
        {
            endingPanel.SetActive(true);
            GameManager.instance.GameStop = true;
            isEnding = true;
        }

        if (endingPanel.activeSelf && endingPanelImage.color.a <= 1f)
        {
            endingPanelImage.color += new Color(0, 0, 0, 5 / 255f);
        }

        HumanCountText.text = "ÃÑ ÀÎ±¸ : " + GameManager.instance.AllHuman.Count;
    }
}
