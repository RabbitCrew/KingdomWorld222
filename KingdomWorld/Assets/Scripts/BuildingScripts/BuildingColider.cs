using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingColider : MonoBehaviour
{
    [SerializeField] private int objTypeNum;
    public bool isFollowMouse { get; set; }
    public bool isSettingComplete { get; set; }
    public ulong objCode { get; set; }
    private List<Collider> colList = new List<Collider>();
    private CallBuildingButtonToBuildingColiderEventDriven callBuildingButtonToBuildingColiderEventDriven = new CallBuildingButtonToBuildingColiderEventDriven();
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
            CallSettingObjectToBuildingColiderEventDriven.RunGetObjectCodeEvent(objCode, this.gameObject);
        }
        colList.Clear();
    }

	public void ClickObject()
	{
        if (isFollowMouse)
        {
            Debug.Log(colList.Count);
            //Debug.Log("Asdfasdf");
            for (int i = 0; i < colList.Count; i++)
            {
                if (colList[i].GetComponent<SpriteRenderer>().color != Color.green)
                {
                    //Debug.Log(colList[i].GetComponent<SpriteRenderer>().color);
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
        }
	}
    public void clickRemoveObject()
    {
        if (isSettingComplete)
        {
            //Debug.Log("¸¸Æ®¶ó !");
            Destroy(this.gameObject);
        }
    }
}


