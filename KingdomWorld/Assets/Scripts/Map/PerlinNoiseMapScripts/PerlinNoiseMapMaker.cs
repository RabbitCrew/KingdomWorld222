using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectNS;
public class PerlinNoiseMapMaker : MonoBehaviour
{
    [SerializeField] private Texture2D gradientTex;
    [SerializeField] private Sprite[] tile;
    [SerializeField] private Sprite[] snowTile;

    [SerializeField] private int chunkSize = 20;    // 청크 사이즈는 20 * 20
    [SerializeField] private int gradientSize = 1000;
    [SerializeField] private int worldSize = 80;
    [SerializeField] private float noiseFreq = 0.2f;
    [SerializeField] private SettingObject settingObject;

    struct point
    {
        public float x { get; }   // 생성자에서만 수정이 가능함. 읽기전용
        public float z { get; }   // 생성자에서만 수정이 가능함. 읽기전용

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
        // 캐싱~
        cameraTrans = Camera.main.transform;
        // 시드를 랜덤으로 구함. 시드 값에 따라 다른 노이즈가 생성
        seed = Random.Range(-10000, 10000);

        startChunkX = Random.Range(-13, 14);
        startChunkZ = Random.Range(-13, 14);
        isStartBuilding = false;
        isSnow = false;
    }

