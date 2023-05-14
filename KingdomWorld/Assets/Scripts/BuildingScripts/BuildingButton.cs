using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 누르면 지정해놓은 프리팹을 BuildingAttachMouse클래스에서 생성할 수 있게 해주는 클래스이다.
public class BuildingButton : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    [SerializeField] private BuildingAttachMouse buildingAttachMouse;
    [SerializeField] private CautionUI cautionUI;
    public GameObject prefab; // 생성할 오브젝트 프리팹
    public int tuto;
    //public static GameObject clone { get; private set; } // 생성된 오브젝트 인스턴스
    //public static bool isClick { get; private set; }
    private Image img;
    private bool isImpossibleBuild;
    List<GameObject> buildingList = new List<GameObject>();

	public void Start()
	{
        img = this.GetComponent<Image>();
	}

	// 해당 클래스가 붙은 오브젝트를 한번 클릭할 시 발동한다.
	public void OnPointerDown(PointerEventData eventData)
    {
        if (!buildingAttachMouse.CheckResourceToMakeBuilding(prefab))
		{
            img.color = Color.red;
            cautionUI.SetActiveTrueCautionObj(1);
            isImpossibleBuild = true;
		}
        else
		{
            isImpossibleBuild = false;
		}
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.instance.ReturnTutorialPanel().TutorialProceeding)
        {
            if (!(GameManager.instance.ReturnTutorialPanel().buildingClickCount == tuto && GameManager.instance.ReturnTutorialPanel().isBuildingTypeButtonOpen))
            { return; }
        }

        if (!isImpossibleBuild)
        {
            buildingAttachMouse.CloneInst(prefab);
        }
        else
		{
            img.color = Color.white;
		}
    }
}

