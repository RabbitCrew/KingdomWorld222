using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectNS;

public class SettingObject : MonoBehaviour
{


    struct ChunkPoint
    {
        public int chunkX { get; }  // �б� ����. �����ڿ����� ���� ���� �� ����.
        public int chunkY { get; }

        // ������
        public ChunkPoint(int chunkX , int chunkY)
        {
            this.chunkX = chunkX;
            this.chunkY = chunkY;
        }
    }
    struct TilePoint
    {
        public int tileX { get; }   // �ش� ûũ �� Ÿ���� X�� ��ȣ 
        public int tileY { get; }   // �ش� ûũ �� Ÿ���� Y�� ��ȣ
        public int objectNum { get; }   // ������Ʈ ������ �з��ϴ� ��ȣ
        public ulong objectCode { get; }    // ������Ʈ �ĺ� �ڵ�
        public bool isRoot { get; } // ������Ʈ�� �߽��� �Ǵ� ��ġ���� ����
        public TilePoint(int tileX, int tileY, int objectNum, ulong objectCode, bool isRoot)
        {
            this.tileX = tileX;
            this.tileY = tileY;
            this.objectNum = objectNum;
            this.objectCode = objectCode;
            this.isRoot = isRoot;
        }
    }

    [SerializeField] private GameObject[] objectArr;
    [SerializeField] private GameObject motehrObject;
    [SerializeField] private SettingObjectInfo settingObjInfo;
    // ûũ ��ǥ�� ���� ������Ʈ ��ǥ ����Ʈ
    private  Dictionary<ChunkPoint,List <TilePoint>> objectPointList = new Dictionary<ChunkPoint, List<TilePoint>>();
    // ûũ ��ǥ�� ���� ���ӿ�����Ʈ ����Ʈ
    private Dictionary<ChunkPoint, List<GameObject>> gameObjectChunkPointList = new Dictionary<ChunkPoint, List<GameObject>>();

    private ulong objCode = 0;

	public void Awake()
	{
        CallSettingObjectToBuildingColiderEventDriven.getObjectCodeEvent += RemoveObject;
        RemoveEventDriven.isRemoveEvent += RemoveEvent;

    }

    private void RemoveEvent()
	{
        CallSettingObjectToBuildingColiderEventDriven.getObjectCodeEvent -= RemoveObject;
        RemoveEventDriven.isRemoveEvent -= RemoveEvent;
    }

    // �ش� ûũ ��ǥ ������ ��ųʸ��� Ű�� �����ϰ� �ִ��� Ȯ��. ������ true ���� ������ false ����
    public bool ActiveTrueObjectPointList(int chunkX, int chunkY)
    {
        if (objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
        {
            return true;
        }
        return false;
    }

    // tileX, tileY�� 0~19 ���� ���
    public void AddObjectPointList(int chunkX, int chunkY, int objectNum, int tileX, int tileY)
    {
        // �ش� ûũ ��ǥ ������ ��ųʸ� Ű�� �����ϰ� �ִ��� Ȯ��
        if (!objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
        {
            // Ű�� �����ϰ� ���� ������ Ű�� ����.
            List<TilePoint> tilePoint = new List<TilePoint>();
            objectPointList.Add(new ChunkPoint(chunkX, chunkY), tilePoint);
            // ûũ��ǥ�� ������Ʈ ������ ����.
            AddTilePoint(chunkX, chunkY, objectNum, tileX, tileY);
        }
        else
        {
            //int index = objectPointList[new ChunkPoint(chunkX, chunkY)].FindIndex(a => a.tileX == tileX && a.tileY == tileY);
            //if (index == -1)
            //{
                AddTilePoint(chunkX, chunkY, objectNum, tileX, tileY);
            //}

            //return;
        }
    }
    // ûũ ��ǥ�� ������Ʈ�� ������ ����. 
    private void AddTilePoint(int chunkX, int chunkY, int objectNum, int tileX, int tileY)
    {
        int index = settingObjInfo.objSize.FindIndex(a => a.objNum == objectNum);
        // ������ ������Ʈ �߾��� �������� ��� Ÿ�Ͽ� ������Ʈ�� ��� �Ǵ��� ���� ���
        int minX = tileX - (int)((float)settingObjInfo.objSize[index].sizeX / 2f) + ((settingObjInfo.objSize[index].sizeX + 1) % 2);
        int maxX = tileX + (int)((float)settingObjInfo.objSize[index].sizeX / 2f);
        int minY = tileY - (int)((float)settingObjInfo.objSize[index].sizeY / 2f) + ((settingObjInfo.objSize[index].sizeY + 1) % 2);
        int maxY = tileY + (int)((float)settingObjInfo.objSize[index].sizeY / 2f);

        // �ٸ� ��ǥ�� ���� ������Ʈ�� �����ϰ� �ִ� ������Ʈ ��ȣ. ulongŸ��
        objCode++;
        // ���� �� ��� �ִ� Ÿ���� ��ǥ�� ������Ʈ ��ȣ�� �Բ� ����
        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                if (tileX == i && tileY == j)
                {
                    objectPointList[new ChunkPoint(chunkX, chunkY)].Add(new TilePoint(i, j, objectNum, objCode, true));
                }
                else
                {
                    objectPointList[new ChunkPoint(chunkX, chunkY)].Add(new TilePoint(i, j, objectNum, objCode, false));
                }
            }
        }
    }

