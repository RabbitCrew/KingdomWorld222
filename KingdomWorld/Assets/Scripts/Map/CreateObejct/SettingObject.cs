using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectNS;

public class SettingObject : MonoBehaviour
{
    struct ChunkPoint
    {
        public int chunkX { get; }  // �б� ����. �����ڿ����� ���� ���� �� ����.
        public int chunkY { get; }  // �б� ����. �����ڿ����� ���� ���� �� ����.

        // ������
        public ChunkPoint(int chunkX , int chunkY)
        {
            this.chunkX = chunkX;
            this.chunkY = chunkY;
        }
    }
    struct TilePoint
    {
        public int tileX { get; }   // �б� ����. �ش� ûũ �� Ÿ���� X�� ��ȣ 
        public int tileY { get; }   // �б� ����. �ش� ûũ �� Ÿ���� Y�� ��ȣ
        public int objectNum { get; }   // �б�����. ������Ʈ ������ �з��ϴ� ��ȣ
        public ulong objectCode { get; }    // �б�����. ������Ʈ �ĺ� �ڵ�
        public bool isRoot { get; } // �б�����. ������Ʈ�� �߽��� �Ǵ� ��ġ���� ����
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
    // ûũ ��ǥ�� ���� ������Ʈ ��ǥ ����Ʈ. ûũ ��ǥ �� Ÿ�� ��ǥ�� ���� � ������Ʈ�� �ö���ִ��� �����ϱ� ����.
    private  Dictionary<ChunkPoint,List <TilePoint>> objectPointList = new Dictionary<ChunkPoint, List<TilePoint>>();
    // ûũ ��ǥ�� ���� ���ӿ�����Ʈ ����Ʈ. ī�޶� ���� �� ûũ ��ǥ�� �ִ� ���� ������Ʈ�� �������� ���� ����.
    private Dictionary<ChunkPoint, List<GameObject>> gameObjectChunkPointList = new Dictionary<ChunkPoint, List<GameObject>>();
    // ������ ������Ʈ�� �ٿ��ֱ� ���� �ĺ��ڵ�. ���� �ϳ� ������Ʈ ���� �ʰ� ���� 0�� ���� ���������� �����ϱ� ���� ulong���� ���. 
    private ulong objCode = 0;

