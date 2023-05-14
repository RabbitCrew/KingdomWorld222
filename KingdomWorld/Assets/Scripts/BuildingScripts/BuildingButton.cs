using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// ������ �����س��� �������� BuildingAttachMouseŬ�������� ������ �� �ְ� ���ִ� Ŭ�����̴�.
public class BuildingButton : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    [SerializeField] private BuildingAttachMouse buildingAttachMouse;
    [SerializeField] private CautionUI cautionUI;
    public GameObject prefab; // ������ ������Ʈ ������
    public int tuto;
    //public static GameObject clone { get; private set; } // ������ ������Ʈ �ν��Ͻ�
    //public static bool isClick { get; private set; }
    private Image img;
    private bool isImpossibleBuild;
    List<GameObject> buildingList = new List<GameObject>();

	public void Start()
	{
        img = this.GetComponent<Image>();
	}

	// �ش� Ŭ������ ���� ������Ʈ�� �ѹ� Ŭ���� �� �ߵ��Ѵ�.
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

