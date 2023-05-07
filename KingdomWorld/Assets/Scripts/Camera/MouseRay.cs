using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseRay : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private float distance = 50f;
    private RaycastHit[] hits;
    private Transform targetTransform;
    private Vector3 targetPosition;
    private bool isTarget;
    // Update is called once per frame
    void Update()
    {
        // 목표 지점이 있을 때
        if (isTarget && targetTransform != null)
        {
            // 카메라가 목표지점까지 이동
            targetPosition = new Vector3(targetTransform.position.x, 40, targetTransform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * 5f);
            // 일정거리거리에 가까워지면 목표지점 도달 성공
            if (Vector3.SqrMagnitude(this.transform.position - targetPosition) < 0.1f)
            {
                this.transform.position = targetPosition;
            }
        }

        if (Time.timeScale == 0) { return; }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (GameManager.instance.timeSpeed == 1f)
            {
                GameManager.instance.timeSpeed = 10f;
            }
            else
			{
                GameManager.instance.timeSpeed = 1f;
			}
        }


        if (!IsPointerOverUIObject())
		{
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward);
            hits = Physics.RaycastAll(ray, distance);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.GetComponent<BuildingSetting>() != null)
                {
                    uiManager.SetIsHpAndShieldBarUIObj(
                        true, hits[i].transform.GetComponent<BuildingSetting>().BuildingHp, hits[i].transform.GetComponent<BuildingSetting>().buildingShield,
                        hits[i].transform.GetComponent<BuildingSetting>().MaxBuildingHp, hits[i].transform.GetComponent<BuildingSetting>().maxBuildingShield,
                        hits[i].transform.GetComponent<BuildingSetting>().BuildingNum);
                    break;
                }
                else if (hits[i].transform.GetComponent<WaitingBuilding>() != null)
                {
                    uiManager.SetIsHpAndShieldBarUIObj(
                        true, (int)hits[i].transform.GetComponent<WaitingBuilding>().time, hits[i].transform.GetComponent<WaitingBuilding>().shield,
                        (int)hits[i].transform.GetComponent<WaitingBuilding>().maxTime, hits[i].transform.GetComponent<WaitingBuilding>().maxShield,
                        hits[i].transform.GetComponent<WaitingBuilding>().BuildingNum);
                    break;
                }
                else
                {
                    uiManager.SetIsHpAndShieldBarUIObj(false, 1, 1, 1, 1, -1);
                }
            }

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.GetComponent<NPC>() != null)
                {
                    uiManager.SetIsNPCHpAndShieldBarUIObj(
                        true, hits[i].transform.GetComponent<NPC>().HP, hits[i].transform.GetComponent<NPC>().Maxhp, hits[i].transform);
                    //Debug.Log(hits[i].transform.GetComponent<NPC>().HP + " "  + hits[i].transform.GetComponent<NPC>().Maxhp);
                    break;
                }
                else
                {
                    uiManager.SetIsNPCHpAndShieldBarUIObj(false, 1, 1, null);
                }
            }

        }

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward);
            hits = Physics.RaycastAll(ray, distance);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.GetComponent<BuildingColider>() != null)
                {
                    hits[i].transform.GetComponent<BuildingColider>().ClickObject();
                }
            }

            uiManager.SetIsOpenCitizenPanel(false, null);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.GetComponent<CitizenInfoPanel>() != null)
                {
                    uiManager.SetIsOpenCitizenPanel(true, hits[i].transform.GetComponent<CitizenInfoPanel>());
                    SetTargetTransform(hits[i].transform);
                    break;
                }
                isTarget = false;
            }
        }

        if (Input.GetMouseButtonDown(2) && !IsPointerOverUIObject())
        {
            //Debug.Log(1);
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward);
            hits = Physics.RaycastAll(ray, distance);
            for (int i = 0; i < hits.Length; i++)
            {

                if (hits[i].transform.GetComponent<BuildingColider>() != null)
                {
                    hits[i].transform.GetComponent<BuildingColider>().clickRemoveObject();
                }
            }
        }
    }
    public void SetTargetTransform(Transform trans)
    {
        targetTransform = trans;
        isTarget = true;
    }
    // ���콺 �����Ͱ� UI���� ������ true���� �ƴϸ� false�� ��ȯ�Ѵ�.
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
