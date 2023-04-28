using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ÿ���� ������ ��� �ִ� Ŭ�����̴�.
public class TileInfo : MonoBehaviour
{
    public int TileNum { get; set; }
    // ���� Ÿ���� �θ������Ʈ�� Transform�� ��Ÿ����.
    private Transform parentTrans;
    // SettingObject�� CheckPossibleSettingBuilding�Լ��� �ҷ����� ���� ������ �����̴�.
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