    public void AddTilePoint2(int pointX, int pointZ, int objectNum, GameObject obj)
    {
        int minX = pointX - (int)((float)settingObjInfo.objSize[objectNum].sizeX / 2f) + ((settingObjInfo.objSize[objectNum].sizeX + 1) % 2);
        int maxX = pointX + (int)((float)settingObjInfo.objSize[objectNum].sizeX / 2f);
        int minY = pointZ - (int)((float)settingObjInfo.objSize[objectNum].sizeY / 2f) + ((settingObjInfo.objSize[objectNum].sizeY + 1) % 2);
        int maxY = pointZ + (int)((float)settingObjInfo.objSize[objectNum].sizeY / 2f);
        objCode++;

        if (obj.GetComponent<BuildingColider>() != null)
		{
            obj.GetComponent<BuildingColider>().objCode = objCode;
		}


        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                int chunkX, chunkY, tileX, tileY;

                if (i >= 0) { chunkX = i / 20; }
                else
                {
                    chunkX = (i / 20) - 1;
                }

                if (j >= 0) { chunkY = j / 20; }
                else
                {
                    chunkY = (j / 20) - 1;
                }
                if (i >= 0) { tileX = i % 20; }
                else 
                {
                    if (i % 20 == 0) { tileX = 0; chunkX++; }
                    else { tileX = (i % 20) + 20; } 
                }
               
                if (j >= 0) { tileY = j % 20; }
                else 
                {
                    if (j % 20 == 0) { tileY = 0; chunkY++; }
                    else { tileY = (j % 20) + 20; } 
                }
                

                if (!objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
                {
                    List<TilePoint> tilePoint = new List<TilePoint>();
                    objectPointList.Add(new ChunkPoint(chunkX, chunkY), tilePoint);
                }

                if (i == pointX && j == pointZ)
				{
                    objectPointList[new ChunkPoint(chunkX, chunkY)].Add(new TilePoint(tileX, tileY, objectNum, objCode, true));
                
                    if (!gameObjectChunkPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
					{
                        List<GameObject> objList = new List<GameObject>();
                        gameObjectChunkPointList.Add(new ChunkPoint(chunkX,chunkY), objList);
                        gameObjectChunkPointList[new ChunkPoint(chunkX, chunkY)].Add(obj);
                    }
                    else
					{
                        gameObjectChunkPointList[new ChunkPoint(chunkX, chunkY)].Add(obj);
                    }
                }
                else
				{
                    objectPointList[new ChunkPoint(chunkX, chunkY)].Add(new TilePoint(tileX, tileY, objectNum, objCode, false));
                }
                //Debug.Log("chunkX : " + chunkX + " chunkY : " + chunkY + " tileX : " + tileX + " tileY : " + tileY + " objectNum : " + objectNum + "  i : " + i + "  j : " + j + " pointX : " + pointX + "pointZ : " + pointZ);
            }
        }


    }


