using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 마우스를 드래그하여 화면을 이동시킨다.
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
        //RaycastResult : BaseRaycastModule에서의 히트 결과.
        List<RaycastResult> results = new List<RaycastResult>();
        //EventSystem.current은 최근에 발생한 이벤트 시스템을 반환한다.
        //첫번째 인자값 : 현재 포인터 데이터.
        //두번째 인자값 : List of 'hits' to populate.
        //RaycastAll : 모두 설정된 BaseRaycaster를 사용을 통한 해당 씬으로의 레이 캐스팅.
        // -> 겹쳐있는 오브젝트들이 있다면 겹쳐있는 수로 results의 카운트가 바뀜
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        //Debug.Log(results.Count);
        return results.Count > 0;
    }
}
