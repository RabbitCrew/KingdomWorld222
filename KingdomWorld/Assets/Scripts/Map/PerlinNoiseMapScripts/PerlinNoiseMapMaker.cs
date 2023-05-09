using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectNS;
public class PerlinNoiseMapMaker : MonoBehaviour
{
    [SerializeField] private Texture2D gradientTex;
    [SerializeField] private Sprite[] tile;
    [SerializeField] private Sprite[] snowTile;
    [SerializeField] private Material material;
    [SerializeField] private int chunkSize = 20;    // ûũ ������� 20 * 20
    [SerializeField] private int gradientSize = 1000;
    [SerializeField] private int worldSize = 80;
    [SerializeField] private float noiseFreq = 0.2f;
    [SerializeField] private SettingObject settingObject;
    [SerializeField] private SpriteManager spriteManager;
    struct Point
    {
        public float x { get; }   // �����ڿ����� ������ ������. �б�����
        public float z { get; }   // �����ڿ����� ������ ������. �б�����

        public Point(float x, float z)
        {
            this.x = x;
            this.z = z;
        }
    }

    struct TileOrder
	{
        public int tileNum { get; }
        public int tileOrder { get; }

        public TileOrder(int tileNum, int tileOrder)
		{
            this.tileNum = tileNum;
            this.tileOrder = tileOrder;
		}
	}


    public float seed;
    public Texture2D noiseTexture;
    public GameObject mother;
    public GameObject[,] worldChunks;

    private GameObject childTile;
    private Transform cameraTrans;
    private Vector3 startVector3;
    private List<Point> pointList = new List<Point>();
    private List<GameObject> falseChunksList = new List<GameObject>();
    private Dictionary<int, TileOrder> tileDIc = new Dictionary<int, TileOrder>();
    private int startChunkX;
    private int startChunkZ;
    private int preCameraXMax;
    private int preCameraXMin;
    private int preCameraZMax;
    private int preCameraZMin;
    private bool isStartBuilding;
    private bool isStartChunk;
    private bool isSnow;
    private void Awake()
    {
        // ĳ��~
        cameraTrans = Camera.main.transform;
        // �õ带 �������� ����. �õ� ���� ���� �ٸ� ����� ����
        seed = Random.Range(-10000, 10000);

        startChunkX = Random.Range(-12, 13);
        startChunkZ = Random.Range(-12, 13);
        isStartBuilding = false;
        isStartChunk = false;
        isSnow = false;

        preCameraXMax = 99999;
        preCameraXMin = -99999;
        preCameraZMax = 99999;
        preCameraZMin = -99999;
    }
    public void InitStart()
    {
        float[,] fl = GenerateNoiseTexture();
        // ûũ�� �����ϴ� �Լ�
        CreateChunks();
        DrawNoiseMap(fl, GenerateMap(worldSize, worldSize));
        GenerateTerrain();
        mother.transform.eulerAngles = new Vector3(90, 0, 0);

        cameraTrans.position = new Vector3(startChunkX * 20f, 40f, startChunkZ * 20f);
		// ȭ�� �ѹ� ���ȴٰ� RefreshChunks�� ������ ���� Ÿ�Ͽ� ���� �־���
		RefreshChunks();
		Camera.main.transform.position += new Vector3(0, 0, 200f);
		RefreshChunks();
		Camera.main.transform.position -= new Vector3(0, 0, 200f);

		for (int i = -4; i < 4; i++)
		{
			for (int j = -4; j < 4; j++)
			{
				RefreshChunks();
				cameraTrans.position = new Vector3(i * 20f, 40f, j * 20f);
			}
		}
        cameraTrans.position = startVector3;

    }
    private void Update()
    {
        RefreshChunks();
    }
    // ûũ�� �����ϴ� �Լ�
    public void CreateChunks()
    {
        int numChunksX = worldSize / chunkSize;
        int numChunksY = worldSize / chunkSize;
        worldChunks = new GameObject[numChunksX, numChunksY];
        for (int x = 0; x < numChunksX; x++)
        {
            for (int y = 0; y < numChunksY; y++)
            {
                GameObject newChunk = new GameObject();
                newChunk.transform.parent = mother.transform;
                newChunk.transform.localPosition = new Vector3(x * chunkSize, y * chunkSize, 0);
                worldChunks[x, y] = newChunk;
            }
        }
    }
    public void SnowRefreshChunk(WinterIsComing winter)
	{
        isSnow = true;
        settingObject.ChangeSnowBuildingSprite(true);
        Camera.main.transform.position += new Vector3(0, 0, 100f);
        RefreshChunks();
        Camera.main.transform.position -= new Vector3(0, 0, 100f);
        //RefreshChunks();
        winter.isChangedSprite = true;

    }



