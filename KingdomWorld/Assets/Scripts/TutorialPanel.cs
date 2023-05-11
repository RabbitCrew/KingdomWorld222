using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class TutorialPanel : MonoBehaviour,IPointerClickHandler
{
	[SerializeField] private GameObject tutoImg;
	[SerializeField] private TextMeshProUGUI tutoText;
	[SerializeField] private Image thisImg;
	public int TutorialNum { get; set; }
    public bool StartTutorial { get; set; }
	public bool TutorialProceeding { get; set; }
	public bool unLockSpawn { get; set; }
	public bool IsBuildingButtonOpen { get; private set; }
	public bool isHumanListButtonOpen { get; private set; }
	public bool isResourceButtonOpen { get; private set; }
	public bool isBuildingTypeButtonOpen { get; private set; }
	private int startTutorialNum;
	// Start is called before the first frame update
	void Awake()
    {
		thisImg = this.GetComponent<Image>();
		thisImg.color = new Color(1, 1, 1, 1/255f);
		TutorialNum = 0;
        StartTutorial = false;
		IsBuildingButtonOpen = false;
		isBuildingTypeButtonOpen = false;
		TutorialProceeding = false;
		unLockSpawn = false;
		startTutorialNum = 0;
		tutoImg.SetActive(false);
    }
	public void OnPointerClick(PointerEventData eventData)
	{
		switch (TutorialNum)
		{
			case 0:
				StartTuto();
				break;
		}
	}

	public void LockAllButton()
	{
		IsBuildingButtonOpen = false;
		isBuildingTypeButtonOpen = false;
		isHumanListButtonOpen = false;
		isResourceButtonOpen = false;
	}

	public void UnLockAllButton()
	{
		IsBuildingButtonOpen = true;
		isBuildingTypeButtonOpen = true;
		isHumanListButtonOpen = true;
		isResourceButtonOpen = true;
	}
	public void Update()
	{
		//Debug.Log(StartTutorial);
	}
	public void StartTuto()
	{
		switch (startTutorialNum)
		{
			case 0:
				GameManager.instance.GameStop = true;
				TutorialProceeding = true;
				StartTutorial = true;
				tutoImg.SetActive(true);
				LockAllButton();
				tutoImg.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1300f);
				tutoImg.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200f);
				tutoImg.GetComponent<RectTransform>().anchoredPosition3D = new Vector2(0, 300f);
				tutoText.text = "�� ���� ���� ���� ȯ���մϴ� !";
				startTutorialNum++;
				break;
			case 1:
				tutoText.text = "��� �ִ� �Ŷ�� �� ��ä�� �� ����� ����������...";
				startTutorialNum++;
				break;
			case 2:
				tutoText.text = "������ ����̶�� �и� �̰��� ������ų �� �����Ŷ� �Ͻ��ϴ� ! ";
				startTutorialNum++;
				break;
			case 3:
				tutoText.text = "������ �Ϸ��� �ǹ��� ���� ���� �˾ƾ� �ϰ���.";
				startTutorialNum++;
				break;
			case 4:
				tutoText.text = "�켱 �����ư�� �������ô� !";
				thisImg.enabled = false;
				IsBuildingButtonOpen = true;
				startTutorialNum++;
				break;
			case 5:
				tutoText.text = "�����ϴ�. �̹��� �������� ���θ��� ã�Ƽ� �ƹ������� ��ġ�غ��ô� !";
				LockAllButton();
				isBuildingTypeButtonOpen = true;
				startTutorialNum++;
				break;
			case 6:
				tutoText.text = "���?...����� ��� �Ǽ� ��� ���¿� �����ֱ���.";
				LockAllButton();
				thisImg.enabled = true;
				startTutorialNum++;
				break;
			case 7:
				tutoText.text = "���������� ����� ���ö����� ����Ѵ��� ������ Ŭ���غ��ô� !";
				unLockSpawn = true;
				thisImg.enabled = false;
				startTutorialNum++;
				break;
			case 8:
				tutoText.text = "���� ���� ���� ��ư�� ������, ����� �����غ��ô� !";
				unLockSpawn = false;
				startTutorialNum++;
				break;
			case 9:
				tutoText.text = "��! �Ǽ��� �˴ϴ�..!";
				thisImg.enabled = true;
				startTutorialNum++;
				break;
			case 10:
				tutoText.text = "�� �⼼�� ���� �� ���� �����س����ڱ���!! �׷� �̸�!";
				startTutorialNum++;
				break;
			case 11:
				UnLockAllButton();
				unLockSpawn = true;
				GameManager.instance.GameStop = false;
				TutorialProceeding = false;
				StartTutorial = false;
				tutoImg.SetActive(false);
				this.gameObject.SetActive(false);
				break;
		}

	}
}
