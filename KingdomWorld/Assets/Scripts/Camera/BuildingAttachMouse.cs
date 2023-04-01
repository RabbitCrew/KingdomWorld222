using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAttachMouse : MonoBehaviour
{
    [SerializeField] private SettingObject settingObj;
    [SerializeField] private GameObject motherBuildingObject;
    public static GameObject clone { get; private set; } // 생성된 오브젝트 인스턴스
    public static bool isClick { get; private set; }

    public void Awake()
    {
        CallBuildingButtonToBuildingColiderEventDriven.isClickFalseEvent += DetachClone;
        RemoveEventDriven.isRemoveEvent += RemoveEvent;
        isClick = false;
        clone = null;
    }

    private void RemoveEvent()
    {
        CallBuildingButtonToBuildingColiderEventDriven.isClickFalseEvent -= DetachClone;
        RemoveEventDriven.isRemoveEvent -= RemoveEvent;
    }

    public void CloneInst(GameObject obj)
	{
        if (clone == null)
        {
            clone = Instantiate(obj);
        }
        else
        {
            Destroy(clone.gameObject);
            clone = Instantiate(obj);
        }

        if (clone.GetComponent<BuildingColider>() != null)
        {
            clone.GetComponent<BuildingColider>().isFollowMouse = true;
        }

        isClick = true;
    }

    // Update is called once per frame
    void Update()
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
            //타일에 맞춰 한칸씩 마우스 이동
            clone.transform.position = new Vector3(Mathf.RoundToInt(mousePosition.x) + plusX, 0, Mathf.RoundToInt(mousePosition.z) + plusZ);
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

    private void DetachClone()
    {
        if (clone.GetComponent<BuildingColider>() != null)
        {
            clone.transform.parent = motherBuildingObject.transform;
            float x, z;
            //Debug.Log(clone.transform.localPosition.x + " " + clone.transform.localPosition.z);
            if (clone.transform.localPosition.x % 1 != 0) { x = -0.5f; }
            else { x = 0; }

            if (clone.transform.localPosition.z % 1 != 0) { z = -0.5f; }
            else { z = 0; }

            settingObj.AddTilePoint2((int)(clone.transform.localPosition.x + x), (int)(clone.transform.localPosition.z + z), clone.GetComponent<BuildingColider>().GetObjTypeNum(), clone);
        }
        //clone.GetComponent<BuildingColider>().isFollowMouse = false;
        clone = null;
        isClick = false;
    }
}