    // ��Ŭ���� �ø��� ���� �Լ�
    void RefreshChunks()
    {
        int cameraX = ((int)(cameraTrans.position.x) / chunkSize);
        int cameraZ = ((int)(cameraTrans.position.z) / chunkSize);
        // ī�޶� ��ġ�� ���� ��������� ûũ ��ǥ ������ �����ش�.
        int cameraXMax = Mathf.RoundToInt(cameraX + 1.8f);
        int cameraXMin = Mathf.RoundToInt(cameraX - 1.8f);
        int cameraZMax = Mathf.RoundToInt(cameraZ + 1.8f);
        int cameraZMin = Mathf.RoundToInt(cameraZ - 1.8f);

        if (cameraXMax == preCameraXMax
            && cameraXMin == preCameraXMin 
            && cameraZMax == preCameraZMax
            && cameraZMin == preCameraZMin)
        { return; }

        // ���� ī�޶� ���� �ۿ� �ִ� ���� ������Ʈ�� ���ش�. 
        for (int x = 0; x < worldChunks.GetLength(0); x++)
        {
            for (int y = 0; y < worldChunks.GetLength(1); y++)
            {
                // ī�޶� �ʸӿ� �ִ� ûũ���� ��Ȱ��ȭ ��Ų��.
                if (((int)worldChunks[x, y].transform.localPosition.x / chunkSize >= cameraXMax ||
                    (int)worldChunks[x, y].transform.localPosition.x / chunkSize < cameraXMin ||
                    (int)worldChunks[x, y].transform.localPosition.y / chunkSize >= cameraZMax ||
                    (int)worldChunks[x, y].transform.localPosition.y / chunkSize < cameraZMin))
                {
                    // ��Ȱ��ȭ ������ ûũ�� ��Ȱ��ȭ ûũ ����Ʈ (falseChunksList)�� ���� �ʾƾ� �Ѵ�. ������ �����ֱ� ����
                    if (falseChunksList.FindIndex(a => a.Equals(worldChunks[x, y])) == -1)
                    {
                        // �ش� ûũ�� ��Ȱ��ȭ ��Ų��.
                        worldChunks[x, y].SetActive(false);
                        // ��Ȱ��ȭ ûũ ����Ʈ(falseChunkList)�� �߰��Ѵ�.
                        falseChunksList.Add(worldChunks[x, y]);
                        // ûũ ��ǥ ����Ʈ(pointList)�� ��Ȱ��ȭ��Ų ûũ�� ��ǥ���� ����Ǿ��ִ��� �ε��� ���� ���Ѵ�.
                        int n = pointList.FindIndex(a => a.x == worldChunks[x, y].transform.localPosition.x && a.z == worldChunks[x, y].transform.localPosition.y);
                        // ���� ��ǥ���� ����Ǿ� ���� ��� �� ���� ����Ʈ���� �����Ѵ�.
                        if (n != -1) { pointList.RemoveAt(n); }
                        // ������Ʈ�� ��������Ʈ �������� ���ش�.
                        int chunkX = (int)worldChunks[x, y].transform.localPosition.x / 20;
                        int chunkY = (int)worldChunks[x, y].transform.localPosition.y / 20;
                        settingObject.DisableSpriteRenderer(chunkX, chunkY);
                    }
                }
                // ûũ�� �ʿ��� �ڸ��� �̹� Ȱ��ȭ�ϰ� ������ pointList�� ��ǥ���� ����Ǿ����� ���� ��
                else if (pointList.FindIndex(a => a.x == worldChunks[x, y].transform.localPosition.x && a.z == worldChunks[x, y].transform.localPosition.y) == -1 &&
                         pointList.Count + falseChunksList.Count < worldChunks.GetLength(0) * worldChunks.GetLength(1))
                {
                    //ûũ ��ǥ�� ���� ���� ������Ѵ�.
                    RefreshTexture(worldChunks[x, y], worldChunks[x, y].transform.localPosition.x, worldChunks[x, y].transform.localPosition.y);
                    //ûũ ��ǥ�� ûũ ��ǥ ����Ʈ(pointList)�� �߰��Ѵ�.
                    pointList.Add(new Point(worldChunks[x, y].transform.localPosition.x, worldChunks[x, y].transform.localPosition.y));

                }
            }
        }
        //���� ���� ī�޶� �������� ȭ��� ����ִ� ���� ��Ȱ��ȭ�� ûũ�� �ְ� �ؽ�ó�� ���ΰ�ħ�Ѵ�.
        for (int x = cameraXMin; x < cameraXMax; x++)
        {
            for (int z = cameraZMin; z < cameraZMax; z++)
            {
                if (pointList.FindIndex(a => a.x == x * chunkSize && a.z == z * chunkSize) == -1)
                {
                    falseChunksList[0].SetActive(true);
                    falseChunksList[0].transform.localPosition = new Vector3(x * chunkSize, z * chunkSize, 0);
                    RefreshTexture(falseChunksList[0], x * chunkSize, z * chunkSize);
                    pointList.Add(new Point(falseChunksList[0].transform.localPosition.x, falseChunksList[0].transform.localPosition.y));
                    falseChunksList.RemoveAt(0);
                    // ������Ʈ�� ��������Ʈ �������� ���ش�.
                    settingObject.EnableSpriteRenderer(x, z);
                }
            }
        }

        preCameraXMax = cameraXMax;
        preCameraXMin = cameraXMin;
        preCameraZMax = cameraZMax;
        preCameraZMin = cameraZMin;

    }
    // Ÿ���� �����ؼ� ûũ�� ���´�.
    public void GenerateTerrain()
    {
        for (int x = 0; x < worldSize; x++)
        {
            for (int y = 0; y < worldSize; y++)
            {
                GameObject newTile = new GameObject(name = "tile");

                // Round �Լ��� �ݿø��� ���� float�ڷ������� ��ȯ�ϴ� �Լ�.
                // RoundToInt �Լ��� �ݿø��� ���� int�ڷ������� ��ȯ�ϴ� �Լ�.
                // ûũ ��ǥ�� ����
                float chunkCoordX = (Mathf.RoundToInt(x / chunkSize) * chunkSize);
                chunkCoordX /= chunkSize;
                float chunkCoordY = (Mathf.RoundToInt(y / chunkSize) * chunkSize);
                chunkCoordY /= chunkSize;
                // ������ Ÿ�� ������Ʈ�� ûũ ��ǥ�� ���� �θ� ������Ʈ�� ����. 
                newTile.transform.parent = worldChunks[(int)chunkCoordX, (int)chunkCoordY].transform;
                // ������Ʈ���� �־��ش�.
                newTile.AddComponent<SpriteRenderer>();
                newTile.AddComponent<BoxCollider>();
                newTile.GetComponent<BoxCollider>().size = new Vector3(0.99f, 0.99f, 1.0f);
                newTile.AddComponent<TileColorChange>();
                newTile.AddComponent<TileInfo>();
                newTile.GetComponent<SpriteRenderer>().material = material;
                // �Ŀ� Ÿ�� ������Ʈ ������ SettingObject Ŭ������ �Լ��� ȣ���ϱ� ����.
                newTile.GetComponent<TileInfo>().InitSettingObject(settingObject);
                newTile.GetComponent<BoxCollider>().isTrigger = true;
                // Ÿ�� ������Ʈ�� ��ġ�� ������.
                newTile.transform.localPosition = new Vector3(x % chunkSize, y % chunkSize);

                for (int i = 0; i < 13; i++)
				{
                    GameObject miniTile = new GameObject(name = "miniTile");
                    miniTile.transform.parent = newTile.transform;
                    miniTile.AddComponent<SpriteRenderer>();
                    miniTile.GetComponent<SpriteRenderer>().material = material;
                    miniTile.SetActive(false);
                }
            }
        }
    }

