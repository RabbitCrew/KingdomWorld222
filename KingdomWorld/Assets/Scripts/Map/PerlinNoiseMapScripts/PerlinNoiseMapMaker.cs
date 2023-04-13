using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectNS;
public class PerlinNoiseMapMaker : MonoBehaviour
{
    [SerializeField] private Texture2D gradientTex;
    [SerializeField] private Sprite[] tile;
    [SerializeField] private Sprite[] snowTile;

    [SerializeField] private int chunkSize = 20;    // ûũ ������� 20 * 20
    [SerializeField] private int gradientSize = 1000;
    [SerializeField] private int worldSize = 80;
    [SerializeField] private float noiseFreq = 0.2f;
    [SerializeField] private SettingObject settingObject;

    struct point
    {
        public float x { get; }   // �����ڿ����� ������ ������. �б�����
        public float z { get; }   // �����ڿ����� ������ ������. �б�����

        public point(float x, float z)
        {
            this.x = x;
            this.z = z;
        }
    }

    public float seed;
    public Texture2D noiseTexture;
    public GameObject mother;
    public GameObject[,] worldChunks;

    private Transform cameraTrans;
    private List<point> pointList = new List<point>();
    private List<GameObject> falseChunksList = new List<GameObject>();
    private int startChunkX;
    private int startChunkZ;
    private bool isStartBuilding;
    private bool isSnow;
    private void Awake()
    {
        // ĳ��~
        cameraTrans = Camera.main.transform;
        // �õ带 �������� ����. �õ� ���� ���� �ٸ� ����� ����
        seed = Random.Range(-10000, 10000);

        startChunkX = Random.Range(-13, 14);
        startChunkZ = Random.Range(-13, 14);
        isStartBuilding = false;
        isSnow = false;
    }