    // ��ǥ�� �°� ������Ʈ�� �����Ѵ�.
    public void CreateObejct(int chunkX, int chunkY)
    {
        // �ش� ûũ ��ǥ ������ ��ųʸ��� Ű�� �����ϰ� �ִ��� Ȯ��. ������ ����
        if (!ActiveTrueObjectPointList(chunkX, chunkY)) { return; }

        ChunkPoint chunk = new ChunkPoint(chunkX, chunkY);
        for (int i = 0; i < objectPointList[chunk].Count; i++)
        {
            if (objectPointList[chunk][i].isRoot)
            {// ������Ʈ ������ �θ� ������Ʈ ����
                GameObject obj = Instantiate(objectArr[objectPointList[chunk][i].objectNum], motehrObject.transform);
                // ������ ������Ʈ�� ��ǥ
                obj.transform.position = new Vector3
                    (
                    chunkX * 20 + objectPointList[chunk][i].tileX,
                    0,
                    chunkY * 20 + objectPointList[chunk][i].tileY
                    ); ;
                // ȭ�鿡 ���̰� ���� ����
                obj.transform.eulerAngles = new Vector3(90, 0, 0);
                // �ش� ûũ ��ǥ key�� �ִ��� Ȯ��
                if (!gameObjectChunkPointList.ContainsKey(chunk))
                {
                    // Ű�� ������ Ű ���� �� �� ûũ ��ǥ �� ������Ʈ�� value ������ �߰�
                    List<GameObject> objList = new List<GameObject>();
                    gameObjectChunkPointList.Add(chunk, objList);
                    gameObjectChunkPointList[chunk].Add(obj);
                }
                else
                {
                    // ûũ ��ǥ �� ������Ʈ�� value ������ �߰�
                    gameObjectChunkPointList[chunk].Add(obj);
                }
            }
        }
    }

    public void RemoveObject(ulong objCode, GameObject obj)
	{
        int inaccurateChunkX = (int)obj.transform.position.x / 20;
        int inaccurateChunkY = (int)obj.transform.position.z / 20;

        for (int x = inaccurateChunkX - 2; x < inaccurateChunkX + 2; x++)
		{
            for (int y = inaccurateChunkY - 2; y < inaccurateChunkY + 2; y++)
			{
                if (gameObjectChunkPointList.ContainsKey(new ChunkPoint(x, y)))
				{
                    int index = gameObjectChunkPointList[new ChunkPoint(x, y)].FindIndex(a => a.Equals(obj));

                    if (index != -1)
					{
                        gameObjectChunkPointList[new ChunkPoint(x, y)].RemoveAt(index);
					}
				}

                if (objectPointList.ContainsKey(new ChunkPoint(x, y)))
				{
                    List<TilePoint> list = objectPointList[new ChunkPoint(x, y)].FindAll(a => a.objectCode == objCode);

                    for (int i = 0; i < list.Count; i++)
					{
                        objectPointList[new ChunkPoint(x, y)].Remove(list[i]);
					}
                    list.Clear();
				}
			}
		}
	}

