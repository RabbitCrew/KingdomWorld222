using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// ������ �����س��� �������� BuildingAttachMouseŬ�������� ������ �� �ְ� ���ִ� Ŭ�����̴�.
public class BuildingButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private BuildingAttachMouse buildingAttachMouse;
    public GameObject prefab; // ������ ������Ʈ ������
    //public static GameObject clone { get; private set; } // ������ ������Ʈ �ν��Ͻ�
    //public static bool isClick { get; private set; }

    List<GameObject> buildingList = new List<GameObject>();
    // �ش� Ŭ������ ���� ������Ʈ�� �ѹ� Ŭ���� �� �ߵ��Ѵ�.
    public void OnPointerClick(PointerEventData eventData)
    {
        buildingAttachMouse.CloneInst(prefab);
    }

}