    void Start()
    {
        float[,] fl = GenerateNoiseTexture();
        // ûũ�� �����ϴ� �Լ�
        CreateChunks();
        DrawNoiseMap(fl, GenerateMap(worldSize, worldSize));
        GenerateTerrain();
        mother.transform.eulerAngles = new Vector3(90, 0, 0);

        cameraTrans.position = new Vector3(startChunkX * 20f, 40f, startChunkZ * 20f);

        RefreshChunks();

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
                    pointList.Add(new point(worldChunks[x, y].transform.localPosition.x, worldChunks[x, y].transform.localPosition.y));

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
                    pointList.Add(new point(falseChunksList[0].transform.localPosition.x, falseChunksList[0].transform.localPosition.y));
                    falseChunksList.RemoveAt(0);
                    // ������Ʈ�� ��������Ʈ �������� ���ش�.
                    settingObject.EnableSpriteRenderer(x, z);
                }
            }
        }

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
                newTile.AddComponent<TileColorChange>();
                newTile.AddComponent<TileInfo>();
                // �Ŀ� Ÿ�� ������Ʈ ������ SettingObject Ŭ������ �Լ��� ȣ���ϱ� ����.
                newTile.GetComponent<TileInfo>().InitSettingObject(settingObject);
                newTile.GetComponent<BoxCollider>().isTrigger = true;
                // Ÿ�� ������Ʈ�� ��ġ�� ������.
                newTile.transform.localPosition = new Vector3(x % chunkSize, y % chunkSize);
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
                // ������ ��ǥ������ �޸������� ���� �����´�. �޸� ������� 0~1 ������ ���̴�.
                // seed�� awake���� �� �ѹ� �����ǹǷ� �޸� ������ ���� �ٲ��� �ʴ´�.
                float v = Mathf.PerlinNoise((x + pointX + seed) * noiseFreq, (y + pointY + seed) * noiseFreq);
                noiseMap[x, y] = v;
                //x�� y�� 0~19������ ��. �ű⿡ pointX,Y���� ûũ ��ǥ�� ��ġ��
                //�׸��� gradientSize�� �׶���Ʈ �ؽ��Ŀ��� ��ǥ��(gradientSize, gradientSize) �̹Ƿ� 0.5f�� ���� ���ؼ� (0,0)��ǥ���� �ؽ�ó�� �߾����� ����
                //�׷��� �ؽ�ó�� �߾ӿ��� (x + pointX, y + pointY)���� ���ϰ� �Ǵ� ��.
                //�ű⿡ ���� �ؽ�ó�� ���� ���� ����� �����ְ�, �̸� gradientSize�� ��ŭ �������ش�.(������.)
                //������ ��ǥ���� ���� �׶���Ʈ�� ��ǥ���� xCoord, yCoord���� ����ȴ�.
                int xCoord = Mathf.RoundToInt((x + pointX + gradientSize * 0.5f) * (float)gradientTex.width / gradientSize);
                int yCoord = Mathf.RoundToInt((y + pointY + gradientSize * 0.5f) * (float)gradientTex.height / gradientSize);
                //Debug.Log(xCoord + " " + yCoord);
                //�׶���Ʈ �ؽ�ó���� ��ǥ���� ���Ͽ� �׷��̽����� ���� ���ϰ� �̸� �迭�� �����Ѵ�. �� ���� 0���� 1���� ���� ������.
                gradientMap[x, y] = gradientTex.GetPixel(xCoord, yCoord).grayscale;
                //���� �� �Ʒ��� 0, ������ ������ 0.75�� �����Ѵ�.
                if (gradientMap[x, y] <= 0.3f) { gradientMap[x, y] = 0f; }
                else if (gradientMap[x, y] >= 0.75f) { gradientMap[x, y] = 0.75f; }
                // �޸������� ���� �׶���Ʈ ���� ��ģ��.
                float value = noiseMap[x, y] + gradientMap[x, y];
                // �� ���� ���� 0���� 2���� ���� ���ϰ� �ȴ�.(������ ������ 0.75�� ���������� �̴� ����)
                // InverseLerp(a,b,c)�� a���� b�� ���� c�� ���� �ۼ�Ʈ�� ȯ���Ͽ� 0~1������ ������ ��ȯ���Ѽ� ��ȯ�Ѵ�. �� 0~1������ ���� ��ȯ�Ѵ�.
                value = Mathf.InverseLerp(0, 2, value);
                // Color.Lerp�� value ���� ����Ͽ� ����,���� ȸ����, ��� ���� �����Ѵ�. 
                color[x, y] = Color.Lerp(Color.black, Color.white, value);
                // color �迭�� �׷��̽����Ͽ� ���� Ÿ���� ��������Ʈ�� �ٽ� �����Ѵ�.
                if (color[x, y].grayscale < 0.2)
                {
                    if (!isSnow)
                    {
                        chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = tile[(int)TileNum.OCEAN];
                    }
					else
					{
                        chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = snowTile[(int)TileNum.OCEAN];
                    }
                    chunk.transform.GetChild(x * chunkSize + y).tag = "NotWalkable";
                }
                else if (color[x, y].grayscale < 0.375)
                {
                    if (!isSnow)
                    {
                        chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = tile[(int)TileNum.RIVER];
                    }
                    else
                    {
                        chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = snowTile[(int)TileNum.RIVER];
                    }
                    chunk.transform.GetChild(x * chunkSize + y).tag = "NotWalkable";
                }
                else if (color[x, y].grayscale < 0.475)
                {
                    if (!isSnow)
                    {
                        chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = tile[(int)TileNum.GLASS];
                    }
                    else
					{
                        chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = snowTile[(int)TileNum.GLASS];
                    }
                    chunk.transform.GetChild(x * chunkSize + y).tag = "Walkable";
                }
                else if (color[x, y].grayscale < 0.6)
                {
                    if (!isSnow)
                    {
                        chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = tile[(int)TileNum.BUMPYTILE];
                    }
                    else
					{
                        chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = snowTile[(int)TileNum.BUMPYTILE];
                    }
                    chunk.transform.GetChild(x * chunkSize + y).tag = "Walkable";
                }
                else if (color[x, y].grayscale < 0.785)
                {
                    if (!isSnow)
                    {
                        chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = tile[(int)TileNum.FLATTILE];
                    }
                    else
					{
                        chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = snowTile[(int)TileNum.FLATTILE];
                    }
                    chunk.transform.GetChild(x * chunkSize + y).tag = "Walkable";
                }
                else
                {
                    if (!isSnow)
                    {
                        chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = tile[(int)TileNum.STONE];
                    }
                    else
                    {
                        chunk.transform.GetChild(x * chunkSize + y).GetComponent<SpriteRenderer>().sprite = snowTile[(int)TileNum.STONE];
                    }
                    chunk.transform.GetChild(x * chunkSize + y).tag = "NotWalkable";
                }

            }
        }

        if (!isStartBuilding)
		{
            for (int x = 0; x < chunkSize; x++)
			{
                for (int y = 0; y < chunkSize; y++)
				{
                    if (settingObject.CheckStartBuildingRange(chunkX, chunkY, chunk, x, y, tile[(int)TileNum.OCEAN], chunkSize) &&
                        settingObject.CheckStartBuildingRange(chunkX, chunkY, chunk, x, y, tile[(int)TileNum.RIVER], chunkSize) &&
                        settingObject.CheckStartBuildingRange(chunkX, chunkY, chunk, x, y, tile[(int)TileNum.STONE], chunkSize))
                    {
                        settingObject.AddTilePoint3(chunkX, chunkY, (int)ObjectTypeNum.HOUSE, x-2, y);
                        settingObject.AddTilePoint3(chunkX, chunkY, (int)ObjectTypeNum.CARPENTERHOUSE, x + 1, y);
                        isStartBuilding = true;
                        cameraTrans.position = new Vector3(pointX + x, 40, pointY + y);
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
                        if (settingObject.CheckMineRange(chunkX, chunkY, chunk, x, y, tile[(int)TileNum.OCEAN], chunkSize) &&
                            settingObject.CheckMineRange(chunkX, chunkY, chunk, x, y, tile[(int)TileNum.RIVER], chunkSize) &&
                            settingObject.CheckMineRange(chunkX, chunkY, chunk, x, y, tile[(int)TileNum.STONE], chunkSize))
                        {
                            settingObject.AddObjectPointList(chunkX, chunkY, (int)ObjectTypeNum.MINE, x, y);
                        }
                    }
                    // ���� ���� Ȯ��
                    else if (ran < 80)
                    {
                        if (settingObject.CheckTreeRange(chunkX, chunkY, chunk, x, y, tile[(int)TileNum.OCEAN], chunkSize) &&
                            settingObject.CheckTreeRange(chunkX, chunkY, chunk, x, y, tile[(int)TileNum.RIVER], chunkSize) &&
                            settingObject.CheckTreeRange(chunkX, chunkY, chunk, x, y, tile[(int)TileNum.STONE], chunkSize))
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
            // ���� ������ Ÿ���� ������ �ϳ��� ���ذ���.
            if (chunk.transform.GetChild(tileX * chunkSize + tileY).GetComponent<SpriteRenderer>().sprite == tile[objTypeNumArr[i]])
			{           
                // ���������� Ÿ�ϰ� ��ġ�ϸ� cnt�� 1�� ���Ѵ�.
                cnt++;
			}
        }
        // ���������� Ÿ�ϰ� ��ġ�ϸ� cnt�� 1�� �ǰ�, ����������(true)�� ��ȯ�Ѵ�.
        if (cnt > 0) { return true; }

        // �� �ܿ� ��������(false)�� ��ȯ�Ѵ�.
        return false;
    }
}
