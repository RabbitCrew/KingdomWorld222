using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseMapMaker : MonoBehaviour
{
    [SerializeField] private Texture2D gradientTex;
    [SerializeField] private Sprite[] tile;

    [SerializeField] private int chunkSize = 20;
    [SerializeField] private float zScreenSize;
    [SerializeField] private float xScreenSize;
    
    public int worldSize = 60;
    public float noiseFreq = 0.38f;
    public float seed;
    public Texture2D noiseTexture;
    public GameObject mother;

    public GameObject[,] worldChunks;

    private Transform cameraTrans;
    
	private void Awake()
	{
        // ĳ��~
        cameraTrans = Camera.main.transform;

        seed = Random.Range(-10000, 10000);
        zScreenSize = Camera.main.orthographicSize * 2;
        xScreenSize = zScreenSize * Camera.main.aspect;

        Debug.Log(zScreenSize + " " + xScreenSize);
    }

    void Start()
    {


        float[,] fl = GenerateNoiseTexture();
        // ûũ�� �����ϴ� �Լ�
        CreateChunks();
        GenerateTerrain(DrawNoiseMap(fl, GenerateMap(worldSize, worldSize)));
        mother.transform.eulerAngles = new Vector3(90,0,0);
    }
	private void Update()
	{
        RefreshChunks();
    }

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
                //newChunk.name = x.ToString() + " " + y.ToString();
                newChunk.transform.position = new Vector3(x * chunkSize, 0, y * chunkSize);
                newChunk.transform.parent = mother.transform;
                worldChunks[x,y] = newChunk;
            }
        }
	}

    // ��Ŭ���� �ø��� ���� �Լ�
    void RefreshChunks()
    {
        for (int x = 0; x < worldChunks.GetLength(0); x++)
        {
            for (int y = 0; y < worldChunks.GetLength(1); y++)
            {
                if (xScreenSize * 2f<
                    Vector3.Distance(
                        new Vector3((x * chunkSize) + chunkSize * 0.5f, 0, (y * chunkSize) + chunkSize * 0.5f),
                        new Vector3(cameraTrans.position.x, 0, cameraTrans.position.z)) ||
                    zScreenSize * 2f<
                    Vector3.Distance(
                        new Vector3((x * chunkSize) + chunkSize * 0.5f, 0, (y * chunkSize) + chunkSize * 0.5f),
                        new Vector3(cameraTrans.position.x, 0, cameraTrans.position.z)))
                {
                    worldChunks[x, y].SetActive(false);
					//               for (int i = 0; i < worldChunks[x,y].transform.childCount; i++)
					//{
					//                   worldChunks[x, y].transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
					//}
				}
				else
				{
                    worldChunks[x, y].SetActive(true);
                    //for (int i = 0; i < worldChunks[x, y].transform.childCount; i++)
                    //{
                    //    worldChunks[x, y].transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                    //}
                }
            }
        }
    }

    public void GenerateTerrain(Color[,] color)
    {
        for (int x = 0; x < worldSize; x++)
        {
            for (int y = 0; y < worldSize; y++)
            {
                GameObject newTile = new GameObject(name = "tile");

                // Round �Լ��� �ݿø��� ���� float�ڷ������� ��ȯ�ϴ� �Լ�.
                // RoundToInt �Լ��� �ݿø��� ���� int�ڷ������� ��ȯ�ϴ� �Լ�.
                float chunkCoordX = (Mathf.RoundToInt(x / chunkSize) * chunkSize);
                chunkCoordX /= chunkSize;
                float chunkCoordY = (Mathf.RoundToInt(y / chunkSize) * chunkSize);
                chunkCoordY /= chunkSize;
                newTile.transform.parent = worldChunks[(int)chunkCoordX, (int)chunkCoordY].transform;

                newTile.AddComponent<SpriteRenderer>();
                if (color[x,y].grayscale < 0.125)
                {
                    newTile.GetComponent<SpriteRenderer>().sprite = tile[0];
                }
                else if (color[x,y].grayscale < 0.25)
                {
                    newTile.GetComponent<SpriteRenderer>().sprite = tile[1];
                }
                else if (color[x,y].grayscale < 0.375)
                {
                    newTile.GetComponent<SpriteRenderer>().sprite = tile[2];
                }
                else if (color[x,y].grayscale < 0.5)
                {
                    newTile.GetComponent<SpriteRenderer>().sprite = tile[3];
                }
                else if (color[x,y].grayscale < 0.625)
                {
                    newTile.GetComponent<SpriteRenderer>().sprite = tile[4];
                }
                else if (color[x,y].grayscale < 0.75)
                {
                    newTile.GetComponent<SpriteRenderer>().sprite = tile[5];
                }
                else if (color[x,y].grayscale < 0.875)
                {
                    newTile.GetComponent<SpriteRenderer>().sprite = tile[6];
                }
                else
                {
                    newTile.GetComponent<SpriteRenderer>().sprite = tile[7];
                }
                newTile.transform.position = new Vector2(x, y);
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
                color[x,y] = Color.Lerp(Color.black, Color.white, value);
            }
		}

        return color;
	}
}
