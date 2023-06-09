using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 빌딩 오브젝트에 붙어있는 클래스이다.
// 해당 빌딩이 다른 타일과 접촉시, 해당 타일의 빌딩생성가능 여부를 판단하거나,
// 설치된 빌딩을 눌렀을 시 (레이가 해당 빌딩의 콜라이더에 반응하여 정보를 가져온다.) 발동하는 함수들에 대해 정리해두었다.
public class BuildingColider : MonoBehaviour
{
    [SerializeField] private int objTypeNum;
    public bool isFollowMouse { get; set; }
    public bool isSettingComplete { get; set; }
    public bool isBuildComplete { get; set; }
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
                    //Debug.Log(col.gameObject.GetComponent<TileInfo>().TileNum);
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
            //Debug.Log("만트라 ! " + isSettingComplete);
        }
	}
    public void clickRemoveObject()
    {
        //Debug.Log("만트라 !");
        if (isSettingComplete)
        {
            if (this.gameObject.GetComponent<BuildingSetting>() != null)
            {
                this.gameObject.GetComponent<BuildingSetting>().ReplenishNecessaryItem();
            }

            //Debug.Log("만트라 !");
            Destroy(this.gameObject);
        }
    }
}