    private float[,] GenerateNoiseTexture()
    {
        float[,] noiseMap = new float[worldSize, worldSize];
        noiseTexture = new Texture2D(worldSize, worldSize);

        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                // PerlinNoise�� 0~1 ������ ���� ��ȯ�ϴ� �Լ�.
                float v = Mathf.PerlinNoise((x + seed) * noiseFreq, (y + seed) * noiseFreq);
                noiseTexture.SetPixel(x, y, new Color(v, v, v));
                noiseMap[x, y] = v;
                //Debug.Log(v);
            }
        }

        noiseTexture.Apply();

        return noiseMap;
    }

    // �ؽ��ĸ� �� ��ġ�� �°� �ٽ� �ٲ��ش�.
    private void RefreshTexture(GameObject chunk, float pointX, float pointY)
    {
        float[,] noiseMap = new float[chunkSize, chunkSize];
        float[,] gradientMap = new float[chunkSize, chunkSize];
        Color[,] color = new Color[chunkSize, chunkSize];

        int chunkX = (int)chunk.transform.localPosition.x / 20;
        int chunkY = (int)chunk.transform.localPosition.y / 20;
        // �ش� ûũ���� ������Ʈ�� �ѹ��� ������ ���� ������ ������Ŵ
        bool isObject = settingObject.ActiveTrueObjectPointList(chunkX, chunkY);


        // ûũ �Ѱ� �����ŭ �ݺ�
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                //// ������ ��ǥ������ �޸������� ���� �����´�. �޸� ������� 0~1 ������ ���̴�.
                //// seed�� awake���� �� �ѹ� �����ǹǷ� �޸� ������ ���� �ٲ��� �ʴ´�.
                //float v = Mathf.PerlinNoise((x + pointX + seed) * noiseFreq, (y + pointY + seed) * noiseFreq);
                //noiseMap[x, y] = v;
                ////x�� y�� 0~19������ ��. �ű⿡ pointX,Y���� ûũ ��ǥ�� ��ġ��
                ////�׸��� gradientSize�� �׶���Ʈ �ؽ��Ŀ��� ��ǥ��(gradientSize, gradientSize) �̹Ƿ� 0.5f�� ���� ���ؼ� (0,0)��ǥ���� �ؽ�ó�� �߾����� ����
                ////�׷��� �ؽ�ó�� �߾ӿ��� (x + pointX, y + pointY)���� ���ϰ� �Ǵ� ��.
                ////�ű⿡ ���� �ؽ�ó�� ���� ���� ����� �����ְ�, �̸� gradientSize�� ��ŭ �������ش�.(������.)
                ////������ ��ǥ���� ���� �׶���Ʈ�� ��ǥ���� xCoord, yCoord���� ����ȴ�.
                //int xCoord = Mathf.RoundToInt((x + pointX + gradientSize * 0.5f) * (float)gradientTex.width / gradientSize);
                //int yCoord = Mathf.RoundToInt((y + pointY + gradientSize * 0.5f) * (float)gradientTex.height / gradientSize);
                ////Debug.Log(xCoord + " " + yCoord);
                ////�׶���Ʈ �ؽ�ó���� ��ǥ���� ���Ͽ� �׷��̽����� ���� ���ϰ� �̸� �迭�� �����Ѵ�. �� ���� 0���� 1���� ���� ������.
                //gradientMap[x, y] = gradientTex.GetPixel(xCoord, yCoord).grayscale;
                ////���� �� �Ʒ��� 0, ������ ������ 0.75�� �����Ѵ�.
                //if (gradientMap[x, y] <= 0.3f) { gradientMap[x, y] = 0f; }
                //else if (gradientMap[x, y] >= 0.75f) { gradientMap[x, y] = 0.75f; }
                //// �޸������� ���� �׶���Ʈ ���� ��ģ��.
                //float value = noiseMap[x, y] + gradientMap[x, y];
                //// �� ���� ���� 0���� 2���� ���� ���ϰ� �ȴ�.(������ ������ 0.75�� ���������� �̴� ����)
                //// InverseLerp(a,b,c)�� a���� b�� ���� c�� ���� �ۼ�Ʈ�� ȯ���Ͽ� 0~1������ ������ ��ȯ���Ѽ� ��ȯ�Ѵ�. �� 0~1������ ���� ��ȯ�Ѵ�.
                //value = Mathf.InverseLerp(0, 2, value);
                //// Color.Lerp�� value ���� ����Ͽ� ����,���� ȸ����, ��� ���� �����Ѵ�. 
                //color[x, y] = Color.Lerp(Color.black, Color.white, value);
                // color �迭�� �׷��̽����Ͽ� ���� Ÿ���� ��������Ʈ�� �ٽ� �����Ѵ�.
     //           if (color[x, y].grayscale < 0.2)
     //           {
     //               if (!isSnow)
     //               {
     //                   chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = tile[(int)TileNum.OCEAN];
     //               }
					//else
					//{
     //                   chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = snowTile[(int)TileNum.OCEAN];
     //               }
     //               chunk.transform.GetChild(x * chunkSize + y).tag = "NotWalkable";
     //           }
     //           else if (color[x, y].grayscale < 0.375)
     //           {
     //               if (!isSnow)
     //               {
     //                   chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = tile[(int)TileNum.RIVER];
     //                   chunk.transform.GetChild(x * chunkSize + y).tag = "NotWalkable";
     //               }
     //               else
     //               {
     //                   chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = snowTile[(int)TileNum.RIVER];
     //                   chunk.transform.GetChild(x * chunkSize + y).tag = "Walkable";
     //               }
     //           }
     //           else if (color[x, y].grayscale < 0.475)
     //           {
     //               if (!isSnow)
     //               {
     //                   chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = tile[(int)TileNum.GRASS];
     //               }
     //               else
					//{
     //                   chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = snowTile[(int)TileNum.GRASS];
     //               }
     //               chunk.transform.GetChild(x * chunkSize + y).tag = "Walkable";
     //           }
     //           else if (color[x, y].grayscale < 0.6)
     //           {
     //               if (!isSnow)
     //               {
     //                   chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = tile[(int)TileNum.BUMPYTILE];
     //               }
     //               else
					//{
     //                   chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = snowTile[(int)TileNum.BUMPYTILE];
     //               }
     //               chunk.transform.GetChild(x * chunkSize + y).tag = "Walkable";
     //           }
     //           else if (color[x, y].grayscale < 0.785)
     //           {
     //               if (!isSnow)
     //               {
     //                   chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = tile[(int)TileNum.FLATTILE];
     //               }
     //               else
					//{
     //                   chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = snowTile[(int)TileNum.FLATTILE];
     //               }
     //               chunk.transform.GetChild(x * chunkSize + y).tag = "Walkable";
     //           }
     //           else
     //           {
     //               if (!isSnow)
     //               {
     //                   chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = tile[(int)TileNum.STONE];
                        
     //               }
     //               else
     //               {
     //                   chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = snowTile[(int)TileNum.STONE];
     //               }
     //               chunk.transform.GetChild(x * chunkSize + y).tag = "NotWalkable";
     //           }

                DecideSprite((int)(x + pointX), (int)(y + pointY), chunk, x, y, chunkX, chunkY);
            }
        }

        if (!isStartBuilding)
		{
            for (int x = 0; x < chunkSize; x++)
			{
                for (int y = 0; y < chunkSize; y++)
				{
                    if (settingObject.CheckStartBuildingRange(chunkX, chunkY, chunk, x, y, (int)TileNum.OCEAN, chunkSize) &&
                        settingObject.CheckStartBuildingRange(chunkX, chunkY, chunk, x, y, (int)TileNum.RIVER, chunkSize) &&
                        settingObject.CheckStartBuildingRange(chunkX, chunkY, chunk, x, y, (int)TileNum.STONE, chunkSize))
                    {
                        settingObject.AddTilePoint3(chunkX, chunkY, (int)ObjectTypeNum.HOUSE, x-2, y);
                        settingObject.AddTilePoint3(chunkX, chunkY, (int)ObjectTypeNum.CARPENTERHOUSE, x + 1, y);
                        isStartBuilding = true;
                        startVector3 = new Vector3(pointX + x, 40, pointY + y);
                        break;
                    }
                }
                if (isStartBuilding) { break; }
			}
		}

        // �����ؾߵ� ������Ʈ�� ��ǥ�� ���ϰ� �����Ѵ�.
        // �ش� ûũ���� ������Ʈ�� �ѹ��� ������ ���� ������ ������Ŵ
        if (!isObject)
        {
            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    int ran = Random.Range(0, 700);

                    // ���� ���� Ȯ��
                    if (ran < 1)
                    {
                        if (settingObject.CheckMineRange(chunkX, chunkY, chunk, x, y, (int)TileNum.OCEAN, chunkSize) &&
                            settingObject.CheckMineRange(chunkX, chunkY, chunk, x, y, (int)TileNum.RIVER, chunkSize) &&
                            settingObject.CheckMineRange(chunkX, chunkY, chunk, x, y, (int)TileNum.STONE, chunkSize))
                        {
                            settingObject.AddObjectPointList(chunkX, chunkY, (int)ObjectTypeNum.MINE, x, y);
                        }
                    }
                    // ���� ���� Ȯ��
                    else if (ran < 80)
                    {
                        if (settingObject.CheckTreeRange(chunkX, chunkY, chunk, x, y, (int)TileNum.OCEAN, chunkSize) &&
                            settingObject.CheckTreeRange(chunkX, chunkY, chunk, x, y, (int)TileNum.RIVER, chunkSize) &&
                            settingObject.CheckTreeRange(chunkX, chunkY, chunk, x, y, (int)TileNum.STONE, chunkSize))
                        {
                            settingObject.AddObjectPointList(chunkX, chunkY, (int)ObjectTypeNum.TREE, x, y);
                        }
                    }
                }
            }
        }
        //������ ��ǥ�� ������Ʈ�� �����Ѵ�.
        // �ش� ûũ���� ������Ʈ�� �ѹ��� ������ ���� ������ ������Ŵ
        if (!isObject) { settingObject.CreateObejct(chunkX, chunkY); }
        // �� ������ ���� ������Ʈ�� Ȯ�������� �����Ǿ����Ƿ� isObject�� ������ true(��� �Ǵ� �ڵ��ε�?)
        isObject = true;
    }
    // RefreshTexture���� worldX�� x + pointX, worldY�� y + pointY �� �����ڷ� �޾ƿ�
    private void DecideSprite(int worldX, int worldY, GameObject chunk, int x, int y, int chunkX, int chunkY)
	{
        GameObject tile = chunk.transform.GetChild(x * chunkSize + y).gameObject;
        float[] nearBlockArr = new float[9];
        tileDIc.Clear();

        int cnt = 0;

        for (int i = 0; i < tile.transform.childCount; i++)
		{
            tile.transform.GetChild(i).gameObject.SetActive(false);
		}

        for (int j = worldY + 1; j > worldY - 2; j--)
        {
            for (int i = worldX - 1; i < worldX + 2; i++)
            {

                float v = Mathf.PerlinNoise((i + seed) * noiseFreq, (j + seed) * noiseFreq);
                int xCoord = Mathf.RoundToInt((i + gradientSize * 0.5f) * (float)gradientTex.width / gradientSize);
                int yCoord = Mathf.RoundToInt((j + gradientSize * 0.5f) * (float)gradientTex.height / gradientSize);
                float g = gradientTex.GetPixel(xCoord, yCoord).grayscale;
                if (g <= 0.3f) { g = 0f; }
                else if (g >= 0.75) { g = 0.75f; }
                float value = v + g;
                value = Mathf.InverseLerp(0, 2, value);
                float gray = Color.Lerp(Color.black, Color.white, value).grayscale;

                nearBlockArr[cnt] = Color.Lerp(Color.black, Color.white, value).grayscale;
                cnt++;
            }
        }

        for (int i = 0; i < nearBlockArr.Length; i++)
		{
            if(nearBlockArr[i] < 0.2f) { tileDIc.Add(i, new TileOrder((int)TileNum.OCEAN, 950)); }
            else if (nearBlockArr[i] < 0.375f) { tileDIc.Add(i, new TileOrder((int)TileNum.RIVER, 960)); }
            else if (nearBlockArr[i] < 0.475f) { tileDIc.Add(i, new TileOrder((int)TileNum.GRASS, 990)); }
            else if (nearBlockArr[i] < 0.6f) { tileDIc.Add(i, new TileOrder((int)TileNum.BUMPYTILE, 973)); }
            else if (nearBlockArr[i] < 0.785f) { tileDIc.Add(i, new TileOrder((int)TileNum.FLATTILE, 970)); }
            else { tileDIc.Add(i, new TileOrder((int)TileNum.STONE, 980)); }
        }
        // �»��
        if (tileDIc[0].tileOrder >= tileDIc[4].tileOrder && tileDIc[0].tileNum != tileDIc[1].tileNum && tileDIc[0].tileNum != tileDIc[3].tileNum && tileDIc[0].tileNum != tileDIc[4].tileNum)
		{
            childTile = FindChildObj(tile);
            childTile.GetComponent<SpriteRenderer>().sprite = spriteManager.GetTileSprite(tileDIc[0].tileNum, 8, isSnow);
            childTile.GetComponent<SpriteRenderer>().sortingOrder = tileDIc[0].tileOrder;
            childTile.transform.localPosition = Vector3.zero;
            if (tileDIc[0].tileOrder > tileDIc[4].tileOrder + 5)
			{
                childTile = FindChildObj(tile);
                childTile.GetComponent<SpriteRenderer>().sprite = spriteManager.GetTileShadowSprite(tileDIc[0].tileNum, 7, isSnow);
                childTile.GetComponent<SpriteRenderer>().sortingOrder = tileDIc[0].tileOrder - 5;
                childTile.transform.localPosition = Vector3.zero;
            }
        }
        // �߻��
        if (tileDIc[1].tileOrder >= tileDIc[4].tileOrder && tileDIc[1].tileNum != tileDIc[4].tileNum)
		{
            childTile = FindChildObj(tile);
            childTile.GetComponent<SpriteRenderer>().sprite = spriteManager.GetTileSprite(tileDIc[1].tileNum, 7, isSnow);
            childTile.GetComponent<SpriteRenderer>().sortingOrder = tileDIc[1].tileOrder;
            childTile.transform.localPosition = Vector3.zero;
            if (tileDIc[1].tileOrder > tileDIc[4].tileOrder + 5)
			{
                childTile = FindChildObj(tile);
                childTile.GetComponent<SpriteRenderer>().sprite = spriteManager.GetTileShadowSprite(tileDIc[1].tileNum, 6, isSnow);
                childTile.GetComponent<SpriteRenderer>().sortingOrder = tileDIc[1].tileOrder - 5;
                childTile.transform.localPosition = Vector3.zero;
            }
        }
        // ����
        if (tileDIc[2].tileOrder >= tileDIc[4].tileOrder && tileDIc[2].tileNum != tileDIc[1].tileNum && tileDIc[2].tileNum != tileDIc[5].tileNum && tileDIc[2].tileNum != tileDIc[4].tileNum)
        {
            childTile = FindChildObj(tile);
            childTile.GetComponent<SpriteRenderer>().sprite = spriteManager.GetTileSprite(tileDIc[2].tileNum, 6, isSnow);
            childTile.GetComponent<SpriteRenderer>().sortingOrder = tileDIc[2].tileOrder;
            childTile.transform.localPosition = Vector3.zero;
            if (tileDIc[2].tileOrder > tileDIc[4].tileOrder + 5)
            {
                childTile = FindChildObj(tile);
                childTile.GetComponent<SpriteRenderer>().sprite = spriteManager.GetTileShadowSprite(tileDIc[2].tileNum, 5, isSnow);
                childTile.GetComponent<SpriteRenderer>().sortingOrder = tileDIc[2].tileOrder - 5;
                childTile.transform.localPosition = Vector3.zero;
            }
        }
        // ���ߴ�
        if (tileDIc[3].tileOrder >= tileDIc[4].tileOrder && tileDIc[3].tileNum != tileDIc[4].tileNum)
        {
            childTile = FindChildObj(tile);
            childTile.GetComponent<SpriteRenderer>().sprite = spriteManager.GetTileSprite(tileDIc[3].tileNum, 5, isSnow);
            childTile.GetComponent<SpriteRenderer>().sortingOrder = tileDIc[3].tileOrder;
            childTile.transform.localPosition = Vector3.zero;
            if (tileDIc[3].tileOrder > tileDIc[4].tileOrder + 5)
            {
                childTile = FindChildObj(tile);
                childTile.GetComponent<SpriteRenderer>().sprite = spriteManager.GetTileShadowSprite(tileDIc[3].tileNum, 4, isSnow);
                childTile.GetComponent<SpriteRenderer>().sortingOrder = tileDIc[3].tileOrder - 5;
                childTile.transform.localPosition = Vector3.zero;
            }
        }
        // ���߾�
        tile.GetComponent<SpriteRenderer>().sprite = spriteManager.GetTileSprite(tileDIc[4].tileNum, 4, isSnow);
        tile.GetComponent<SpriteRenderer>().sortingOrder = tileDIc[4].tileOrder;
        tile.GetComponent<TileInfo>().TileNum = tileDIc[4].tileNum;
        AttachTag(tile, tileDIc[4].tileNum);

        //���ߴ�
        if (tileDIc[5].tileOrder >= tileDIc[4].tileOrder && tileDIc[5].tileNum != tileDIc[4].tileNum)
        {
            childTile = FindChildObj(tile);
            childTile.GetComponent<SpriteRenderer>().sprite = spriteManager.GetTileSprite(tileDIc[5].tileNum, 3, isSnow);
            childTile.GetComponent<SpriteRenderer>().sortingOrder = tileDIc[5].tileOrder;
            childTile.transform.localPosition = Vector3.zero;
            if (tileDIc[5].tileOrder > tileDIc[4].tileOrder + 5)
            {
                childTile = FindChildObj(tile);
                childTile.GetComponent<SpriteRenderer>().sprite = spriteManager.GetTileShadowSprite(tileDIc[5].tileNum, 3, isSnow);
                childTile.GetComponent<SpriteRenderer>().sortingOrder = tileDIc[5].tileOrder - 5;
                childTile.transform.localPosition = Vector3.zero;
            }
        }

        //���ϴ�
        if (tileDIc[6].tileOrder >= tileDIc[4].tileOrder && tileDIc[6].tileNum != tileDIc[3].tileNum && tileDIc[6].tileNum != tileDIc[7].tileNum && tileDIc[6].tileNum != tileDIc[4].tileNum)
        {
            childTile = FindChildObj(tile);
            childTile.GetComponent<SpriteRenderer>().sprite = spriteManager.GetTileSprite(tileDIc[6].tileNum, 2, isSnow);
            childTile.GetComponent<SpriteRenderer>().sortingOrder = tileDIc[6].tileOrder;
            childTile.transform.localPosition = Vector3.zero;
        }

        //���ϴ�
        if (tileDIc[7].tileOrder >= tileDIc[4].tileOrder && tileDIc[7].tileNum != tileDIc[4].tileNum)
        {
            childTile = FindChildObj(tile);
            childTile.GetComponent<SpriteRenderer>().sprite = spriteManager.GetTileSprite(tileDIc[7].tileNum, 1, isSnow);
            childTile.GetComponent<SpriteRenderer>().sortingOrder = tileDIc[7].tileOrder;
            childTile.transform.localPosition = Vector3.zero;
        }

        //���ϴ�
        if (tileDIc[8].tileOrder >= tileDIc[4].tileOrder && tileDIc[8].tileNum != tileDIc[5].tileNum && tileDIc[8].tileNum != tileDIc[7].tileNum && tileDIc[8].tileNum != tileDIc[4].tileNum)
        {
            childTile = FindChildObj(tile);
            childTile.GetComponent<SpriteRenderer>().sprite = spriteManager.GetTileSprite(tileDIc[8].tileNum, 0, isSnow);
            childTile.GetComponent<SpriteRenderer>().sortingOrder = tileDIc[8].tileOrder;
            childTile.transform.localPosition = Vector3.zero;
        }
    }
    private GameObject FindChildObj(GameObject tile)
	{
        for (int i = 0; i < tile.transform.childCount; i++)
		{
            if (!tile.transform.GetChild(i).gameObject.activeSelf)
			{
                tile.transform.GetChild(i).gameObject.SetActive(true);
                return tile.transform.GetChild(i).gameObject;
			}
		}

        return null;
	}

    private void AttachTag(GameObject tile, int tileNum)
	{
        switch ((TileNum)tileNum)
        {
            case TileNum.OCEAN: tile.tag = "NotWalkable"; break;
            case TileNum.RIVER:
                if (!isSnow) { tile.tag = "NotWalkable"; }
                else { tile.tag = "Walkable"; }
                break;
            case TileNum.FLATTILE: tile.tag = "Walkable"; break;
            case TileNum.BUMPYTILE: tile.tag = "Walkable"; break;
            case TileNum.STONE: tile.tag = "Stone"; break;
            case TileNum.GRASS: tile.tag = "Walkable"; break;
        }
    }

    public float[,] GenerateMap(int width, int height)
    {
        float[,] gradientMap = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // �ؽ�ó ���� ũ�� ���� ���� ��ǥ ����
                int xCoord = Mathf.RoundToInt(x * (float)gradientTex.width / width);
                int yCoord = Mathf.RoundToInt(y * (float)gradientTex.height / height);
                // �ؽ�ó���� ������ ������ �׷��� �����Ϸ� �迭�� ����
                gradientMap[x, y] = gradientTex.GetPixel(xCoord, yCoord).grayscale;
                //Debug.Log(gradientMap[x, y]);
            }
        }
        return gradientMap;
    }

    public Color[,] DrawNoiseMap(float[,] noiseMap, float[,] gradientMap)
    {
        Color[,] color = new Color[worldSize, worldSize];
        for (int x = 0; x < worldSize; x++)
        {
            for (int y = 0; y < worldSize; y++)
            {
                float value = noiseMap[x, y] + gradientMap[x, y];
                // ������ �ʰ� �׶���Ʈ ���� ���� ���� 0~1 ������ ������ ��ȯ
                value = Mathf.InverseLerp(0, 2, value);
                // ��ȯ�� ���� �ش��ϴ� ������ �׷��� �����Ϸ� ����
                color[x, y] = Color.Lerp(Color.black, Color.white, value);
            }
        }

        return color;
    }
    // �ش� ��ǥ�� ������ ������ �� �ִ����� Ȯ���ϴ� �Լ��̴�.
    // ���ڰ� objTypeNumArr�� ������ ������ Ÿ�� ������ ���� �迭�̴�.
    public bool CheckPossibleSettingBuilding(int[] objTypeNumArr, int chunkX, int chunkY, int tileX, int tileY)
    {
        GameObject chunk = null;
        // �����Ϸ��� ���� ûũ ��ǥ�� ���� ûũ ������Ʈ�� ã�´�. 
        for (int x = 0; x < worldChunks.GetLength(0); x++)
        {
            for (int y = 0; y < worldChunks.GetLength(1); y++)
            { 
                if ((int)worldChunks[x,y].transform.position.x / 20 == chunkX &&
                    (int)worldChunks[x,y].transform.position.z / 20 == chunkY)
				{
                    chunk = worldChunks[x, y];
                    break;
				}
            }
            if (chunk != null) { break; }
        }
        // ûũ ������Ʈ�� ã�����ϸ� ��������(false)�� ��ȯ�Ѵ�.
        if (chunk == null) { return false; }

        int cnt = 0;
        // ������ ������ Ÿ������ Ȯ���Ѵ�.
        for (int i = 0; i < objTypeNumArr.Length; i++)
        {
            if (!isSnow)
            {// ���� ������ Ÿ���� ������ �ϳ��� ���ذ���.
                if (chunk.transform.GetChild(tileX * chunkSize + tileY).GetComponent<TileInfo>().TileNum == objTypeNumArr[i])
                {
                    // ���������� Ÿ�ϰ� ��ġ�ϸ� cnt�� 1�� ���Ѵ�.
                    cnt++;
                }
            }
            else
			{
                if (chunk.transform.GetChild(tileX * chunkSize + tileY).GetComponent<TileInfo>().TileNum == objTypeNumArr[i])
                {
                    // ���������� Ÿ�ϰ� ��ġ�ϸ� cnt�� 1�� ���Ѵ�.
                    cnt++;
                }
            }

        }
        // ���������� Ÿ�ϰ� ��ġ�ϸ� cnt�� 1�� �ǰ�, ����������(true)�� ��ȯ�Ѵ�.
        if (cnt > 0) { return true; }

        // �� �ܿ� ��������(false)�� ��ȯ�Ѵ�.
        return false;
    }
}
/*
�ǹ�Ŭ���ϸ� �ڿ� ����ִ��� �����ִ� UI �߰�

�ǹ�����߰�(�ذ�)
�ùΰ����� : ���� 10��
������� : ���� 10��
��ɲ��� ���θ� : ���� 10��
�������� ���θ� : ���� 10��
������ �� : ���� 10��
�й� : �ķ� 2��
���尣 : ���� 10��, �� 10��
�ŷ��� : ���� 20��, ö���� 10��
���� : ���� 15��
��ī���� : ���� 20��, ö���� 10��
�� ������ : ���� 15��, �� 10��
�ʰ����� : ���� 15��, �� 10��
ġ����� : ���� 15��, �� 10��
â�� : ���� 10�� 

����ȭ��
*/

