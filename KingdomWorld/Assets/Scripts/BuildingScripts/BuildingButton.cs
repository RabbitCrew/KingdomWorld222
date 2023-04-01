using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BuildingButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private BuildingAttachMouse buildingAttachMouse;
    public GameObject prefab; // ������ ������Ʈ ������
    //public static GameObject clone { get; private set; } // ������ ������Ʈ �ν��Ͻ�
    //public static bool isClick { get; private set; }

    List<GameObject> buildingList = new List<GameObject>();

    public void OnPointerClick(PointerEventData eventData)
    {
        buildingAttachMouse.CloneInst(prefab);
    }

}

