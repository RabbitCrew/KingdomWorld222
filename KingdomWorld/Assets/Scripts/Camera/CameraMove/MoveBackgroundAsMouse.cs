using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 마우스를 드래그하여 화면을 이동시킨다.
public class MoveBackgroundAsMouse : MonoBehaviour
{


    public Camera cam;

    private float mouseX, mouseY;
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && !IsPointerOverUIObject())
        {
            cam.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * 2;
            
            if (cam.orthographicSize > 10) { cam.orthographicSize = 10; }
            else ( cam.orthographicSize < 4) { cam.orthographicSize = 4; }
        }


        if (Input.GetMouseButton(0) && !IsPointerOverUIObject())
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            cam.transform.position += new Vector3(-mouseX * 0.9f,0,-mouseY * 0.9f);
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
