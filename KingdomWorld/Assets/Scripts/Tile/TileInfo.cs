using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    private Transform parentTrans;
    private SettingObject settingobj;
    // Start is called before the first frame update
    void Start()
    {
        parentTrans = this.transform.parent.transform;
    }

    public void InitSettingObject(SettingObject setObj)
	{
        settingobj = setObj;
	}

    public bool CheckPossibleSettingBuilding(int objTypeNum)
	{
        return settingobj.CheckPossibleSettingBuilding
            (
            objTypeNum,
            (int)parentTrans.localPosition.x /20, (int)parentTrans.localPosition.y/ 20, 
            (int)this.transform.localPosition.x, (int)this.transform.localPosition.y
            );
	}
}
