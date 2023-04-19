using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectNS;

public class SettingObject : MonoBehaviour
{
    struct ChunkPoint
    {
        public int chunkX { get; }  // 읽기 전용. 생성자에서만 값을 받을 수 있음.
        public int chunkY { get; }  // 읽기 전용. 생성자에서만 값을 받을 수 있음.

        // 생성자
        public ChunkPoint(int chunkX , int chunkY)
        {
            this.chunkX = chunkX;
            this.chunkY = chunkY;
        }
    }
    struct TilePoint
    {
        public int tileX { get; }   // 읽기 전용. 해당 청크 내 타일의 X축 번호 
        public int tileY { get; }   // 읽기 전용. 해당 청크 내 타일의 Y축 번호
        public int objectNum { get; }   // 읽기전용. 오브젝트 종류를 분류하는 번호
        public ulong objectCode { get; }    // 읽기전용. 오브젝트 식별 코드
        public bool isRoot { get; } // 읽기전용. 오브젝트의 중심이 되는 위치인지 여부
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
    [SerializeField] private GameObject[] snowObjectArr;
    [SerializeField] private GameObject motehrObject;
    [SerializeField] private GameObject motherBuildingObject;
    [SerializeField] private SettingObjectInfo settingObjInfo;
    private CallBuildingAttachMouseToSettingObjectEventDriven callBuildingAttachMouseToSettingObjectEventDriven = new CallBuildingAttachMouseToSettingObjectEventDriven();
    // 청크 좌표에 따른 오브젝트 좌표 리스트. 청크 좌표 별 타일 좌표에 현재 어떤 오브젝트가 올라와있는지 저장하기 위함.
    private  Dictionary<ChunkPoint,List <TilePoint>> objectPointList = new Dictionary<ChunkPoint, List<TilePoint>>();
    // 청크 좌표에 따른 게임오브젝트 리스트. 카메라 범위 밖 청크 좌표에 있는 게임 오브젝트의 렌더러를 끄기 위함.
    private Dictionary<ChunkPoint, List<GameObject>> gameObjectChunkPointList = new Dictionary<ChunkPoint, List<GameObject>>();
    // 생성된 오브젝트에 붙여주기 위한 식별코드. 만에 하나 오브젝트 개수 초과 대비와 0과 양의 정수값만을 저장하기 위해 ulong형을 사용. 
    private ulong objCode = 0;
    private bool isSnow;
	public void Awake()
	{
        // 이벤트 드리븐. 제목은 Call(호출되어야할 클래스)To(호출하는클래스) 형식으로 지었음.
        CallSettingObjectToBuildingColiderEventDriven.getObjectCodeEvent += RemoveObject;
        CallSettingObjectToNatureObjectEventDriven.removeObjectInfoToTileEvent += RemoveObject;
        RemoveEventDriven.isRemoveEvent += RemoveEvent;
        isSnow = false;
    }
    // 다른 씬으로 넘어갈때 이벤트에 추가한 함수를 전부 끄고 넘어가기 위함.
    // 안끄고 넘어가면 다른 씬으로 넘어가도 추가한 함수가 남아있으나 스크립트가 담긴 오브젝트는 삭제되고 다시 생성되었기 때문에 에러가 남.
    // static 형식이라 그런듯?
    private void RemoveEvent()
	{
        CallSettingObjectToBuildingColiderEventDriven.getObjectCodeEvent -= RemoveObject;
        CallSettingObjectToNatureObjectEventDriven.removeObjectInfoToTileEvent -= RemoveObject;
        RemoveEventDriven.isRemoveEvent -= RemoveEvent;
    }

    public void ChangeSnowBuildingSprite(bool bo)
	{
        isSnow = bo;
	}

