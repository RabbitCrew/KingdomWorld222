using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public abstract class UIComment : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] protected GameObject commentPanel;
	[SerializeField] protected TextMeshProUGUI commentText;
	protected int commentNum;
	private bool isActiveComment;
	public void OnPointerEnter(PointerEventData eventData)
	{
		Invoke("ActiveTrueComment", 1f);
		isActiveComment = true;
	}

	private void ActiveTrueComment()
	{
		if (!isActiveComment) { return; }

		commentPanel.SetActive(true);
		switch (commentNum)
		{
			case 1:
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400f);
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200f);
				commentPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(760f, 340f);

				commentText.text = "현재 보유중인 자원을 표시합니다. 클릭시, 전체 자원을 확인할 수 있습니다.";
				break;
			case 2:
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 350f);
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200f);
				commentPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-500f, 320f);

				commentText.text = "현재 날짜입니다. 좌측은 시간을 보여줍니다.";
				break;
			case 3:
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 450f);
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100f);
				commentPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-735f, -400f);

				commentText.text = "클릭 시, 건축 가능한 오브젝트를 보여줍니다.";
				break;
			case 4:
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 450f);
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100f);
				commentPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-575f, -400f);

				commentText.text = "클릭 시, 현재 인구 정보를 보여줍니다.";
				break;
			case 5:
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 450f);
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100f);
				commentPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-415f, -400f);

				commentText.text = "클릭 시, 현재 소유중인 유물을 보여줍니다.";
				break;

		}
	}

	private void ActiveFalseComment()
	{
		commentPanel.SetActive(false);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isActiveComment = false;
		ActiveFalseComment();
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }
}
