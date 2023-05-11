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
				tutoText.text = "이제 직업 고르기 버튼을 누르고, 목수를 선택해봅시다 !";
				unLockSpawn = false;
				startTutorialNum++;
				break;
			case 9:
				tutoText.text = "앗! 건설이 됩니다..!";
				thisImg.enabled = true;
				startTutorialNum++;
				break;
			case 10:
				tutoText.text = "이 기세를 몰아 이 땅을 발전해나가자구요!! 그럼 이만!";
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