    // 해당 청크 좌표 정보를 딕셔너리에 키로 저장하고 있는지 확인. 있으면 true 리턴 없으면 false 리턴
    public bool ActiveTrueObjectPointList(int chunkX, int chunkY)
    {
        if (objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
        {
            // 청크좌표가 있으면 true 리턴
            return true;
        }
        // 없으면 false 리턴
        return false;
    }

    // tileX, tileY는 0~19 사이 어딘가. Check~~Range함수에서 0~19 사이 어딘가로 범위를 정해주었기 때문.
    // 단, 함수호출은 AddObjectPointList, Check~~Range 둘다 PerlinNoiseMapMaker에서 이루어지고 있다는 점.
    // 청크 좌표에 오브젝트 정보를 넣기 위한 함수.
    public void AddObjectPointList(int chunkX, int chunkY, int objectNum, int tileX, int tileY)
    {
        // 해당 청크 좌표 정보를 딕셔너리 키로 저장하고 있는지 확인
        if (!objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
        {
            // 키로 저장하고 있지 않으면 키로 저장.
            List<TilePoint> tilePoint = new List<TilePoint>();
            objectPointList.Add(new ChunkPoint(chunkX, chunkY), tilePoint);
            // 청크 좌표에 오브젝트 정보를 담음.
            AddTilePoint(chunkX, chunkY, objectNum, tileX, tileY);
        }
        else
        {
            // 청크 좌표에 오브젝트 정보를 담음.
            AddTilePoint(chunkX, chunkY, objectNum, tileX, tileY);
        }
    }
    // 청크 좌표에 있는 타일별로 오브젝트의 정보를 담기 위한 함수. 
    // 맵과 동시에 자연 생성되어야 하는 오브젝트에 한해서 이 함수를 사용한다.
    // chunkX, chunkY는 청크 좌표. objectNum은 건물타입 번호. tileX, tileY는 청크 좌표 내 타일의 좌표 번호.
    private void AddTilePoint(int chunkX, int chunkY, int objectNum, int tileX, int tileY)
    {
        // 저장해야될 오브젝트의 가로 세로 사이즈가 저장된 리스트에서 index 값을 가져옴.
        int index = settingObjInfo.objSize.FindIndex(a => a.objNum == objectNum);
        // 생성될 오브젝트 중앙을 기준으로 어느 타일에 오브젝트가 닿게 되는지 범위 계산
        int minX = tileX - (int)((float)settingObjInfo.objSize[index].sizeX / 2f) + ((settingObjInfo.objSize[index].sizeX + 1) % 2);
        int maxX = tileX + (int)((float)settingObjInfo.objSize[index].sizeX / 2f);
        int minY = tileY - (int)((float)settingObjInfo.objSize[index].sizeY / 2f) + ((settingObjInfo.objSize[index].sizeY + 1) % 2);
        int maxY = tileY + (int)((float)settingObjInfo.objSize[index].sizeY / 2f);

        // 다른 좌표에 같은 오브젝트가 공유하고 있는 오브젝트 번호. ulong타입
        objCode++;
        // 범위 내 닿고 있는 타일의 좌표를 오브젝트 번호와 함께 저장
        // chunkX, chunkY 값을 그대로 가져온 이유는 오브젝트가 닿고 있는 타일의 범위가 0~19 사이를 넘어가지 않게 했기 때문
        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                if (tileX == i && tileY == j)
                {
                    //건물의 기준이 되는 좌표(건물과 마우스커서가 맞닿고 있는 좌표)는 특별히 TilePoint의 isRoot를 true로 해준다.
                    // isRoot가 true인 좌표에 오브젝트를 생성시키기 위함이다.
                    //청크 좌표 키를 통해 TilePoint리스트에 TilePoint 구조체 형식을 저장한다.
                    objectPointList[new ChunkPoint(chunkX, chunkY)].Add(new TilePoint(i, j, objectNum, objCode, true));
                }
                else
                {
                    //청크 좌표 키를 통해 TilePoint리스트에  TilePoint 구조체 형식을 저장한다.
                    //건물의 기준이 되는 좌표가 아니라면 TilePoint의 isRoot를 false로 해준다.
                    objectPointList[new ChunkPoint(chunkX, chunkY)].Add(new TilePoint(i, j, objectNum, objCode, false));
                }
            }
        }
    }
    private bool bo1;
    // 청크 좌표에 있는 타일 별로 오브젝트의 정보를 담기 위한 함수.
    // ADdTilePoint함수와 다른 점은 플레이어가 직접 생성한 오브젝트를 담기 위한 함수라는 것.
    // 따라서 생성한 오브젝트의 범위가 두 청크, 많으면 네 청크 좌표와 맞닿아 있을 경우가 있기 때문에
    // 인자값을 오브젝트의 기준이 되는 좌표 pointX, pointZ으로만 받고 청크 좌표와 청크 내 타일 좌표는 for문 내에서 계속 다시 계산해준다.
    // objectNum은 건물타입 번호, obj는 생성할 게임오브젝트이다.
    public void AddTilePoint2(int pointX, int pointZ, int objectNum, GameObject obj)
    {
        //if (!bo1)
        //{
        //    GameManager.instance.InitializeGrid(500, 500);
        //    bo1 = true;
        //}

        // 생성될 오브젝트 중앙을 기준으로 어느 타일에 오브젝트에 닿게 되는지 범위 계산
        int minX = pointX - (int)((float)settingObjInfo.objSize[objectNum].sizeX / 2f) + ((settingObjInfo.objSize[objectNum].sizeX + 1) % 2);
        int maxX = pointX + (int)((float)settingObjInfo.objSize[objectNum].sizeX / 2f);
        int minY = pointZ - (int)((float)settingObjInfo.objSize[objectNum].sizeY / 2f) + ((settingObjInfo.objSize[objectNum].sizeY + 1) % 2);
        int maxY = pointZ + (int)((float)settingObjInfo.objSize[objectNum].sizeY / 2f);
        // 다른 좌표에 같은 오브젝트가 공유하고 있는 오브젝트 번호. ulong타입
        objCode++;
        // obj에 담긴 BuildingColider클래스 내 objCode를 갱신한다.
        if (obj.GetComponent<BuildingColider>() != null)
		{
            obj.GetComponent<BuildingColider>().objCode = objCode;
		}

        if (obj.GetComponent<ChangeSnowBuildingSprite>() != null)
        {
            if (isSnow)
            {
                obj.GetComponent<ChangeSnowBuildingSprite>().ChangeSprite(1);
            }
            else
            {
                obj.GetComponent<ChangeSnowBuildingSprite>().ChangeSprite(0);
            }
        }
        // 범위 내 닿고 있는 타일의 좌표를 오브젝트 번호와 함께 저장한다.
        // 추가로 청크 좌표와 청크 좌표 내 타일 좌표도 해당 for문 내에서 계산한다.
        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                // 차례로 청크 좌표X,Y, 타일좌표 X,Y
                int chunkX, chunkY, tileX, tileY;
                // 범위가 0보다 크면 청크 X좌표는 나누기 20. 음수값이면 나누기 20 값에 -1을 해준다.
                // 예컨대 -15, 15 둘다 나누기 20을 하면 0이 나오기 때문에 청크좌표 계산에 오차가 생기기 때문이다. 
                if (i >= 0) { chunkX = i / 20; }
                else { chunkX = (i / 20) - 1; }
                // 범위가 0보다 크면 청크 Y좌표는 나누기 20. 음수값이면 나누기 20값에 -1을 해준다.
                if (j >= 0) { chunkY = j / 20; }
                else { chunkY = (j / 20) - 1; }
                // 범위가 0보다 크면 타일 X좌표는 나누기 20의 나머지.
                if (i >= 0) { tileX = i % 20; }
                else 
                {
                    // 범위가 음수일 시, 나누기 20의 나머지가 0이면 청크 X좌표를 한칸 땡긴다. 그 외엔 타일좌표에 20을 추가한다.
                    if (i % 20 == 0) { tileX = 0; chunkX++; }
                    else { tileX = (i % 20) + 20; } 
                }
               // 범위가 0보다 크면 타일 Y좌표는 나누기 20의 나머지.
                if (j >= 0) { tileY = j % 20; }
                else 
                {
                    // 범위가 음수일 시, 나누기 20의 나머지가 0이면 청크 Y좌표를 한칸 때긴다. 그 외엔 타일좌표에 20을 추가한다.
                    if (j % 20 == 0) { tileY = 0; chunkY++; }
                    else { tileY = (j % 20) + 20; } 
                }
                
                // objectPointList에 청크 좌표 키가 없으면 키를 추가한다.
                if (!objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
                {
                    List<TilePoint> tilePoint = new List<TilePoint>();
                    objectPointList.Add(new ChunkPoint(chunkX, chunkY), tilePoint);
                }

                // 건물의 기준이 되는 좌표는 objectPointList에 저장하는 것 외에도
                // gameObjectChunkPointList에 그 좌표가 있는 청크 좌표 값을 키로 하여 게임 오브젝트를 추가해준다. 
                if (i == pointX && j == pointZ)
				{
                    //청크 좌표 키를 통해 TilePoint리스트에 TilePoint 구조체 형식을 저장한다.
                    objectPointList[new ChunkPoint(chunkX, chunkY)].Add(new TilePoint(tileX, tileY, obj.GetComponent<BuildingColider>().GetObjTypeNum(), objCode, true));
                
                    // 해당 청크 좌표 키가 없으면 추가해주고, 그 청크 좌표 키를 통해 GameObject리스트에 GameObject를 넣어준다.
                    if (!gameObjectChunkPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
					{
                        List<GameObject> objList = new List<GameObject>();
                        gameObjectChunkPointList.Add(new ChunkPoint(chunkX,chunkY), objList);
                        //청크 좌표 키를 통해 GameObject리스트에 GameObject를 넣어준다.
                        gameObjectChunkPointList[new ChunkPoint(chunkX, chunkY)].Add(obj);
                    }
                    else
					{
                        //청크 좌표 키를 통해 GameObject리스트에 GameObject를 넣어준다.
                        gameObjectChunkPointList[new ChunkPoint(chunkX, chunkY)].Add(obj);
                    }
                }
                else
				{
                    //청크 좌표 키를 통해 TilePoint리스트에 TilePoint 구조체 형식을 저장한다.
                    objectPointList[new ChunkPoint(chunkX, chunkY)].Add(new TilePoint(tileX, tileY, obj.GetComponent<BuildingColider>().GetObjTypeNum(), objCode, false));
                }
                //Debug.Log("chunkX : " + chunkX + " chunkY : " + chunkY + " tileX : " + tileX + " tileY : " + tileY + " objectNum : " + objectNum + "  i : " + i + "  j : " + j + " pointX : " + pointX + "pointZ : " + pointZ);
            }
        }

        if (obj.GetComponent<BuildingSetting>() != null)
        {
            obj.GetComponent<BuildingSetting>().AddNpcCount();
        }
    }
    public void AddTilePoint3(int chunkX, int chunkY, int objectNum, int tileX, int tileY)
	{
        callBuildingAttachMouseToSettingObjectEventDriven.RunSetObjectAndPointEvnet(chunkX, chunkY, objectArr[objectNum], tileX, tileY);

    }
    // 좌표에 맞게 오브젝트를 생성한다. 생성하는 오브젝트는 맵 생성시 자연 생성되는 오브젝트에 한해서이다.
    // 인자값 chunkX, chunkY는 해당 오브젝트가 생성되는 청크 값이다.
    public void CreateObejct(int chunkX, int chunkY)
    {
        // 해당 청크 좌표 정보를 딕셔너리에 키로 저장하고 있는지 확인. 없으면 리턴
        if (!ActiveTrueObjectPointList(chunkX, chunkY)) { return; }
        // 인자값을 토대로 ChunkPoint 형 변수를 하나 생성한다.
        ChunkPoint chunk = new ChunkPoint(chunkX, chunkY);
        for (int i = 0; i < objectPointList[chunk].Count; i++)
        {
            if (objectPointList[chunk][i].isRoot && (objectPointList[chunk][i].objectNum == 0 || objectPointList[chunk][i].objectNum == 1))
            {// 오브젝트 생성과 부모 오브젝트 설정
                GameObject obj = Instantiate(objectArr[objectPointList[chunk][i].objectNum], motehrObject.transform);
                if (obj.GetComponent<NatureObject>() != null)
                {
                    obj.GetComponent<NatureObject>().objCode = objectPointList[chunk][i].objectCode;
                }
                // 생성될 오브젝트의 좌표
                obj.transform.position = new Vector3
                    ( chunkX * 20 + objectPointList[chunk][i].tileX, 0, chunkY * 20 + objectPointList[chunk][i].tileY);
                // 화면에 보이게 각도 설정
                obj.transform.eulerAngles = new Vector3(90, 0, 0);
                if (isSnow)
                {
                    obj.GetComponent<ChangeSnowBuildingSprite>().ChangeSprite(1);
                }
                else
				{
                    obj.GetComponent<ChangeSnowBuildingSprite>().ChangeSprite(0);
				}
                // 해당 청크 좌표 키가 있는지 확인
                if (!gameObjectChunkPointList.ContainsKey(chunk))
                {
                    // 키가 없으면 키 생성 및 그 청크 좌표를 통해 GameObject 리스트에 GameObject를 추가한다. 
                    List<GameObject> objList = new List<GameObject>();
                    gameObjectChunkPointList.Add(chunk, objList);
                    gameObjectChunkPointList[chunk].Add(obj);
                }
                else
                {
                    // 청크 좌표를 통해 GameObject 리스트에 GameObject를 추가한다.
                    gameObjectChunkPointList[chunk].Add(obj);
                }
            }
        }
    }

