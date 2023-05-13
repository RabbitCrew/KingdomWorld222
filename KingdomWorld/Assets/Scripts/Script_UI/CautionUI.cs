using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CautionUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject cautionObj;
    [SerializeField] private TextMeshProUGUI cautionText;
    private float clickTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetActiveFalseCautionObj()
	{
        if (Mathf.RoundToInt(Time.realtimeSinceStartup - clickTime) != 4) { return; }
        cautionObj.SetActive(false);
	}

    public void SetActiveTrueCautionObj(int num)
	{
        clickTime = Time.realtimeSinceStartup;
        cautionObj.SetActive(true);

        switch(num)
		{
            case 0:
                cautionText.text = "필요한 건물이 부족합니다 ! 직업 설명란에서 필요한 건물을 확인해주세요.";
                break;
            case 1:
                cautionText.text = "필요한 자원이 부족합니다 ! 자원 창을 클릭하여 남은 자원을 확인해주세요.";
                break;

		}

        Invoke("SetActiveFalseCautionObj", 4f);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
        SetActiveFalseCautionObj();
	}
}
