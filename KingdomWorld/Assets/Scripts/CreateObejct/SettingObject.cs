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
        public int objectNum { get; }
        // 생성자
        public ChunkPoint(int chunkX , int chunkY, int objectNum)
        {
            this.chunkX = chunkX;
            this.chunkY = chunkY;
            this.objectNum = objectNum;
        }
    }
    struct TilePoint
    {
        public int tileX { get; }
        public int tileY { get; }

        public TilePoint(int tileX, int tileY)
        {
            this.tileX = tileX;
            this.tileY = tileY;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ActiveTrueObjectPointList(int chunkX, int chunkY, int objectNum)
    {
        //Debug.Log("발동 ! ! !");

        if (objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY, objectNum)))
        {
            return true;
        }
        return false;
    }


    public void AddObjectPointList(int chunkX, int chunkY, int objectNum, int tileX, int tileY)
    {
        //Debug.Log("발동 ! ! !");

        if (!objectPointList.ContainsKey(new ChunkPoint(chunkX, chunkY, objectNum)))
        {
            List<TilePoint> tilePoint = new List<TilePoint>();
            objectPointList.Add(new ChunkPoint(chunkX, chunkY, objectNum), tilePoint);
            objectPointList[new ChunkPoint(chunkX, chunkY, objectNum)].Add(new TilePoint(tileX, tileY));
            //Debug.Log(tileX + " " + tileY);
        }
        else
        {
            objectPointList[new ChunkPoint(chunkX, chunkY, objectNum)].Add(new TilePoint(tileX, tileY));
            return;
        }

        //Debug.Log("발동 !");
    }

    public void CreateObejct(int chunkX, int chunkY, int objectNum)
    {
        //Debug.Log("발동 ! ! ! ");
        if (!ActiveTrueObjectPointList(chunkX, chunkY, objectNum)) { return; }


        for (int i = 0; i < objectPointList[new ChunkPoint(chunkX, chunkY, objectNum)].Count; i++)
        {
            GameObject obj = Instantiate(objectArr[objectNum], motehrObject.transform);
            obj.transform.position = new Vector3(
                chunkX * 20 + objectPointList[new ChunkPoint(chunkX, chunkY, objectNum)][i].tileX,
                0,
                chunkY * 20 + objectPointList[new ChunkPoint(chunkX, chunkY, objectNum)][i].tileY
                ); ;
            obj.transform.eulerAngles = new Vector3(90, 0, 0);
        }
    }
}
