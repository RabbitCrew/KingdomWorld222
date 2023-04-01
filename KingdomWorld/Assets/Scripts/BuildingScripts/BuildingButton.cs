using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BuildingButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private BuildingAttachMouse buildingAttachMouse;
    public GameObject prefab; // 생성할 오브젝트 프리팹
    //public static GameObject clone { get; private set; } // 생성된 오브젝트 인스턴스
    //public static bool isClick { get; private set; }

    List<GameObject> buildingList = new List<GameObject>();

    public void OnPointerClick(PointerEventData eventData)
    {
        buildingAttachMouse.CloneInst(prefab);
    }

}

