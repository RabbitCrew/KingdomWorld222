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
				smallCommentText.text = "식량입니다." + "\n" + "밤에서 아침으로 넘어갈때마다 인구수에 비례하여 소모됩니다.";
				break;
			case 1:
				smallCommentText.text = "나무입니다." + "\n" + "기본적으로 건물을 지을때 사용되며 겨울이 되면 식량처럼 밤에서 아침으로 넘어갈때마다 인구수에 비례하여 소모됩니다.";
				break;
			case 2:
				smallCommentText.text = "골드입니다." + "\n" + "현재는 아무 기능도 하지 않습니다."; 
				break;
			case 3:
				smallCommentText.text = "육류입니다." + "\n" + "농장에서 소,양을 길러 얻을 수 있습니다. 식량이 0 아래로 되었을 때 육류는 식량 1당 5의 비율로 바뀝니다. ";
				break;
			case 4:
				smallCommentText.text = "가죽입니다." + "\n" + "거래소에서 거래할 때 사용됩니다. 현재는 거래로만 얻을 수 있습니다..";
				break;
			case 5:
				smallCommentText.text = "밀입니다." + "\n" + "밭에서 수확할 수 있습니다. 식량이 0 아래로 되었을 때 밀은 식량 1당 1의 비율로 바뀝니다.";
				break;
			case 6:
				smallCommentText.text = "돌입니다." + "\n" + "기본적으로 건물을 지을 때 사용됩니다.";
				break;
			case 7:
				smallCommentText.text = "우유입니다." + "\n" + "농장에서 소를 길러 얻을 수 있습니다. 거래소에서 거래할 때 사용됩니다.";
				break;
			case 8:
				smallCommentText.text = "주조철입니다." + "\n" + "대장간에서 철광석을 제련하여 얻을 수 있습니다. 기본적으로 건물을 지을 때 사용됩니다.";
				break;
			case 9:
				smallCommentText.text = "철광석입니다." + "\n" + "동굴에서 철 광부가 캐서 얻을 수 있습니다. ";
				break;
			case 10:
				smallCommentText.text = "치즈입니다." + "\n" + "치즈 공방에서 우유를 사용하여 만들어집니다. 식량이 0 아래로 되었을 때 치즈는 식량 1당 20의 비율로 바뀝니다.";
				break;
			case 11:
				smallCommentText.text = "양털입니다." + "\n" + "농장에서 양을 길러 얻을 수 있습니다.";
				break;
			case 12:
				smallCommentText.text = "옷감입니다." + "\n" + "옷감 공방에서 양털을 사용하여 만들어집니다. 거래소에서 거래할 때 사용됩니다.";
				break;
			case 13:
				smallCommentText.text = "햄입니다." + "\n" + "햄 공방에서 육유를 사용하여 만들어집니다. 식량이 0 아래로 되었을 때 햄은 식량 1당 20의 비율로 바뀝니다.";
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
