using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 타일의 정보를 담고 있는 클래스이다.
public class TileInfo : MonoBehaviour
{
    public int TileNum { get; set; }
    // 현재 타일의 부모오브젝트의 Transform을 나타낸다.
    private Transform parentTrans;
    // SettingObject의 CheckPossibleSettingBuilding함수를 불러오기 위해 생성한 변수이다.
    private SettingObject settingobj;
    // Start is called before the first frame update
    void Start()
    {
        TileNum = -1;
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
