using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BuildingButton : MonoBehaviour, IPointerClickHandler
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

            //Ÿ�Ͽ� ���� ��ĭ�� ���콺 �̵�
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

