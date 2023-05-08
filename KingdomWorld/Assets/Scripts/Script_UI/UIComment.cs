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

				commentText.text = "���� �������� �ڿ��� ǥ���մϴ�. Ŭ����, ��ü �ڿ��� Ȯ���� �� �ֽ��ϴ�.";
				break;
			case 2:
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 350f);
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200f);
				commentPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-500f, 320f);

				commentText.text = "���� ��¥�Դϴ�. ������ �ð��� �����ݴϴ�.";
				break;
			case 3:
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 450f);
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100f);
				commentPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-735f, -400f);

				commentText.text = "Ŭ�� ��, ���� ������ ������Ʈ�� �����ݴϴ�.";
				break;
			case 4:
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 450f);
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100f);
				commentPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-575f, -400f);

				commentText.text = "Ŭ�� ��, ���� �α� ������ �����ݴϴ�.";
				break;
			case 5:
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 450f);
				commentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100f);
				commentPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-415f, -400f);

				commentText.text = "Ŭ�� ��, ���� �������� ������ �����ݴϴ�.";
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
