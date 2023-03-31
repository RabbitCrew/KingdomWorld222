using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BuildingButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject prefab; // 생성할 오브젝트 프리팹
    private static GameObject clone = null; // 생성된 오브젝트 인스턴스
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
            float plusX = 0;
            float plusZ = 0;
            if ((clone.transform.GetComponent<SpriteRenderer>().sprite.rect.width / 16) % 2 == 1)
            {
                plusX = 0f;
            }
            else
            {
                plusX = 0.5f;
            }

            if ((clone.transform.GetComponent<SpriteRenderer>().sprite.rect.height / 16) % 2 == 1)
            {
                plusZ = 0f;
            }
            else
            {
                plusZ = 0.5f;
            }

            clone.transform.position = new Vector3(Mathf.RoundToInt(mousePosition.x) + plusX, 0, Mathf.RoundToInt(mousePosition.z) + plusZ);

            //타일에 맞춰 한칸씩 마우스 이동
            //Debug.Log(mousePosition);
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

