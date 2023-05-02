using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// ���� ������Ʈ�� �پ��ִ� Ŭ�����̴�.
// �ش� ������ �ٸ� Ÿ�ϰ� ���˽�, �ش� Ÿ���� ������������ ���θ� �Ǵ��ϰų�,
// ��ġ�� ������ ������ �� (���̰� �ش� ������ �ݶ��̴��� �����Ͽ� ������ �����´�.) �ߵ��ϴ� �Լ��鿡 ���� �����صξ���.
public class BuildingColider : MonoBehaviour
{
    [SerializeField] private int objTypeNum;
    public bool isFollowMouse { get; set; }
    public bool isSettingComplete { get; set; }
    public ulong objCode { get; set; }
    private List<Collider> colList = new List<Collider>();
    private CallBuildingAttachMouseToBuildingColiderEventDriven callBuildingButtonToBuildingColiderEventDriven = new CallBuildingAttachMouseToBuildingColiderEventDriven();
    private CallSettingObjectToBuildingColiderEventDriven CallSettingObjectToBuildingColiderEventDriven = new CallSettingObjectToBuildingColiderEventDriven();
    public int GetObjTypeNum()
	{
        return objTypeNum;
	}

    private void OnTriggerEnter(Collider col)
    {
        if (!isSettingComplete)
        {
            colList.Add(col);
            if (col.gameObject.GetComponent<TileColorChange>() != null)
            {
                if (col.gameObject.GetComponent<TileInfo>() != null)
                {
                    if (col.gameObject.GetComponent<TileInfo>().CheckPossibleSettingBuilding(objTypeNum))
                    {
                        col.gameObject.GetComponent<TileColorChange>().ChangeGreenColor();
                    }
                    else
                    {
                        col.gameObject.GetComponent<TileColorChange>().ChangeRedColor();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<TileColorChange>() != null)
        {
            col.gameObject.GetComponent<TileColorChange>().ChangeWhiteColor();
        }
        colList.Remove(col);

    }
    private void OnEnable()
	{
        objCode = 0;
        isSettingComplete = false;
        colList.Clear();
    }
    private void OnDisable()
    {
        //Debug.Log(isSettingComplete);

        if (!isSettingComplete)
        {
            for (int i = 0; i < colList.Count; i++)
            {
                if (colList[i] == null)
                {
                    return;
                }

                if (colList[i].gameObject.GetComponent<TileColorChange>() != null)
                {
                    colList[i].gameObject.GetComponent<TileColorChange>().ChangeWhiteColor();
                }
            }
        }
        else
		{
            //Debug.Log(this.gameObject.name);
            CallSettingObjectToBuildingColiderEventDriven.RunGetObjectCodeEvent(objCode, this.gameObject);
        }
        colList.Clear();
    }

	public void ClickObject()
	{
        if (isFollowMouse)
        {
            for (int i = 0; i < colList.Count; i++)
            {
                if (colList[i] == null) { colList.RemoveAt(i); i = 0; continue; }
                if (colList[i].GetComponent<SpriteRenderer>() == null) { colList.RemoveAt(i); i = 0; continue; }
                if (colList[i].GetComponent<SpriteRenderer>().color != Color.green)
                {
                    return;
                }
            }

            for (int i = 0; i < colList.Count; i++)
            {
                if (colList[i].gameObject.GetComponent<TileColorChange>() != null)
                {
                    colList[i].gameObject.GetComponent<TileColorChange>().ChangeWhiteColor();
                }
            }

            colList.Clear();

            callBuildingButtonToBuildingColiderEventDriven.RunIsClickFalseEvent();
            isFollowMouse = false;
            isSettingComplete = true;
            //Debug.Log("��Ʈ�� ! " + isSettingComplete);
        }
	}
    public void clickRemoveObject()
    {
        Debug.Log("��Ʈ�� !");
        if (isSettingComplete)
        {
            if (this.gameObject.GetComponent<BuildingSetting>() != null)
            {
                this.gameObject.GetComponent<BuildingSetting>().ReplenishNecessaryItem();
            }

            //Debug.Log("��Ʈ�� !");
            Destroy(this.gameObject);
        }
    }
}