    void Start()
    {
        float[,] fl = GenerateNoiseTexture();
        // 청크를 생성하는 함수
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
    // 청크를 생성하는 함수
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
    // 오클루전 컬링을 위한 함수
    void RefreshChunks()
    {
        int cameraX = ((int)(cameraTrans.position.x) / chunkSize);
        int cameraZ = ((int)(cameraTrans.position.z) / chunkSize);
        // 카메라 위치에 따른 보여줘야할 청크 좌표 범위를 정해준다.
        int cameraXMax = Mathf.RoundToInt(cameraX + 1.8f);
        int cameraXMin = Mathf.RoundToInt(cameraX - 1.8f);
        int cameraZMax = Mathf.RoundToInt(cameraZ + 1.8f);
        int cameraZMin = Mathf.RoundToInt(cameraZ - 1.8f);


        // 메인 카메라 범위 밖에 있는 맵은 오브젝트를 꺼준다. 
        for (int x = 0; x < worldChunks.GetLength(0); x++)
        {
            for (int y = 0; y < worldChunks.GetLength(1); y++)
            {
                // 카메라 너머에 있는 청크들은 비활성화 시킨다.
                if (((int)worldChunks[x, y].transform.localPosition.x / chunkSize >= cameraXMax ||
                    (int)worldChunks[x, y].transform.localPosition.x / chunkSize < cameraXMin ||
                    (int)worldChunks[x, y].transform.localPosition.y / chunkSize >= cameraZMax ||
                    (int)worldChunks[x, y].transform.localPosition.y / chunkSize < cameraZMin))
                {
                    // 비활성화 시켜줄 청크가 비활성화 청크 리스트 (falseChunksList)에 있지 않아야 한다. 아직은 켜져있기 때문
                    if (falseChunksList.FindIndex(a => a.Equals(worldChunks[x, y])) == -1)
                    {
                        // 해당 청크를 비활성화 시킨다.
                        worldChunks[x, y].SetActive(false);
                        // 비활성화 청크 리스트(falseChunkList)에 추가한다.
                        falseChunksList.Add(worldChunks[x, y]);
                        // 청크 좌표 리스트(pointList)에 비활성화시킨 청크의 좌표값이 저장되어있는지 인덱스 값을 구한다.
                        int n = pointList.FindIndex(a => a.x == worldChunks[x, y].transform.localPosition.x && a.z == worldChunks[x, y].transform.localPosition.y);
                        // 만약 좌표값이 저장되어 있을 경우 그 값을 리스트에서 삭제한다.
                        if (n != -1) { pointList.RemoveAt(n); }
                        // 오브젝트의 스프라이트 렌더러를 꺼준다.
                        int chunkX = (int)worldChunks[x, y].transform.localPosition.x / 20;
                        int chunkY = (int)worldChunks[x, y].transform.localPosition.y / 20;
                        settingObject.DisableSpriteRenderer(chunkX, chunkY);
                    }
                }
                // 청크가 필요한 자리에 이미 활성화하고 있지만 pointList에 좌표값이 저장되어있지 않을 때
                else if (pointList.FindIndex(a => a.x == worldChunks[x, y].transform.localPosition.x && a.z == worldChunks[x, y].transform.localPosition.y) == -1 &&
                         pointList.Count + falseChunksList.Count < worldChunks.GetLength(0) * worldChunks.GetLength(1))
                {
                    //청크 좌표에 맞춰 맵을 재생성한다.
                    RefreshTexture(worldChunks[x, y], worldChunks[x, y].transform.localPosition.x, worldChunks[x, y].transform.localPosition.y);
                    //청크 좌표를 청크 좌표 리스트(pointList)에 추가한다.
                    pointList.Add(new point(worldChunks[x, y].transform.localPosition.x, worldChunks[x, y].transform.localPosition.y));

                }
            }
        }
        //현재 메인 카메라를 기준으로 화면상 비어있는 곳에 비활성화된 청크를 넣고 텍스처를 새로고침한다.
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
                    // 오브젝트의 스프라이트 렌더러를 켜준다.
                    settingObject.EnableSpriteRenderer(x, z);
                }
            }
        }

    }
    // 타일을 생성해서 청크로 묶는다.
    public void GenerateTerrain()
    {
        for (int x = 0; x < worldSize; x++)
        {
            for (int y = 0; y < worldSize; y++)
            {
                GameObject newTile = new GameObject(name = "tile");

                // Round 함수는 반올림한 값을 float자료형으로 반환하는 함수.
                // RoundToInt 함수는 반올림한 값을 int자료형으로 반환하는 함수.
                // 청크 좌표를 구함
                float chunkCoordX = (Mathf.RoundToInt(x / chunkSize) * chunkSize);
                chunkCoordX /= chunkSize;
                float chunkCoordY = (Mathf.RoundToInt(y / chunkSize) * chunkSize);
                chunkCoordY /= chunkSize;
                // 생성한 타일 오브젝트를 청크 좌표에 맞춰 부모 오브젝트로 지정. 
                newTile.transform.parent = worldChunks[(int)chunkCoordX, (int)chunkCoordY].transform;
                // 컴포넌트들을 넣어준다.
                newTile.AddComponent<SpriteRenderer>();
                newTile.AddComponent<BoxCollider>();
                newTile.AddComponent<TileColorChange>();
                newTile.AddComponent<TileInfo>();
                // 후에 타일 오브젝트 내에서 SettingObject 클래스의 함수를 호출하기 위함.
                newTile.GetComponent<TileInfo>().InitSettingObject(settingObject);
                newTile.GetComponent<BoxCollider>().isTrigger = true;
                // 타일 오브젝트의 위치를 지정함.
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
                // PerlinNoise는 0~1 사이의 값을 반환하는 함수.
                float v = Mathf.PerlinNoise((x + seed) * noiseFreq, (y + seed) * noiseFreq);
                noiseTexture.SetPixel(x, y, new Color(v, v, v));
                noiseMap[x, y] = v;
                //Debug.Log(v);
            }
        }

        noiseTexture.Apply();

        return noiseMap;
    }

    // 텍스쳐를 맵 위치에 맞게 다시 바꿔준다.
    private void RefreshTexture(GameObject chunk, float pointX, float pointY)
    {
        float[,] noiseMap = new float[chunkSize, chunkSize];
        float[,] gradientMap = new float[chunkSize, chunkSize];
        Color[,] color = new Color[chunkSize, chunkSize];

        int chunkX = (int)chunk.transform.localPosition.x / 20;
        int chunkY = (int)chunk.transform.localPosition.y / 20;
        // 해당 청크에서 오브젝트가 한번도 생성된 적이 없으면 생성시킴
        bool isObject = settingObject.ActiveTrueObjectPointList(chunkX, chunkY);


        // 청크 한개 사이즈만큼 반복
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                // 지정된 좌표에서의 펄린노이즈 값을 가져온다. 펄린 노이즈값은 0~1 사이의 값이다.
                // seed는 awake에서 딱 한번 설정되므로 펄린 노이즈 값이 바뀌지 않는다.
                float v = Mathf.PerlinNoise((x + pointX + seed) * noiseFreq, (y + pointY + seed) * noiseFreq);
                noiseMap[x, y] = v;
                //x와 y는 0~19사이의 값. 거기에 pointX,Y값은 청크 좌표의 위치값
                //그리고 gradientSize는 그라디언트 텍스쳐에서 좌표값(gradientSize, gradientSize) 이므로 0.5f를 각각 곱해서 (0,0)좌표값을 텍스처의 중앙으로 맞춤
                //그러면 텍스처의 중앙에서 (x + pointX, y + pointY)값을 더하게 되는 셈.
                //거기에 실제 텍스처의 가로 세로 사이즈를 곱해주고, 이를 gradientSize로 만큼 분할해준다.(나눈다.)
                //지정된 좌표값에 따른 그라디언트의 좌표값이 xCoord, yCoord값에 저장된다.
                int xCoord = Mathf.RoundToInt((x + pointX + gradientSize * 0.5f) * (float)gradientTex.width / gradientSize);
                int yCoord = Mathf.RoundToInt((y + pointY + gradientSize * 0.5f) * (float)gradientTex.height / gradientSize);
                //Debug.Log(xCoord + " " + yCoord);
                //그라디언트 텍스처에서 좌표값을 통하여 그레이스케일 값을 구하고 이를 배열에 저장한다. 이 값은 0에서 1사이 값을 가진다.
                gradientMap[x, y] = gradientTex.GetPixel(xCoord, yCoord).grayscale;
                //일정 값 아래는 0, 일정값 위에는 0.75로 고정한다.
                if (gradientMap[x, y] <= 0.3f) { gradientMap[x, y] = 0f; }
                else if (gradientMap[x, y] >= 0.75f) { gradientMap[x, y] = 0.75f; }
                // 펄린노이즈 값과 그라디언트 값을 합친다.
                float value = noiseMap[x, y] + gradientMap[x, y];
                // 두 값의 합은 0에서 2사이 값을 지니게 된다.(일정값 위에는 0.75로 고정했지만 이는 무시)
                // InverseLerp(a,b,c)는 a에서 b로 가는 c의 값을 퍼센트로 환산하여 0~1사이의 값으로 변환시켜서 반환한다. 즉 0~1사이의 값을 반환한다.
                value = Mathf.InverseLerp(0, 2, value);
                // Color.Lerp에 value 값을 사용하여 검정,각종 회색들, 흰색 값을 지정한다. 
                color[x, y] = Color.Lerp(Color.black, Color.white, value);
                // color 배열의 그레이스케일에 따라 타일의 스프라이트를 다시 설정한다.
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

        // 생성해야될 오브젝트의 좌표를 구하고 저장한다.
        // 해당 청크에서 오브젝트가 한번도 생성된 적이 없으면 생성시킴
        if (!isObject)
        {
            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    int ran = Random.Range(0, 700);

                    // 동굴 생성 확률
                    if (ran < 1)
                    {
                        if (settingObject.CheckMineRange(chunkX, chunkY, chunk, x, y, tile[(int)TileNum.OCEAN], chunkSize) &&
                            settingObject.CheckMineRange(chunkX, chunkY, chunk, x, y, tile[(int)TileNum.RIVER], chunkSize) &&
                            settingObject.CheckMineRange(chunkX, chunkY, chunk, x, y, tile[(int)TileNum.STONE], chunkSize))
                        {
                            settingObject.AddObjectPointList(chunkX, chunkY, (int)ObjectTypeNum.MINE, x, y);
                        }
                    }
                    // 나무 생성 확률
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
        //지정된 좌표에 오브젝트를 생성한다.
        // 해당 청크에서 오브젝트가 한번도 생성된 적이 없으면 생성시킴
        if (!isObject) { settingObject.CreateObejct(chunkX, chunkY); }
        // 위 과정을 통해 오브젝트가 확정적으로 생성되었으므로 isObject는 무조건 true(없어도 되는 코드인듯?)
        isObject = true;
    }

    public float[,] GenerateMap(int width, int height)
    {
        float[,] gradientMap = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // 텍스처 값과 크기 값에 맞춰 좌표 저장
                int xCoord = Mathf.RoundToInt(x * (float)gradientTex.width / width);
                int yCoord = Mathf.RoundToInt(y * (float)gradientTex.height / height);
                // 텍스처에서 색상을 가져와 그레이 스케일로 배열에 저장
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
                // 노이즈 맵과 그라디언트 맵을 더한 값을 0~1 사이의 값으로 변환
                value = Mathf.InverseLerp(0, 2, value);
                // 변환된 값에 해당하는 색상을 그레이 스케일로 저장
                color[x, y] = Color.Lerp(Color.black, Color.white, value);
            }
        }

        return color;
    }
    // 해당 좌표에 빌딩을 생성할 수 있는지를 확인하는 함수이다.
    // 인자값 objTypeNumArr은 생성이 가능한 타일 종류를 담은 배열이다.
    public bool CheckPossibleSettingBuilding(int[] objTypeNumArr, int chunkX, int chunkY, int tileX, int tileY)
    {
        GameObject chunk = null;
        // 생성하려는 곳의 청크 좌표를 지닌 청크 오브젝트를 찾는다. 
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
        // 청크 오브젝트를 찾지못하면 생성못함(false)을 반환한다.
        if (chunk == null) { return false; }

        int cnt = 0;
        // 생성이 가능한 타일인지 확인한다.
        for (int i = 0; i < objTypeNumArr.Length; i++)
        {
            // 생성 가능한 타일의 종류와 하나씩 비교해간다.
            if (chunk.transform.GetChild(tileX * chunkSize + tileY).GetComponent<SpriteRenderer>().sprite == tile[objTypeNumArr[i]])
			{           
                // 생성가능한 타일과 일치하면 cnt에 1을 더한다.
                cnt++;
			}
        }
        // 생성가능한 타일과 일치하면 cnt는 1이 되고, 생성가능함(true)을 반환한다.
        if (cnt > 0) { return true; }

        // 그 외엔 생성못함(false)을 반환한다.
        return false;
    }
}
