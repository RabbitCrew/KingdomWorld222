using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// ���콺�� �巡���Ͽ� ȭ���� �̵���Ų��.
public class MoveBackgroundAsMouse : MonoBehaviour
{


    public Camera cam;

    private float mouseX, mouseY;
    private float preMouseX, preMouseY;
    private void Awake()
    {
        preMouseX = 0;
        preMouseY = 0;
    }

    void Update()
    {
        if (GameManager.instance.GameStop) { return; }

        if (Input.mousePosition.x <= Screen.width && Input.mousePosition.x >= 0 &&
            Input.mousePosition.y <= Screen.height && Input.mousePosition.y >= 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0 && !IsPointerOverUIObject())
            {
                cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * 2;

                if (cam.orthographicSize > 14) { cam.orthographicSize = 14; }
                else if (cam.orthographicSize < 4) { cam.orthographicSize = 4; }
            }


            if (Input.GetMouseButton(0) && !IsPointerOverUIObject())
            {
                //Debug.Log(Input.GetAxis("Mouse X") + " " + Input.GetAxis("Mouse Y"));
                mouseX = Input.GetAxis("Mouse X");
                mouseY = Input.GetAxis("Mouse Y");
                
                if (Mathf.Abs(mouseX - preMouseX) > 5) { mouseX = 0; }
                if (Mathf.Abs(mouseY - preMouseY) > 5) { mouseY = 0; }

                cam.transform.position += new Vector3(-mouseX * 0.9f, 0, -mouseY * 0.9f);
                
                preMouseX = mouseX;
                preMouseY = mouseY;
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
