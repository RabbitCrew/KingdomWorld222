using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BuildingButton2 : MonoBehaviour, IPointerClickHandler
{
    public GameObject prefab; // ������ ������Ʈ ������
    private GameObject clone; // ������ ������Ʈ �ν��Ͻ�
    private bool isDragging = false;

    List<GameObject> buildingList = new List<GameObject>();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isDragging)
        {
            clone = Instantiate(prefab);
            //clone.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            // Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - clone.transform.position;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clone.transform.position = new Vector3(mousePosition.x, 0, mousePosition.z);
            Debug.Log(mousePosition);
            Debug.Log(clone.transform.position);
        }
        //if (isDragging && Input.GetMouseButtonDown(0))
        //{
        //    isDragging = false;
        //}
    }

}