    // 오브젝트를 삭제해준다. 삭제하는 오브젝트는 플레이어가 직접 생성한 오브젝트에 한한다.
    // 해당 함수는 이벤트 드리븐을 사용하여 BuildingColider 클래스에서 호출된다.
    public void RemoveObject(ulong objCode, GameObject obj)
	{
        // 청크와 좌표를 적당히 구했다. 부정확할 확률이 매우 높다.
        int inaccurateChunkX = (int)obj.transform.position.x / 20;
        int inaccurateChunkY = (int)obj.transform.position.z / 20;
        // 부정확하게 구한 청크좌표이므로 해당 청크좌표를 기준으로 상하좌우로 -2, +2범위 내에서 삭제할 오브젝트를 찾는다.
        for (int x = inaccurateChunkX - 2; x < inaccurateChunkX + 2; x++)
		{
            for (int y = inaccurateChunkY - 2; y < inaccurateChunkY + 2; y++)
			{
                // 먼저 gameObjectChunkPointList에서 게임오브젝트를 삭제한다.
                if (gameObjectChunkPointList.ContainsKey(new ChunkPoint(x, y)))
				{
                    // 청크 좌표 내에서 오브젝트 정보를 찾는다.
                    int index = gameObjectChunkPointList[new ChunkPoint(x, y)].FindIndex(a => a.Equals(obj));
                    // 오브젝트 정보를 찾았을 시 게임 오브젝트를 삭제해준다.
                    if (index != -1)
                    {
                        // 인덱스 번호로 지워준다.
                        gameObjectChunkPointList[new ChunkPoint(x, y)].RemoveAt(index);
					}
				}
                // 그 다음으로 ojbectPointList에서 TilePoint 좌표 정보를 지워준다. 다양한 청크 내에서 조각조각 정보가 담겨있을 수 있다.
                // 하지만 부정확한 청크 좌표를 기준으로 상하좌우로 -2, +2 범위 내에서 삭제하므로 문제 없다.
                if (objectPointList.ContainsKey(new ChunkPoint(x, y)))
                {
                    // list에 청크좌표 내 필요한 TilePoint 정보를 모두 받아온다.
                    List<TilePoint> list = objectPointList[new ChunkPoint(x, y)].FindAll(a => a.objectCode == objCode);
                    //Debug.Log(objCode);
                    // 저장한 정보들을 청크 좌표 내 List<TilePoint>에서 전부 지워준다.
                    for (int i = 0; i < list.Count; i++)
					{
                        // TilePoint형식으로 지워준다.
                        objectPointList[new ChunkPoint(x, y)].Remove(list[i]);
					}
                    // 혹시모르니 list는 clear~
                    list.Clear();
				}
			}
		}

        if (obj.GetComponent<BuildingSetting>() != null)
        {
            obj.GetComponent<BuildingSetting>().MinusNpcCount();

        }
	}
    public bool CheckStartBuildingRange(int chunkX, int chunkY, GameObject chunk, int x, int y, Sprite tile, int chunkSize)
	{
        if (x - 2 <= 0 || x + 2 >= chunkSize || y + 1 >= chunkSize) { return false; }
        for (int i = x - 2; i <= x + 2; i++)
		{
            for (int j = y; j <= y + 1; j++)
			{
                // 청크좌표 키가 있는지 확인
                if (objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
                {
                    // 청크 좌표 내 List<TilePoint>에서 타일 좌표가 이미 저장되어 있는지 확인.
                    int index = objectPointList[new ChunkPoint(chunkX, chunkY)].FindIndex(a => a.tileX == i && a.tileY == j);
                    // 이미 저장되어 있으면 시작건물을 생성할 수 없음을 (false) 반환
                    if (index != -1)
                    {
                        return false;
                    }
                }
                // 청크 오브젝트 내 자식 오브젝트인 타일 오브젝트의 스프라이트가 tile이라면 시작건물을 생성할 수 없음(false)을 반환
                if (chunk.transform.GetChild(i * chunkSize + j).GetComponent<SpriteRenderer>().sprite == tile)
                {
                    return false;
                }
            }
		}

        return true;
	}
    // 지정된 범위 내에 동굴을 생성할 조건을 만족하는지 여부를 판단.
    // 인자값 chunkX, chnukY는 청크 좌표. chunk는 해당 게임오브젝트의 자식 오브젝트인 tile 오브젝트를 찾기 위함.
    // x,y는 청크 좌표 내 타일 좌표, tile은 비교할 타일, chunkSize는 청크 하나의 가로세로 사이즈
    public bool CheckMineRange(int chunkX, int chunkY, GameObject chunk, int x, int y, Sprite tile, int chunkSize)
    {
        // 동굴의 범위는 가로 3(*16픽셀),세로 3(*16픽셀)임. 따라서 상하좌우로 한칸 범위를 청크 사이즈 내에서 확보해야됨
        if (x - 1 <= 0 || x + 1 >= chunkSize || y - 1 <= 0 || y + 1 >= chunkSize) { return false; }
        // 범위 확보 후, 해당 좌표 내 이미 오브젝트가 생성되어 있는지, 혹은 생성을 불가능케하는 타일이 있는지 확인함
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                // 청크좌표 키가 있는지 확인
                if (objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
                {
                    // 청크 좌표 내 List<TilePoint>에서 타일 좌표가 이미 저장되어 있는지 확인.
                    int index = objectPointList[new ChunkPoint(chunkX, chunkY)].FindIndex(a => a.tileX == i && a.tileY == j);
                    // 이미 저장되어 있으면 동굴을 생성할 수 없음을 (false) 반환
                    if (index != -1)
                    {
                        return false;
                    }
                }
                // 청크 오브젝트 내 자식 오브젝트인 타일 오브젝트의 스프라이트가 tile이라면 동굴을 생성할 수 없음(false)을 반환
                if (chunk.transform.GetChild(i * chunkSize + j).GetComponent<SpriteRenderer>().sprite == tile)
                {
                    return false;
                }
            }
        }
        // 예외상황이 없다면 동굴을 생성할 수 있음(true)을 반환
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
            //청크좌표 키가 있는지 확인
            if (objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
            {
                // 청크 좌표 내 List<TilePoint>에서 타일 좌표가 이미 저장되어 있는지 확인.
                int index = objectPointList[new ChunkPoint(chunkX, chunkY)].FindIndex(a => a.tileX == x && a.tileY == i);
                // 이미 저장되어 있으면 나무를 생성할 수 없음을 (false) 반환
                if (index != -1)
                {
                    return false;
                }
            }
            // 청크 오브젝트 내 자식 오브젝트인 타일 오브젝트의 스프라이트가 tile이라면 나무를 생성할 수 없음(false)을 반환 
            if (chunk.transform.GetChild(x * chunkSize + i).GetComponent<SpriteRenderer>().sprite == tile)
            {
                return false;
            }
        }
        // 예외상황이 없다면 나무를 생성할 수 있음(true)을 반환.
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

            if (isSnow)
			{
                gameObjectChunkPointList[new ChunkPoint(chunkX, chunkY)][i].GetComponent<ChangeSnowBuildingSprite>().ChangeSprite(1);
            }
            else
			{
                gameObjectChunkPointList[new ChunkPoint(chunkX, chunkY)][i].GetComponent<ChangeSnowBuildingSprite>().ChangeSprite(0);
            }
        }
    }

