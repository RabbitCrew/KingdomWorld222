using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectNS;

public class SettingObject : MonoBehaviour
{


    struct ChunkPoint
    {
        public int chunkX { get; }  // 읽기 전용. 생성자에서만 값을 받을 수 있음.
        public int chunkY { get; }

        // 생성자
        public ChunkPoint(int chunkX , int chunkY)
        {
            this.chunkX = chunkX;
            this.chunkY = chunkY;
        }
    }
    struct TilePoint
    {
        public int tileX { get; }   // 해당 청크 내 타일의 X축 번호 
        public int tileY { get; }   // 해당 청크 내 타일의 Y축 번호
        public int objectNum { get; }   // 오브젝트 종류를 분류하는 번호
        public ulong objectCode { get; }    // 오브젝트 식별 코드
        public bool isRoot { get; } // 오브젝트의 중심이 되는 위치인지 여부
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
    // 청크 좌표에 따른 오브젝트 좌표 리스트
    private  Dictionary<ChunkPoint,List <TilePoint>> objectPointList = new Dictionary<ChunkPoint, List<TilePoint>>();
    // 청크 좌표에 따른 게임오브젝트 리스트
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

    // 해당 청크 좌표 정보를 딕셔너리에 키로 저장하고 있는지 확인. 있으면 true 리턴 없으면 false 리턴
    public bool ActiveTrueObjectPointList(int chunkX, int chunkY)
    {
        if (objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
        {
            return true;
        }
        return false;
    }

    // tileX, tileY는 0~19 사이 어딘가
    public void AddObjectPointList(int chunkX, int chunkY, int objectNum, int tileX, int tileY)
    {
        // 해당 청크 좌표 정보를 딕셔너리 키로 저장하고 있는지 확인
        if (!objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
        {
            // 키로 저장하고 있지 않으면 키로 저장.
            List<TilePoint> tilePoint = new List<TilePoint>();
            objectPointList.Add(new ChunkPoint(chunkX, chunkY), tilePoint);
            // 청크좌표에 오브젝트 정보를 담음.
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
    // 청크 좌표에 오브젝트의 정보를 담음. 
    private void AddTilePoint(int chunkX, int chunkY, int objectNum, int tileX, int tileY)
    {
        int index = settingObjInfo.objSize.FindIndex(a => a.objNum == objectNum);
        // 생성될 오브젝트 중앙을 기준으로 어느 타일에 오브젝트가 닿게 되는지 범위 계산
        int minX = tileX - (int)((float)settingObjInfo.objSize[index].sizeX / 2f) + ((settingObjInfo.objSize[index].sizeX + 1) % 2);
        int maxX = tileX + (int)((float)settingObjInfo.objSize[index].sizeX / 2f);
        int minY = tileY - (int)((float)settingObjInfo.objSize[index].sizeY / 2f) + ((settingObjInfo.objSize[index].sizeY + 1) % 2);
        int maxY = tileY + (int)((float)settingObjInfo.objSize[index].sizeY / 2f);

        // 다른 좌표에 같은 오브젝트가 공유하고 있는 오브젝트 번호. ulong타입
        objCode++;
        // 범위 내 닿고 있는 타일의 좌표를 오브젝트 번호와 함께 저장
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


    // 좌표에 맞게 오브젝트를 생성한다.
    public void CreateObejct(int chunkX, int chunkY)
    {
        // 해당 청크 좌표 정보를 딕셔너리에 키로 저장하고 있는지 확인. 없으면 리턴
        if (!ActiveTrueObjectPointList(chunkX, chunkY)) { return; }

        ChunkPoint chunk = new ChunkPoint(chunkX, chunkY);
        for (int i = 0; i < objectPointList[chunk].Count; i++)
        {
            if (objectPointList[chunk][i].isRoot)
            {// 오브젝트 생성과 부모 오브젝트 설정
                GameObject obj = Instantiate(objectArr[objectPointList[chunk][i].objectNum], motehrObject.transform);
                // 생성될 오브젝트의 좌표
                obj.transform.position = new Vector3
                    (
                    chunkX * 20 + objectPointList[chunk][i].tileX,
                    0,
                    chunkY * 20 + objectPointList[chunk][i].tileY
                    ); ;
                // 화면에 보이게 각도 설정
                obj.transform.eulerAngles = new Vector3(90, 0, 0);
                // 해당 청크 좌표 key가 있는지 확인
                if (!gameObjectChunkPointList.ContainsKey(chunk))
                {
                    // 키가 없으면 키 생성 및 그 청크 좌표 내 오브젝트를 value 값으로 추가
                    List<GameObject> objList = new List<GameObject>();
                    gameObjectChunkPointList.Add(chunk, objList);
                    gameObjectChunkPointList[chunk].Add(obj);
                }
                else
                {
                    // 청크 좌표 내 오브젝트를 value 값으로 추가
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

    // 지정된 범위 내에 동굴을 생성할 조건을 만족하는지 여부를 판단
    public bool CheckMineRange(int chunkX, int chunkY, GameObject chunk, int x, int y, Sprite tile, int chunkSize)
    {
        // 동굴의 범위는 가로 3(*16픽셀),세로 3(*16픽셀)임. 따라서 상하좌우로 한칸 범위를 청크 사이즈 내에서 확보해야됨
        if (x - 1 <= 0 || x + 1 >= chunkSize || y - 1 <= 0 || y + 1 >= chunkSize) { return false; }
        // 범위 확보 후, 해당 좌표 내 이미 오브젝트가 생성되어 있는지, 혹은 생성을 불가능케하는 타일이 있는지 확인함
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

    // 지정된 범위 내에 나무를 생성할 조건을 만족하는지 여부를 판단
    public bool CheckTreeRange(int chunkX, int chunkY, GameObject chunk, int x, int y, Sprite tile, int chunkSize)
    {
        // 나무의 범위는 가로 1(*16픽셀), 세로 2(*16)픽셀임. 위로 한칸 범위까지만 청크 사이즈 내에서 확보해야됨
        if (y + 1 >= chunkSize) { return false; }
        // 범위 확보 후, 해당 좌표 내 이미 오브젝트가 생성되어 있는지, 혹은 생성을 불가능케하는 타일이 있는지 확인함.
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
    // 활성화된 청크 위치 있는 오브젝트의 스프라이트 렌더러를 켜준다.
    public void EnableSpriteRenderer(int chunkX, int chunkY)
    {
        if (!gameObjectChunkPointList.ContainsKey(new ChunkPoint(chunkX, chunkY))) { return; }

        for (int i = 0; i < gameObjectChunkPointList[new ChunkPoint(chunkX, chunkY)].Count; i++)
        {
            gameObjectChunkPointList[new ChunkPoint(chunkX, chunkY)][i].GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    // 비활성화된 청크 위치 있는 오브젝트의 스프라이트 렌더러를 꺼준다.
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
        // 청크 좌표 키가 있을 경우
        if (objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
		{
            int index = objectPointList[new ChunkPoint(chunkX, chunkY)].FindIndex(a => a.tileX == tileX && a.tileY == tileY);
            int index2 = settingObjInfo.objSize.FindIndex(a => a.objNum == objTypeNum);
            // 청크 좌표 키를 통한 값 리스트에 타일에 대한 정보가 담겨있을 경우
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