	public void Awake()
	{
        // �̺�Ʈ �帮��. ������ Call(ȣ��Ǿ���� Ŭ����)To(ȣ���ϴ�Ŭ����) �������� ������.
        CallSettingObjectToBuildingColiderEventDriven.getObjectCodeEvent += RemoveObject;
        RemoveEventDriven.isRemoveEvent += RemoveEvent;

    }
    // �ٸ� ������ �Ѿ�� �̺�Ʈ�� �߰��� �Լ��� ���� ���� �Ѿ�� ����.
    // �Ȳ��� �Ѿ�� �ٸ� ������ �Ѿ�� �߰��� �Լ��� ���������� ��ũ��Ʈ�� ��� ������Ʈ�� �����ǰ� �ٽ� �����Ǿ��� ������ ������ ��.
    // static �����̶� �׷���?
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
            // ûũ��ǥ�� ������ true ����
            return true;
        }
        // ������ false ����
        return false;
    }

    // tileX, tileY�� 0~19 ���� ���. Check~~Range�Լ����� 0~19 ���� ��򰡷� ������ �����־��� ����.
    // ��, �Լ�ȣ���� AddObjectPointList, Check~~Range �Ѵ� PerlinNoiseMapMaker���� �̷������ �ִٴ� ��.
    // ûũ ��ǥ�� ������Ʈ ������ �ֱ� ���� �Լ�.
    public void AddObjectPointList(int chunkX, int chunkY, int objectNum, int tileX, int tileY)
    {
        // �ش� ûũ ��ǥ ������ ��ųʸ� Ű�� �����ϰ� �ִ��� Ȯ��
        if (!objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
        {
            // Ű�� �����ϰ� ���� ������ Ű�� ����.
            List<TilePoint> tilePoint = new List<TilePoint>();
            objectPointList.Add(new ChunkPoint(chunkX, chunkY), tilePoint);
            // ûũ ��ǥ�� ������Ʈ ������ ����.
            AddTilePoint(chunkX, chunkY, objectNum, tileX, tileY);
        }
        else
        {
            // ûũ ��ǥ�� ������Ʈ ������ ����.
            AddTilePoint(chunkX, chunkY, objectNum, tileX, tileY);
        }
    }
    // ûũ ��ǥ�� �ִ� Ÿ�Ϻ��� ������Ʈ�� ������ ��� ���� �Լ�. 
    // �ʰ� ���ÿ� �ڿ� �����Ǿ�� �ϴ� ������Ʈ�� ���ؼ� �� �Լ��� ����Ѵ�.
    // chunkX, chunkY�� ûũ ��ǥ. objectNum�� �ǹ�Ÿ�� ��ȣ. tileX, tileY�� ûũ ��ǥ �� Ÿ���� ��ǥ ��ȣ.
    private void AddTilePoint(int chunkX, int chunkY, int objectNum, int tileX, int tileY)
    {
        // �����ؾߵ� ������Ʈ�� ���� ���� ����� ����� ����Ʈ���� index ���� ������.
        int index = settingObjInfo.objSize.FindIndex(a => a.objNum == objectNum);
        // ������ ������Ʈ �߾��� �������� ��� Ÿ�Ͽ� ������Ʈ�� ��� �Ǵ��� ���� ���
        int minX = tileX - (int)((float)settingObjInfo.objSize[index].sizeX / 2f) + ((settingObjInfo.objSize[index].sizeX + 1) % 2);
        int maxX = tileX + (int)((float)settingObjInfo.objSize[index].sizeX / 2f);
        int minY = tileY - (int)((float)settingObjInfo.objSize[index].sizeY / 2f) + ((settingObjInfo.objSize[index].sizeY + 1) % 2);
        int maxY = tileY + (int)((float)settingObjInfo.objSize[index].sizeY / 2f);

        // �ٸ� ��ǥ�� ���� ������Ʈ�� �����ϰ� �ִ� ������Ʈ ��ȣ. ulongŸ��
        objCode++;
        // ���� �� ��� �ִ� Ÿ���� ��ǥ�� ������Ʈ ��ȣ�� �Բ� ����
        // chunkX, chunkY ���� �״�� ������ ������ ������Ʈ�� ��� �ִ� Ÿ���� ������ 0~19 ���̸� �Ѿ�� �ʰ� �߱� ����
        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                if (tileX == i && tileY == j)
                {
                    //�ǹ��� ������ �Ǵ� ��ǥ(�ǹ��� ���콺Ŀ���� �´�� �ִ� ��ǥ)�� Ư���� TilePoint�� isRoot�� true�� ���ش�.
                    // isRoot�� true�� ��ǥ�� ������Ʈ�� ������Ű�� �����̴�.
                    //ûũ ��ǥ Ű�� ���� TilePoint����Ʈ�� TilePoint ����ü ������ �����Ѵ�.
                    objectPointList[new ChunkPoint(chunkX, chunkY)].Add(new TilePoint(i, j, objectNum, objCode, true));
                }
                else
                {
                    //ûũ ��ǥ Ű�� ���� TilePoint����Ʈ��  TilePoint ����ü ������ �����Ѵ�.
                    //�ǹ��� ������ �Ǵ� ��ǥ�� �ƴ϶�� TilePoint�� isRoot�� false�� ���ش�.
                    objectPointList[new ChunkPoint(chunkX, chunkY)].Add(new TilePoint(i, j, objectNum, objCode, false));
                }
            }
        }
    }
    // ûũ ��ǥ�� �ִ� Ÿ�� ���� ������Ʈ�� ������ ��� ���� �Լ�.
    // ADdTilePoint�Լ��� �ٸ� ���� �÷��̾ ���� ������ ������Ʈ�� ��� ���� �Լ���� ��.
    // ���� ������ ������Ʈ�� ������ �� ûũ, ������ �� ûũ ��ǥ�� �´�� ���� ��찡 �ֱ� ������
    // ���ڰ��� ������Ʈ�� ������ �Ǵ� ��ǥ pointX, pointZ���θ� �ް� ûũ ��ǥ�� ûũ �� Ÿ�� ��ǥ�� for�� ������ ��� �ٽ� ������ش�.
    // objectNum�� �ǹ�Ÿ�� ��ȣ, obj�� ������ ���ӿ�����Ʈ�̴�.
    public void AddTilePoint2(int pointX, int pointZ, int objectNum, GameObject obj)
    {
        // ������ ������Ʈ �߾��� �������� ��� Ÿ�Ͽ� ������Ʈ�� ��� �Ǵ��� ���� ���
        int minX = pointX - (int)((float)settingObjInfo.objSize[objectNum].sizeX / 2f) + ((settingObjInfo.objSize[objectNum].sizeX + 1) % 2);
        int maxX = pointX + (int)((float)settingObjInfo.objSize[objectNum].sizeX / 2f);
        int minY = pointZ - (int)((float)settingObjInfo.objSize[objectNum].sizeY / 2f) + ((settingObjInfo.objSize[objectNum].sizeY + 1) % 2);
        int maxY = pointZ + (int)((float)settingObjInfo.objSize[objectNum].sizeY / 2f);
        // �ٸ� ��ǥ�� ���� ������Ʈ�� �����ϰ� �ִ� ������Ʈ ��ȣ. ulongŸ��
        objCode++;
        // obj�� ��� BuildingColiderŬ���� �� objCode�� �����Ѵ�.
        if (obj.GetComponent<BuildingColider>() != null)
		{
            obj.GetComponent<BuildingColider>().objCode = objCode;
		}
        // ���� �� ��� �ִ� Ÿ���� ��ǥ�� ������Ʈ ��ȣ�� �Բ� �����Ѵ�.
        // �߰��� ûũ ��ǥ�� ûũ ��ǥ �� Ÿ�� ��ǥ�� �ش� for�� ������ ����Ѵ�.
        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                // ���ʷ� ûũ ��ǥX,Y, Ÿ����ǥ X,Y
                int chunkX, chunkY, tileX, tileY;
                // ������ 0���� ũ�� ûũ X��ǥ�� ������ 20. �������̸� ������ 20 ���� -1�� ���ش�.
                // ������ -15, 15 �Ѵ� ������ 20�� �ϸ� 0�� ������ ������ ûũ��ǥ ��꿡 ������ ����� �����̴�. 
                if (i >= 0) { chunkX = i / 20; }
                else { chunkX = (i / 20) - 1; }
                // ������ 0���� ũ�� ûũ Y��ǥ�� ������ 20. �������̸� ������ 20���� -1�� ���ش�.
                if (j >= 0) { chunkY = j / 20; }
                else { chunkY = (j / 20) - 1; }
                // ������ 0���� ũ�� Ÿ�� X��ǥ�� ������ 20�� ������.
                if (i >= 0) { tileX = i % 20; }
                else 
                {
                    // ������ ������ ��, ������ 20�� �������� 0�̸� ûũ X��ǥ�� ��ĭ �����. �� �ܿ� Ÿ����ǥ�� 20�� �߰��Ѵ�.
                    if (i % 20 == 0) { tileX = 0; chunkX++; }
                    else { tileX = (i % 20) + 20; } 
                }
               // ������ 0���� ũ�� Ÿ�� Y��ǥ�� ������ 20�� ������.
                if (j >= 0) { tileY = j % 20; }
                else 
                {
                    // ������ ������ ��, ������ 20�� �������� 0�̸� ûũ Y��ǥ�� ��ĭ �����. �� �ܿ� Ÿ����ǥ�� 20�� �߰��Ѵ�.
                    if (j % 20 == 0) { tileY = 0; chunkY++; }
                    else { tileY = (j % 20) + 20; } 
                }
                
                // objectPointList�� ûũ ��ǥ Ű�� ������ Ű�� �߰��Ѵ�.
                if (!objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
                {
                    List<TilePoint> tilePoint = new List<TilePoint>();
                    objectPointList.Add(new ChunkPoint(chunkX, chunkY), tilePoint);
                }

                // �ǹ��� ������ �Ǵ� ��ǥ�� objectPointList�� �����ϴ� �� �ܿ���
                // gameObjectChunkPointList�� �� ��ǥ�� �ִ� ûũ ��ǥ ���� Ű�� �Ͽ� ���� ������Ʈ�� �߰����ش�. 
                if (i == pointX && j == pointZ)
				{
                    //ûũ ��ǥ Ű�� ���� TilePoint����Ʈ�� TilePoint ����ü ������ �����Ѵ�.
                    objectPointList[new ChunkPoint(chunkX, chunkY)].Add(new TilePoint(tileX, tileY, obj.GetComponent<BuildingColider>().GetObjTypeNum(), objCode, true));
                
                    // �ش� ûũ ��ǥ Ű�� ������ �߰����ְ�, �� ûũ ��ǥ Ű�� ���� GameObject����Ʈ�� GameObject�� �־��ش�.
                    if (!gameObjectChunkPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
					{
                        List<GameObject> objList = new List<GameObject>();
                        gameObjectChunkPointList.Add(new ChunkPoint(chunkX,chunkY), objList);
                        //ûũ ��ǥ Ű�� ���� GameObject����Ʈ�� GameObject�� �־��ش�.
                        gameObjectChunkPointList[new ChunkPoint(chunkX, chunkY)].Add(obj);
                    }
                    else
					{
                        //ûũ ��ǥ Ű�� ���� GameObject����Ʈ�� GameObject�� �־��ش�.
                        gameObjectChunkPointList[new ChunkPoint(chunkX, chunkY)].Add(obj);
                    }
                }
                else
				{
                    //ûũ ��ǥ Ű�� ���� TilePoint����Ʈ�� TilePoint ����ü ������ �����Ѵ�.
                    objectPointList[new ChunkPoint(chunkX, chunkY)].Add(new TilePoint(tileX, tileY, obj.GetComponent<BuildingColider>().GetObjTypeNum(), objCode, false));
                }
                //Debug.Log("chunkX : " + chunkX + " chunkY : " + chunkY + " tileX : " + tileX + " tileY : " + tileY + " objectNum : " + objectNum + "  i : " + i + "  j : " + j + " pointX : " + pointX + "pointZ : " + pointZ);
            }
        }


    }


    // ��ǥ�� �°� ������Ʈ�� �����Ѵ�. �����ϴ� ������Ʈ�� �� ������ �ڿ� �����Ǵ� ������Ʈ�� ���ؼ��̴�.
    // ���ڰ� chunkX, chunkY�� �ش� ������Ʈ�� �����Ǵ� ûũ ���̴�.
    public void CreateObejct(int chunkX, int chunkY)
    {
        // �ش� ûũ ��ǥ ������ ��ųʸ��� Ű�� �����ϰ� �ִ��� Ȯ��. ������ ����
        if (!ActiveTrueObjectPointList(chunkX, chunkY)) { return; }
        // ���ڰ��� ���� ChunkPoint �� ������ �ϳ� �����Ѵ�.
        ChunkPoint chunk = new ChunkPoint(chunkX, chunkY);
        for (int i = 0; i < objectPointList[chunk].Count; i++)
        {
            if (objectPointList[chunk][i].isRoot)
            {// ������Ʈ ������ �θ� ������Ʈ ����
                GameObject obj = Instantiate(objectArr[objectPointList[chunk][i].objectNum], motehrObject.transform);
                // ������ ������Ʈ�� ��ǥ
                obj.transform.position = new Vector3
                    ( chunkX * 20 + objectPointList[chunk][i].tileX, 0, chunkY * 20 + objectPointList[chunk][i].tileY);
                // ȭ�鿡 ���̰� ���� ����
                obj.transform.eulerAngles = new Vector3(90, 0, 0);
                // �ش� ûũ ��ǥ Ű�� �ִ��� Ȯ��
                if (!gameObjectChunkPointList.ContainsKey(chunk))
                {
                    // Ű�� ������ Ű ���� �� �� ûũ ��ǥ�� ���� GameObject ����Ʈ�� GameObject�� �߰��Ѵ�. 
                    List<GameObject> objList = new List<GameObject>();
                    gameObjectChunkPointList.Add(chunk, objList);
                    gameObjectChunkPointList[chunk].Add(obj);
                }
                else
                {
                    // ûũ ��ǥ�� ���� GameObject ����Ʈ�� GameObject�� �߰��Ѵ�.
                    gameObjectChunkPointList[chunk].Add(obj);
                }
            }
        }
    }

    // ������Ʈ�� �������ش�. �����ϴ� ������Ʈ�� �÷��̾ ���� ������ ������Ʈ�� ���Ѵ�.
    // �ش� �Լ��� �̺�Ʈ �帮���� ����Ͽ� BuildingColider Ŭ�������� ȣ��ȴ�.
    public void RemoveObject(ulong objCode, GameObject obj)
	{
        // ûũ�� ��ǥ�� ������ ���ߴ�. ����Ȯ�� Ȯ���� �ſ� ����.
        int inaccurateChunkX = (int)obj.transform.position.x / 20;
        int inaccurateChunkY = (int)obj.transform.position.z / 20;
        // ����Ȯ�ϰ� ���� ûũ��ǥ�̹Ƿ� �ش� ûũ��ǥ�� �������� �����¿�� -2, +2���� ������ ������ ������Ʈ�� ã�´�.
        for (int x = inaccurateChunkX - 2; x < inaccurateChunkX + 2; x++)
		{
            for (int y = inaccurateChunkY - 2; y < inaccurateChunkY + 2; y++)
			{
                // ���� gameObjectChunkPointList���� ���ӿ�����Ʈ�� �����Ѵ�.
                if (gameObjectChunkPointList.ContainsKey(new ChunkPoint(x, y)))
				{
                    // ûũ ��ǥ ������ ������Ʈ ������ ã�´�.
                    int index = gameObjectChunkPointList[new ChunkPoint(x, y)].FindIndex(a => a.Equals(obj));
                    // ������Ʈ ������ ã���� �� ���� ������Ʈ�� �������ش�.
                    if (index != -1)
					{
                        // �ε��� ��ȣ�� �����ش�.
                        gameObjectChunkPointList[new ChunkPoint(x, y)].RemoveAt(index);
					}
				}
                // �� �������� ojbectPointList���� TilePoint ��ǥ ������ �����ش�. �پ��� ûũ ������ �������� ������ ������� �� �ִ�.
                // ������ ����Ȯ�� ûũ ��ǥ�� �������� �����¿�� -2, +2 ���� ������ �����ϹǷ� ���� ����.
                if (objectPointList.ContainsKey(new ChunkPoint(x, y)))
				{
                    // list�� ûũ��ǥ �� �ʿ��� TilePoint ������ ��� �޾ƿ´�.
                    List<TilePoint> list = objectPointList[new ChunkPoint(x, y)].FindAll(a => a.objectCode == objCode);

                    // ������ �������� ûũ ��ǥ �� List<TilePoint>���� ���� �����ش�.
                    for (int i = 0; i < list.Count; i++)
					{
                        // TilePoint�������� �����ش�.
                        objectPointList[new ChunkPoint(x, y)].Remove(list[i]);
					}
                    // Ȥ�ø𸣴� list�� clear~
                    list.Clear();
				}
			}
		}
	}

    // ������ ���� ���� ������ ������ ������ �����ϴ��� ���θ� �Ǵ�.
    // ���ڰ� chunkX, chnukY�� ûũ ��ǥ. chunk�� �ش� ���ӿ�����Ʈ�� �ڽ� ������Ʈ�� tile ������Ʈ�� ã�� ����.
    // x,y�� ûũ ��ǥ �� Ÿ�� ��ǥ, tile�� ���� Ÿ��, chunkSize�� ûũ �ϳ��� ���μ��� ������
    public bool CheckMineRange(int chunkX, int chunkY, GameObject chunk, int x, int y, Sprite tile, int chunkSize)
    {
        // ������ ������ ���� 3(*16�ȼ�),���� 3(*16�ȼ�)��. ���� �����¿�� ��ĭ ������ ûũ ������ ������ Ȯ���ؾߵ�
        if (x - 1 <= 0 || x + 1 >= chunkSize || y - 1 <= 0 || y + 1 >= chunkSize) { return false; }
        // ���� Ȯ�� ��, �ش� ��ǥ �� �̹� ������Ʈ�� �����Ǿ� �ִ���, Ȥ�� ������ �Ұ������ϴ� Ÿ���� �ִ��� Ȯ����
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                // ûũ��ǥ Ű�� �ִ��� Ȯ��
                if (objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
                {
                    // ûũ ��ǥ �� List<TilePoint>���� Ÿ�� ��ǥ�� �̹� ����Ǿ� �ִ��� Ȯ��.
                    int index = objectPointList[new ChunkPoint(chunkX, chunkY)].FindIndex(a => a.tileX == i && a.tileY == j);
                    // �̹� ����Ǿ� ������ ������ ������ �� ������ (false) ��ȯ
                    if (index != -1)
                    {
                        return false;
                    }
                }
                // ûũ ������Ʈ �� �ڽ� ������Ʈ�� Ÿ�� ������Ʈ�� ��������Ʈ�� tile�̶�� ������ ������ �� ����(false)�� ��ȯ
                if (chunk.transform.GetChild(i * chunkSize + j).GetComponent<SpriteRenderer>().sprite == tile)
                {
                    return false;
                }
            }
        }
        // ���ܻ�Ȳ�� ���ٸ� ������ ������ �� ����(true)�� ��ȯ
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
            //ûũ��ǥ Ű�� �ִ��� Ȯ��
            if (objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
            {
                // ûũ ��ǥ �� List<TilePoint>���� Ÿ�� ��ǥ�� �̹� ����Ǿ� �ִ��� Ȯ��.
                int index = objectPointList[new ChunkPoint(chunkX, chunkY)].FindIndex(a => a.tileX == x && a.tileY == i);
                // �̹� ����Ǿ� ������ ������ ������ �� ������ (false) ��ȯ
                if (index != -1)
                {
                    return false;
                }
            }
            // ûũ ������Ʈ �� �ڽ� ������Ʈ�� Ÿ�� ������Ʈ�� ��������Ʈ�� tile�̶�� ������ ������ �� ����(false)�� ��ȯ 
            if (chunk.transform.GetChild(x * chunkSize + i).GetComponent<SpriteRenderer>().sprite == tile)
            {
                return false;
            }
        }
        // ���ܻ�Ȳ�� ���ٸ� ������ ������ �� ����(true)�� ��ȯ.
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

    // �ش� ��ǥ�� ������ ������ �� �ִ����� Ȯ���ϴ� �Լ��̴�.
    public bool CheckPossibleSettingBuilding(int objTypeNum, int chunkX, int chunkY, int tileX, int tileY)
	{
        // ûũ ��ǥ Ű�� ���� ���
        if (objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
		{
            // Ÿ�� ��ǥ�� ���� TilePoint ����Ʈ�� �ε��� ���� �����´�. 
            int index = objectPointList[new ChunkPoint(chunkX, chunkY)].FindIndex(a => a.tileX == tileX && a.tileY == tileY);
            // �ǹ� Ÿ�� ��ȣ�� ���� �����Ϸ��� �ǹ��� ������ ��� ����Ʈ�� �ε��� ���� �����´�.
            int index2 = settingObjInfo.objSize.FindIndex(a => a.objNum == objTypeNum);
            // ������ �ε��� ���� ������ �����ϴ� ���
            if (index != -1)
			{
                // ������ ������ �� ����(false)�� ��ȯ�Ѵ�.
                return false;
			}
            else if (index2 != 1)
			{
                // �ش� ��ǥ�� ������ ������ �� �մ��� Ȯ���Ѵ�.
                if (this.GetComponent<PerlinNoiseMapMaker>().CheckPossibleSettingBuilding(
                    settingObjInfo.objSize[index2].possibleTileArr, chunkX, chunkY, tileX, tileY))
				{
                    // ������ ������ �� ����(true)�� ��ȯ�Ѵ�.
                    return true;
				}
			}
		}

        return false;
	}
}