    // 해당 좌표에 빌딩을 생성할 수 있는지를 확인하는 함수이다.
    public bool CheckPossibleSettingBuilding(int objTypeNum, int chunkX, int chunkY, int tileX, int tileY)
	{
        // 청크 좌표 키가 있을 경우
        if (objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
		{
            // 타일 좌표를 통해 TilePoint 리스트의 인덱스 값을 가져온다. 
            int index = objectPointList[new ChunkPoint(chunkX, chunkY)].FindIndex(a => a.tileX == tileX && a.tileY == tileY);
            // 건물 타입 번호를 통해 생성하려는 건물의 정보가 담긴 리스트의 인덱스 값을 가져온다.
            int index2 = settingObjInfo.objSize.FindIndex(a => a.objNum == objTypeNum);
            // 가져온 인덱스 값이 실제로 존재하는 경우
            if (index != -1)
			{
                // 빌딩을 생성할 수 없음(false)을 반환한다.
                return false;
			}
            else if (index2 != 1)
			{
                // 해당 좌표에 빌딩을 생성할 수 잇는지 확인한다.
                if (this.GetComponent<PerlinNoiseMapMaker>().CheckPossibleSettingBuilding(
                    settingObjInfo.objSize[index2].possibleTileArr, chunkX, chunkY, tileX, tileY))
				{
                    // 빌딩을 생성할 수 있음(true)을 반환한다.
                    return true;
				}
			}
		}

        return false;
	}
}
