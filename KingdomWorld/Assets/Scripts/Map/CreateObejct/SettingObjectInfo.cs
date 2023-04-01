using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectNS;

public class SettingObjectInfo : MonoBehaviour
{

    private List<ObjectInfo> _objSize = new List<ObjectInfo>();

    //private int[] possibleTileTypeArr;
    //private int[] possibleTileTypeArr2;
    // �б� ���� ������Ƽ
    public List<ObjectInfo> objSize { 
        get
		{
            return _objSize;
		}
    }

    public void Awake()
    {
        //possibleTileTypeArr = new int[3] {(int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE, (int)TileNum.GLASS };
        //possibleTileTypeArr = new int[2] { (int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE };

        // ������Ʈ�� ���� ���� ����� �� �� ĭ ������(16�ȼ�)�� �������� ���ص�.
        objSize.Add(new ObjectInfo(1, 2, (int)ObjectTypeNum.TREE, new int[3] { (int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE, (int)TileNum.GLASS }));
        objSize.Add(new ObjectInfo(3, 3, (int)ObjectTypeNum.MINE, new int[3] { (int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE, (int)TileNum.GLASS }));
        objSize.Add(new ObjectInfo(3, 2, (int)ObjectTypeNum.ANEXCHANGE, new int[3] { (int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE, (int)TileNum.GLASS }));
        objSize.Add(new ObjectInfo(3, 2, (int)ObjectTypeNum.CARPENTERHOUSE, new int[3] { (int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE, (int)TileNum.GLASS }));
        objSize.Add(new ObjectInfo(3, 3, (int)ObjectTypeNum.CHEESEHOUSE, new int[3] { (int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE, (int)TileNum.GLASS }));
        objSize.Add(new ObjectInfo(3, 3, (int)ObjectTypeNum.FABRICHOUSE, new int[3] { (int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE, (int)TileNum.GLASS }));
        objSize.Add(new ObjectInfo(4, 2, (int)ObjectTypeNum.FARM, new int[3] { (int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE, (int)TileNum.GLASS }));
        objSize.Add(new ObjectInfo(3, 3, (int)ObjectTypeNum.HAMHOUSE, new int[3] { (int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE, (int)TileNum.GLASS }));
        objSize.Add(new ObjectInfo(2, 2, (int)ObjectTypeNum.HOUSE, new int[3] { (int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE, (int)TileNum.GLASS }));
        objSize.Add(new ObjectInfo(2, 2, (int)ObjectTypeNum.HUNTERHOUSE, new int[3] { (int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE, (int)TileNum.GLASS }));
        objSize.Add(new ObjectInfo(2, 2, (int)ObjectTypeNum.MINEWORKERHOUSE, new int[3] { (int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE, (int)TileNum.GLASS }));
        objSize.Add(new ObjectInfo(2, 3, (int)ObjectTypeNum.SMITHY, new int[3] { (int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE, (int)TileNum.GLASS }));
        objSize.Add(new ObjectInfo(3, 2, (int)ObjectTypeNum.STORAGE, new int[3] { (int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE, (int)TileNum.GLASS }));
        objSize.Add(new ObjectInfo(3, 3, (int)ObjectTypeNum.UNIVERSITAS, new int[3] { (int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE, (int)TileNum.GLASS }));
        objSize.Add(new ObjectInfo(2, 2, (int)ObjectTypeNum.WOODCUTTERHOUSE, new int[3] { (int)TileNum.BUMPYTILE, (int)TileNum.FLATTILE, (int)TileNum.GLASS }));
    }

}
