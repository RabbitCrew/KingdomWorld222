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
        public int tileX { get; }
        public int tileY { get; }
        public int objectNum { get; }
        public ulong objectCode { get; }
        public bool isRoot { get; }
        public TilePoint(int tileX, int tileY, int objectNum, ulong objectCode, bool isRoot)
        {
            this.tileX = tileX;
            this.tileY = tileY;
            this.objectNum = objectNum;
            this.objectCode = objectCode;
            this.isRoot = isRoot;
        }
    }

    [SerializeField]private GameObject[] objectArr;
    [SerializeField] private GameObject motehrObject;

    private  Dictionary<ChunkPoint,List <TilePoint>> objectPointList = new Dictionary<ChunkPoint, List<TilePoint>>();
    private Dictionary<TilePoint, GameObject> gameObjectWorldPointList = new Dictionary<TilePoint, GameObject>();

    private List<ObjectSize> objSize = new List<ObjectSize>();
    private ulong objCode = 0;

    public void Awake()
    {
        objSize.Add(new ObjectSize(1, 2, (int)ObjectNum.TREE));
        objSize.Add(new ObjectSize(3, 3, (int)ObjectNum.MINE));
        objSize.Add(new ObjectSize(3, 2, (int)ObjectNum.ANEXCHANGE));
        objSize.Add(new ObjectSize(3, 2, (int)ObjectNum.CARPENTERHOUSE));
        objSize.Add(new ObjectSize(3, 3, (int)ObjectNum.CHEESEHOUSE));
        objSize.Add(new ObjectSize(3, 3, (int)ObjectNum.FABRICHOUSE));
        objSize.Add(new ObjectSize(4, 2, (int)ObjectNum.FARM));
        objSize.Add(new ObjectSize(3, 3, (int)ObjectNum.HAMHOUSE));
        objSize.Add(new ObjectSize(2, 2, (int)ObjectNum.HOUSE));
        objSize.Add(new ObjectSize(2, 2, (int)ObjectNum.HUNTERHOUSE));
        objSize.Add(new ObjectSize(2, 2, (int)ObjectNum.MINEWORKERHOUSE));
        objSize.Add(new ObjectSize(2, 3, (int)ObjectNum.SMITHY));
        objSize.Add(new ObjectSize(3, 2, (int)ObjectNum.STORAGE));
        objSize.Add(new ObjectSize(3, 3, (int)ObjectNum.UNIVERSITAS));
        objSize.Add(new ObjectSize(2, 2, (int)ObjectNum.WOODCUTTERHOUSE));
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

        if (!objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
        {
            List<TilePoint> tilePoint = new List<TilePoint>();
            objectPointList.Add(new ChunkPoint(chunkX, chunkY), tilePoint);
            AddTilePoint(chunkX, chunkY, objectNum, tileX, tileY);
        }
        else
        {
            int index = objectPointList[new ChunkPoint(chunkX, chunkY)].FindIndex(a => a.tileX == tileX && a.tileY == tileY);
            if (index == -1)
            {
                AddTilePoint(chunkX, chunkY, objectNum, tileX, tileY);
            }

            return;
        }
    }

    private void AddTilePoint(int chunkX, int chunkY, int objectNum, int tileX, int tileY)
    {
        int index = objSize.FindIndex(a => a.objNum == objectNum);

        int minX = tileX - (int)((float)objSize[index].sizeX / 2f) + ((objSize[index].sizeX + 1) % 2);
        int maxX = tileX + (int)((float)objSize[index].sizeX / 2f);
        int minY = tileY - (int)((float)objSize[index].sizeY / 2f) + ((objSize[index].sizeY + 1) % 2);
        int maxY = tileY + (int)((float)objSize[index].sizeY / 2f);
        //Debug.Log("minX : " + minX + " maxX : " + maxX + " minY : " + minY + " maxY : " + maxY);
        objCode++;
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


    // 좌표에 맞게 오브젝트를 생성한다.
    public void CreateObejct(int chunkX, int chunkY)
    {
        // 해당 청크 좌표 정보를 딕셔너리에 키로 저장하고 있는지 확인. 없으면 리턴
        if (!ActiveTrueObjectPointList(chunkX, chunkY)) { return; }

        for (int i = 0; i < objectPointList[new ChunkPoint(chunkX, chunkY)].Count; i++)
        {
            if (objectPointList[new ChunkPoint(chunkX, chunkY)][i].isRoot)
            {// 오브젝트 생성과 부모 오브젝트 설정
                GameObject obj = Instantiate(objectArr[objectPointList[new ChunkPoint(chunkX, chunkY)][i].objectNum], motehrObject.transform);
                // 생성될 오브젝트의 좌표
                obj.transform.position = new Vector3
                    (
                    chunkX * 20 + objectPointList[new ChunkPoint(chunkX, chunkY)][i].tileX,
                    0,
                    chunkY * 20 + objectPointList[new ChunkPoint(chunkX, chunkY)][i].tileY
                    ); ;
                // 화면에 보이게 각도 설정
                obj.transform.eulerAngles = new Vector3(90, 0, 0);
            }
        }
    }

    // 지정된 범위 내에 동굴을 생성할 조건을 만족하는지 여부를 판단
    public bool CheckMineRange(int chunkX, int chunkY, GameObject chunk, int x, int y, Sprite tile, int chunkSize)
    {
        if (x - 1 <= 0 || x + 1 >= chunkSize || y - 1 <= 0 || y + 1 >= chunkSize) { return false; }

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
        if (y + 1 >= chunkSize) { return false; }

        for (int i = y; i <= y + 1; i++)
        {
            if (objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
            {
                int index = objectPointList[new ChunkPoint(chunkX, chunkY)].FindIndex(a => a.tileX == x && a.tileY == i);
                //Debug.Log(index);
                if (index != -1)
                {
                    //Debug.Log(index);
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
}
