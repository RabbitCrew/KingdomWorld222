using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// ���콺�� �巡���Ͽ� ȭ���� �̵���Ų��.
public class MoveBackgroundAsMouse : MonoBehaviour
{


    public Camera CAM;

    private float mouseX, mouseY;
    void Update()
    {

        if (Input.GetMouseButton(0) && !IsPointerOverUIObject())
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            CAM.transform.position += new Vector3(-mouseX * 0.9f,0,-mouseY * 0.9f);
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