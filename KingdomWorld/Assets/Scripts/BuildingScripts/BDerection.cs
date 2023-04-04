using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BDerection : MonoBehaviour, IPointerClickHandler
{
    public GameObject prefab; // 생성할 오브젝트 프리팹
    private GameObject clone; // 생성된 오브젝트 인스턴스
    private bool isDragging = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!isDragging)
        {
            clone = Instantiate(prefab);

            isDragging = true;
        }
        else
        {
            return;
        }
    }

    private void Update()
    { 

        if(isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - clone.transform.position;
            clone.transform.Translate(mousePosition);
        }
        if (Input.GetMouseButtonDown(0))
        {
        }
    }
}
