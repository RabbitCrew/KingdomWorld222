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
				tutoText.text = "이 땅에 오신 것을 환영합니다 !";
				startTutorialNum++;
				break;
			case 1:
				tutoText.text = "비록 있는 거라곤 집 한채와 빈 목공소 뿐이지만요...";
				startTutorialNum++;
				break;
			case 2:
				tutoText.text = "하지만 당신이라면 분명 이곳을 번영시킬 수 있을거라 믿습니다 ! ";
				startTutorialNum++;
				break;
			case 3:
				tutoText.text = "번영을 하려면 건물을 짓는 법을 알아야 하겠죠.";
				startTutorialNum++;
				break;
			case 4:
				tutoText.text = "우선 건축버튼을 눌러봅시다 !";
				thisImg.enabled = false;
				IsBuildingButtonOpen = true;
				startTutorialNum++;
				break;
			case 5:
				tutoText.text = "좋습니다. 이번엔 나무꾼의 오두막을 찾아서 아무곳에나 설치해봅시다 !";
				LockAllButton();
				buildingClickCount = 1;
				isBuildingTypeButtonOpen = true;
				startTutorialNum++;
				break;
			case 6:
				tutoText.text = "어라?...목수가 없어서 건설 대기 상태에 멈춰있군요.";
				LockAllButton();
				thisImg.enabled = true;
				startTutorialNum++;
				break;
			case 7:
				tutoText.text = "거주지에서 사람이 나올때까지 대기한다음 나오면 클릭해봅시다 !";
				unLockSpawn = true;
				thisImg.enabled = false;
				startTutorialNum++;
				break;
			case 8:
				tutoText.text = "이제 직업 바꾸기 버튼을 누르고, 목수를 선택해봅시다 !";
				startTutorialNum++;
				break;
			case 9:
				tutoText.text = "앗! 건설이 됩니다..!";
				unLockSpawn = false;
				thisImg.enabled = true;
				startTutorialNum++;
				break;
			case 10:
				tutoText.text = "이처럼 목수는 저희 마을에 건설을 담당하고 있습니다. 심지어 밭까지 갈아주죠. 만들고 싶은게 있다면 목수를 부릅시다!";
				startTutorialNum++;
				break;
			case 11:
				tutoText.text = "이번엔 창고를 만들어봅시다 !";
				LockAllButton();
				buildingClickCount = 2;
				thisImg.enabled = false;
				IsBuildingButtonOpen = true;
				isBuildingTypeButtonOpen = true;
				startTutorialNum++;
				break;
			case 12:
				tutoText.text = "창고에는 각종 물자들이 들어갑니다. 나무, 식량, 밀, 전부 다요. ";
				LockAllButton();
				thisImg.enabled = true;
				startTutorialNum++;
				break;
			case 13:
				tutoText.text = "그러면 이번엔 창고로 물자를 전달하는 창고지기를 시민 한명에게 배정해봅시다. 시민이 나올때까지 다시 대기합시다 !";
				jobButtonIndex = 6;
				unLockSpawn = true;
				thisImg.enabled = false;
				startTutorialNum++;
				break;
			case 14:
				tutoText.text = "직업 바꾸기에서 창고지기를 선택합시다 !";
				startTutorialNum++;
				break;
			case 15:
				tutoText.text = "아차차! 생각해보니 나무꾼이 없군요 ! 시민 한명이 나올때까지 대기한 후 나무꾼을 배정해봅시다.";
				unLockSpawn = true;
				jobButtonIndex = 1;
				startTutorialNum++;
				break;
			case 16:
				tutoText.text = "직업 바꾸기에서 나무꾼을 선택합시다 !";
				startTutorialNum++;
				break;
			case 17:
				tutoText.text = "이러면 나무꾼이 나무를 캐고, 창고지기가 오두막에 있는 나무를 창고로 가져가고..순환이 되죠";
				thisImg.enabled = true;
				startTutorialNum++;
				break;
			case 18:
				tutoText.text = "이 기세를 몰아 이 땅을 발전해나가자구요!! 인구 100명을 목표로 스타트!";
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
