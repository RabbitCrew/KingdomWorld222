using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
public class UISmallComment : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] protected GameObject smallCommentPanel;
	[SerializeField] protected TextMeshProUGUI smallCommentText;
	[SerializeField ]protected int smallCommentNum;

	public void OnPointerEnter(PointerEventData eventData)
	{
		ActiveTrueSmallComment();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		smallCommentPanel.SetActive(false);
		this.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
	}

	private void ActiveTrueSmallComment()
	{
		smallCommentPanel.SetActive(true);
		this.GetComponent<Image>().color = new Color(70 / 255f, 70 / 255f, 70 / 255f, 70 / 255f);
		smallCommentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400f);
		smallCommentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 260f);
		smallCommentPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(360f, 80f);

		switch (smallCommentNum)
		{
			case 0:
				smallCommentText.text = "�ķ��Դϴ�." + "\n" + "�㿡�� ��ħ���� �Ѿ������ �α����� ����Ͽ� �Ҹ�˴ϴ�.";
				break;
			case 1:
				smallCommentText.text = "�����Դϴ�." + "\n" + "�⺻������ �ǹ��� ������ ���Ǹ� �ܿ��� �Ǹ� �ķ�ó�� �㿡�� ��ħ���� �Ѿ������ �α����� ����Ͽ� �Ҹ�˴ϴ�.";
				break;
			case 2:
				smallCommentText.text = "����Դϴ�." + "\n" + "����� �ƹ� ��ɵ� ���� �ʽ��ϴ�."; 
				break;
			case 3:
				smallCommentText.text = "�����Դϴ�." + "\n" + "���忡�� ��,���� �淯 ���� �� �ֽ��ϴ�. �ķ��� 0 �Ʒ��� �Ǿ��� �� ������ �ķ� 1�� 5�� ������ �ٲ�ϴ�. ";
				break;
			case 4:
				smallCommentText.text = "�����Դϴ�." + "\n" + "�ŷ��ҿ��� �ŷ��� �� ���˴ϴ�. ����� �ŷ��θ� ���� �� �ֽ��ϴ�..";
				break;
			case 5:
				smallCommentText.text = "���Դϴ�." + "\n" + "�翡�� ��Ȯ�� �� �ֽ��ϴ�. �ķ��� 0 �Ʒ��� �Ǿ��� �� ���� �ķ� 1�� 1�� ������ �ٲ�ϴ�.";
				break;
			case 6:
				smallCommentText.text = "���Դϴ�." + "\n" + "�⺻������ �ǹ��� ���� �� ���˴ϴ�.";
				break;
			case 7:
				smallCommentText.text = "�����Դϴ�." + "\n" + "���忡�� �Ҹ� �淯 ���� �� �ֽ��ϴ�. �ŷ��ҿ��� �ŷ��� �� ���˴ϴ�.";
				break;
			case 8:
				smallCommentText.text = "����ö�Դϴ�." + "\n" + "���尣���� ö������ �����Ͽ� ���� �� �ֽ��ϴ�. �⺻������ �ǹ��� ���� �� ���˴ϴ�.";
				break;
			case 9:
				smallCommentText.text = "ö�����Դϴ�." + "\n" + "�������� ö ���ΰ� ĳ�� ���� �� �ֽ��ϴ�. ";
				break;
			case 10:
				smallCommentText.text = "ġ���Դϴ�." + "\n" + "ġ�� ���濡�� ������ ����Ͽ� ��������ϴ�. �ķ��� 0 �Ʒ��� �Ǿ��� �� ġ��� �ķ� 1�� 20�� ������ �ٲ�ϴ�.";
				break;
			case 11:
				smallCommentText.text = "�����Դϴ�." + "\n" + "���忡�� ���� �淯 ���� �� �ֽ��ϴ�.";
				break;
			case 12:
				smallCommentText.text = "�ʰ��Դϴ�." + "\n" + "�ʰ� ���濡�� ������ ����Ͽ� ��������ϴ�. �ŷ��ҿ��� �ŷ��� �� ���˴ϴ�.";
				break;
			case 13:
				smallCommentText.text = "���Դϴ�." + "\n" + "�� ���濡�� ������ ����Ͽ� ��������ϴ�. �ķ��� 0 �Ʒ��� �Ǿ��� �� ���� �ķ� 1�� 20�� ������ �ٲ�ϴ�.";
				break;
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
