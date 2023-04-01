using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAttachMouse : MonoBehaviour
{
    [SerializeField] private SettingObject settingObj;
    public static GameObject clone { get; private set; } // ������ ������Ʈ �ν��Ͻ�
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
            //Debug.Log("true");
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
            //Ÿ�Ͽ� ���� ��ĭ�� ���콺 �̵�
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
            Debug.Log(clone.GetComponent<BuildingColider>().GetObjTypeNum());
            settingObj.AddTilePoint2((int)clone.transform.position.x, (int)clone.transform.position.z, clone.GetComponent<BuildingColider>().GetObjTypeNum());
        }
        //clone.GetComponent<BuildingColider>().isFollowMouse = false;
        clone = null;
        isClick = false;
    }
}