    // ������ ���� ���� ������ ������ ������ �����ϴ��� ���θ� �Ǵ�
    public bool CheckMineRange(int chunkX, int chunkY, GameObject chunk, int x, int y, Sprite tile, int chunkSize)
    {
        // ������ ������ ���� 3(*16�ȼ�),���� 3(*16�ȼ�)��. ���� �����¿�� ��ĭ ������ ûũ ������ ������ Ȯ���ؾߵ�
        if (x - 1 <= 0 || x + 1 >= chunkSize || y - 1 <= 0 || y + 1 >= chunkSize) { return false; }
        // ���� Ȯ�� ��, �ش� ��ǥ �� �̹� ������Ʈ�� �����Ǿ� �ִ���, Ȥ�� ������ �Ұ������ϴ� Ÿ���� �ִ��� Ȯ����
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
                {
                    int index = objectPointList[new ChunkPoint(chunkX, chunkY)].FindIndex(a => a.tileX == i && a.tileY == j);
                    if (index != -1)
                    {
                        return false;
                    }
                }
                if (chunk.transform.GetChild(i * chunkSize + j).GetComponent<SpriteRenderer>().sprite == tile)
                {
                    return false;
                }
            }
        }

        return true;
    }

    // ������ ���� ���� ������ ������ ������ �����ϴ��� ���θ� �Ǵ�
    public bool CheckTreeRange(int chunkX, int chunkY, GameObject chunk, int x, int y, Sprite tile, int chunkSize)
    {
        // ������ ������ ���� 1(*16�ȼ�), ���� 2(*16)�ȼ���. ���� ��ĭ ���������� ûũ ������ ������ Ȯ���ؾߵ�
        if (y + 1 >= chunkSize) { return false; }
        // ���� Ȯ�� ��, �ش� ��ǥ �� �̹� ������Ʈ�� �����Ǿ� �ִ���, Ȥ�� ������ �Ұ������ϴ� Ÿ���� �ִ��� Ȯ����.
        for (int i = y; i <= y + 1; i++)
        {
            if (objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
            {
                int index = objectPointList[new ChunkPoint(chunkX, chunkY)].FindIndex(a => a.tileX == x && a.tileY == i);
                if (index != -1)
                {
                    return false;
                }
            }
            if (chunk.transform.GetChild(x * chunkSize + i).GetComponent<SpriteRenderer>().sprite == tile)
            {
                return false;
            }
        }

        return true;
    }
    // Ȱ��ȭ�� ûũ ��ġ �ִ� ������Ʈ�� ��������Ʈ �������� ���ش�.
    public void EnableSpriteRenderer(int chunkX, int chunkY)
    {
        if (!gameObjectChunkPointList.ContainsKey(new ChunkPoint(chunkX, chunkY))) { return; }

        for (int i = 0; i < gameObjectChunkPointList[new ChunkPoint(chunkX, chunkY)].Count; i++)
        {
            gameObjectChunkPointList[new ChunkPoint(chunkX, chunkY)][i].GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    // ��Ȱ��ȭ�� ûũ ��ġ �ִ� ������Ʈ�� ��������Ʈ �������� ���ش�.
    public void DisableSpriteRenderer(int chunkX, int chunkY)
    {
        if (!gameObjectChunkPointList.ContainsKey(new ChunkPoint(chunkX, chunkY))) { return; }

        for (int i = 0; i < gameObjectChunkPointList[new ChunkPoint(chunkX, chunkY)].Count; i++)
        {
            gameObjectChunkPointList[new ChunkPoint(chunkX, chunkY)][i].GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public bool CheckPossibleSettingBuilding(int objTypeNum, int chunkX, int chunkY, int tileX, int tileY)
	{
        // ûũ ��ǥ Ű�� ���� ���
        if (objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
		{
            int index = objectPointList[new ChunkPoint(chunkX, chunkY)].FindIndex(a => a.tileX == tileX && a.tileY == tileY);
            int index2 = settingObjInfo.objSize.FindIndex(a => a.objNum == objTypeNum);
            // ûũ ��ǥ Ű�� ���� �� ����Ʈ�� Ÿ�Ͽ� ���� ������ ������� ���
            if (index != -1)
			{
                return false;
			}
            else if (index2 != 1)
			{
                if (this.GetComponent<PerlinNoiseMapMaker>().CheckPossibleSettingBuilding(
                    settingObjInfo.objSize[index2].possibleTileArr, chunkX, chunkY, tileX, tileY))
				{
                    return true;
				}
			}
		}

        return false;
	}
}
