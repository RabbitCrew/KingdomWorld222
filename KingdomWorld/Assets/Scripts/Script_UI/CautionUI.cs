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
                cautionText.text = "�ʿ��� �ǹ��� �����մϴ� ! ���� ��������� �ʿ��� �ǹ��� Ȯ�����ּ���.";
                break;
            case 1:
                cautionText.text = "�ʿ��� �ڿ��� �����մϴ� ! �ڿ� â�� Ŭ���Ͽ� ���� �ڿ��� Ȯ�����ּ���.";
                break;

		}

        Invoke("SetActiveFalseCautionObj", 4f);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
        SetActiveFalseCautionObj();
	}
}
