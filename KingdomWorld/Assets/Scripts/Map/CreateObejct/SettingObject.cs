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

    // ûũ ��ǥ�� ���� ������Ʈ ��ǥ ����Ʈ
    private  Dictionary<ChunkPoint,List <TilePoint>> objectPointList = new Dictionary<ChunkPoint, List<TilePoint>>();
    // ûũ ��ǥ�� ���� ���ӿ�����Ʈ ����Ʈ
    private Dictionary<ChunkPoint, List<GameObject>> gameObjectChunkPointList = new Dictionary<ChunkPoint, List<GameObject>>();

    private List<ObjectSize> objSize = new List<ObjectSize>();
    private ulong objCode = 0;

    public void Awake()
    {
        // ������Ʈ�� ���� ���� ����� �� �� ĭ ������(16�ȼ�)�� �������� ���ص�.
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
        int index = objSize.FindIndex(a => a.objNum == objectNum);
        // ������ ������Ʈ �߾��� �������� ��� Ÿ�Ͽ� ������Ʈ�� ��� �Ǵ��� ���� ���
        int minX = tileX - (int)((float)objSize[index].sizeX / 2f) + ((objSize[index].sizeX + 1) % 2);
        int maxX = tileX + (int)((float)objSize[index].sizeX / 2f);
        int minY = tileY - (int)((float)objSize[index].sizeY / 2f) + ((objSize[index].sizeY + 1) % 2);
        int maxY = tileY + (int)((float)objSize[index].sizeY / 2f);

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
}