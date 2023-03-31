using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BuildingButton2 : MonoBehaviour, IPointerClickHandler
{
    public GameObject prefab; // ������ ������Ʈ ������
    private static GameObject clone = null; // ������ ������Ʈ �ν��Ͻ�
    private static bool isClick = false;

    List<GameObject> buildingList = new List<GameObject>();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clone == null)
        {
            clone = Instantiate(prefab);
        }
        else
        {
            Destroy(clone.gameObject);
            clone = Instantiate(prefab);
        }
        isClick = true;
    }

    private void Update()
    {
        if (isClick)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clone.transform.position = new Vector3(mousePosition.x, 0, mousePosition.z);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (clone != null)
            {
                Destroy(clone.gameObject);
            }
            isClick = false;
        }
    }

}

