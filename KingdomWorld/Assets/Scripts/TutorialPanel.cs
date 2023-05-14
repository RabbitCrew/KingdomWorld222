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
	public bool isPossibleDeleteBuilding { get; private set; }
	public bool isAutoNPCWorkButtonOpen { get; private set; }
	public bool isNightSpeedUpButtonOpen { get; private set; }
	public int jobButtonIndex { get; private set; }
	public int buildingClickCount { get; private set; }
	private int startTutorialNum;
	// Start is called before the first frame update
	void Awake()
    {
		thisImg = this.GetComponent<Image>();
		thisImg.color = new Color(1, 1, 1, 1/255f);
		TutorialNum = 0;
		buildingClickCount = 1;
		jobButtonIndex = 2;
		StartTutorial = false;
		UnLockAllButton();
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
		isPossibleDeleteBuilding = false;
		isAutoNPCWorkButtonOpen = false;
		isNightSpeedUpButtonOpen = false;
	}

	public void UnLockAllButton()
	{
		IsBuildingButtonOpen = true;
		isBuildingTypeButtonOpen = true;
		isHumanListButtonOpen = true;
		isResourceButtonOpen = true;
		isPossibleDeleteBuilding = true;
		isAutoNPCWorkButtonOpen = true;
		isNightSpeedUpButtonOpen = true;
	}
	public void StartTuto()
	{
		switch (startTutorialNum)
		{
			case 0:
				GameManager.instance.GameStop = true;
				TutorialProceeding = true;
				StartTutorial = true;
				buildingClickCount = 1;
				jobButtonIndex = 2;
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
				buildingClickCount = 1;
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
				tutoText.text = "���� ���� �ٲٱ� ��ư�� ������, ����� �����غ��ô� !";
				startTutorialNum++;
				break;
			case 9:
				tutoText.text = "��! �Ǽ��� �˴ϴ�..!";
				unLockSpawn = false;
				thisImg.enabled = true;
				startTutorialNum++;
				break;
			case 10:
				tutoText.text = "��ó�� ����� ���� ������ �Ǽ��� ����ϰ� �ֽ��ϴ�. ������ ����� ��������. ����� ������ �ִٸ� ����� �θ��ô�!";
				startTutorialNum++;
				break;
			case 11:
				tutoText.text = "�̹��� â�� �����ô� !";
				LockAllButton();
				buildingClickCount = 2;
				thisImg.enabled = false;
				IsBuildingButtonOpen = true;
				isBuildingTypeButtonOpen = true;
				startTutorialNum++;
				break;
			case 12:
				tutoText.text = "â���� ���� ���ڵ��� ���ϴ�. ����, �ķ�, ��, ���� �ٿ�. ";
				LockAllButton();
				thisImg.enabled = true;
				startTutorialNum++;
				break;
			case 13:
				tutoText.text = "�׷��� �̹��� â��� ���ڸ� �����ϴ� â�����⸦ �ù� �Ѹ��� �����غ��ô�. �ù��� ���ö����� �ٽ� ����սô� !";
				jobButtonIndex = 6;
				unLockSpawn = true;
				thisImg.enabled = false;
				startTutorialNum++;
				break;
			case 14:
				tutoText.text = "���� �ٲٱ⿡�� â�����⸦ �����սô� !";
				startTutorialNum++;
				break;
			case 15:
				tutoText.text = "������! �����غ��� �������� ������ ! �ù� �Ѹ��� ���ö����� ����� �� �������� �����غ��ô�.";
				unLockSpawn = true;
				jobButtonIndex = 1;
				startTutorialNum++;
				break;
			case 16:
				tutoText.text = "���� �ٲٱ⿡�� �������� �����սô� !";
				startTutorialNum++;
				break;
			case 17:
				tutoText.text = "�̷��� �������� ������ ĳ��, â�����Ⱑ ���θ��� �ִ� ������ â��� ��������..��ȯ�� ����";
				thisImg.enabled = true;
				startTutorialNum++;
				break;
			case 18:
				tutoText.text = "�� �⼼�� ���� �� ���� �����س����ڱ���!! �α� 100���� ��ǥ�� ��ŸƮ!";
				startTutorialNum++;
				break;
			case 19:
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
