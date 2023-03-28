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
        public TilePoint(int tileX, int tileY, int objectNum)
        {
            this.tileX = tileX;
            this.tileY = tileY;
            this.objectNum = objectNum;
        }
    }

    [SerializeField]private GameObject[] objectArr;
    [SerializeField] private GameObject motehrObject;

    Dictionary<ChunkPoint,List <TilePoint>> objectPointList = new Dictionary<ChunkPoint, List<TilePoint>>();
    Dictionary<TilePoint, GameObject> gameObjectWorldPointList = new Dictionary<TilePoint, GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        
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


    public void AddObjectPointList(int chunkX, int chunkY, int objectNum, int tileX, int tileY)
    {
        //Debug.Log("�ߵ� ! ! !");

        if (!objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY)))
        {
            List<TilePoint> tilePoint = new List<TilePoint>();
            objectPointList.Add(new ChunkPoint(chunkX, chunkY), tilePoint);
            objectPointList[new ChunkPoint(chunkX, chunkY)].Add(new TilePoint(tileX, tileY, objectNum));
        }
        else
        {
            int index = objectPointList[new ChunkPoint(chunkX, chunkY)].FindIndex(a => a.tileX == tileX && a.tileY == tileY);
            if (index == -1)
            {
                objectPointList[new ChunkPoint(chunkX, chunkY)].Add(new TilePoint(tileX, tileY, objectNum));
            }

            return;
        }

        //Debug.Log("�ߵ� !");
    }

    // ��ǥ�� �°� ������Ʈ�� �����Ѵ�.
    public void CreateObejct(int chunkX, int chunkY)
    {
        // �ش� ûũ ��ǥ ������ ��ųʸ��� Ű�� �����ϰ� �ִ��� Ȯ��. ������ ����
        if (!ActiveTrueObjectPointList(chunkX, chunkY)) { return; }


        for (int i = 0; i < objectPointList[new ChunkPoint(chunkX, chunkY)].Count; i++)
        {
            // ������Ʈ ������ �θ� ������Ʈ ����
            GameObject obj = Instantiate(objectArr[objectPointList[new ChunkPoint(chunkX, chunkY)][i].objectNum], motehrObject.transform);
            // ������ ������Ʈ�� ��ǥ
            obj.transform.position = new Vector3
                (
                chunkX * 20 + objectPointList[new ChunkPoint(chunkX, chunkY)][i].tileX,
                0,
                chunkY * 20 + objectPointList[new ChunkPoint(chunkX, chunkY)][i].tileY
                ); ;
            // ȭ�鿡 ���̰� ���� ����
            obj.transform.eulerAngles = new Vector3(90, 0, 0);
        }
    }

    // ������ ���� ���� ������ ������ ������ �����ϴ��� ���θ� �Ǵ�
    public bool CheckMineRange(GameObject chunk, int x, int y, Sprite tile, int chunkSize)
    {
        if (x - 1 <= 0 || x + 1 >= chunkSize || y - 1 <= 0 || y + 1 >= chunkSize) { return false; }

        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {

                if (chunk.transform.GetChild(i * chunkSize + j).GetComponent<SpriteRenderer>().sprite == tile)
                {
                    return false;
                }
            }
        }

        return true;
    }

    // ������ ���� ���� ������ ������ ������ �����ϴ��� ���θ� �Ǵ�
    public bool CheckTreeRange(GameObject chunk, int x, int y, Sprite tile, int chunkSize)
    {
        if (y + 1 >= chunkSize) { return false; }

        for (int i = y; i <= y + 1; i++)
        {
            if (chunk.transform.GetChild(x * chunkSize + i).GetComponent<SpriteRenderer>().sprite == tile)
            {
                return false;
            }
        }

        return true;
    }
}
