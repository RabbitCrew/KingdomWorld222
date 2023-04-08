using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseRay : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private float distance = 50f;
    private RaycastHit[] hits;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            //Debug.Log(1);
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward);
            hits = Physics.RaycastAll(ray, distance);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.GetComponent<BuildingColider>() != null)
                {
                    //Debug.Log(hits[i].transform.name);
                    hits[i].transform.GetComponent<BuildingColider>().ClickObject();
                }
            }

            uiManager.SetIsOpenCitizenPanel(false, null);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.GetComponent<CitizenInfoPanel>() != null)
                {
                    uiManager.SetIsOpenCitizenPanel(true, hits[i].transform.GetComponent<CitizenInfoPanel>());
                }
            }
        }

        if (Input.GetMouseButtonDown(2) && !IsPointerOverUIObject())
        {
            Debug.Log(1);
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward);
            hits = Physics.RaycastAll(ray, distance);
            for (int i = 0; i < hits.Length; i++)
            {

                if (hits[i].transform.GetComponent<BuildingColider>() != null)
                {
                    //Debug.Log(hits[i].transform.name);
                    hits[i].transform.GetComponent<BuildingColider>().clickRemoveObject();
                }
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //RaycastResult : BaseRaycastModule������ ��Ʈ ���.
        List<RaycastResult> results = new List<RaycastResult>();
        //EventSystem.current�� �ֱٿ� �߻��� �̺�Ʈ �ý����� ��ȯ�Ѵ�.
        //ù��° ���ڰ� : ���� ������ ������.
        //�ι�° ���ڰ� : List of 'hits' to populate.
        //RaycastAll : ��� ������ BaseRaycaster�� ����� ���� �ش� �������� ���� ĳ����.
        // -> �����ִ� ������Ʈ���� �ִٸ� �����ִ� ���� results�� ī��Ʈ�� �ٲ�
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        //Debug.Log(results.Count);
        return results.Count > 0;
    }
}
