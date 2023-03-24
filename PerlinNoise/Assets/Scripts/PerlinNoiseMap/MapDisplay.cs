using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Range(0f, 1f)]
    public float[] fillPercents;
    public Color[] fillColors;
    
    public GameObject [] tileArr;
    public GameObject mother;

    public void DrawNoiseMap(float[,] noiseMap, float[,] gradientMap, bool useColorMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);
        Texture2D noiseTex = new Texture2D(width, height);
        noiseTex.filterMode = FilterMode.Point;
        Color[] colorMap = new Color[width * height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                colorMap[x * height + y] = CalcColor(noiseMap[x, y], gradientMap[x, y], useColorMap, x, y, width, height);

            }
        }
        //noiseTex.SetPixels(colorMap);
        //noiseTex.Apply();

        //spriteRenderer.sprite = Sprite.Create(noiseTex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
    }

    private Color CalcColor(float noiseValue, float gradientValue, bool useColorMap, int x, int y, int width, int height)
    {
        float value = noiseValue + gradientValue;
        //Debug.Log(noiseValue);
        // 노이즈 맵과 그라디언트 맵을 더한 값을 0~1 사이의 값으로 변환
        value = Mathf.InverseLerp(0, 2, value);
        // 변환된 값에 해당하는 색상을 그레이 스케일로 저장
        Color color = Color.Lerp(Color.black, Color.white, value);

        if (useColorMap)
        {
            for (int i = 0; i < fillPercents.Length; i++)
            {
                if (color.grayscale < fillPercents[i])
                {
                    // 미리 설정한 색상 범위에 따라 색상 변환
                    color = fillColors[i];
                    break;
                }
            }
        }

        if (color.grayscale < 0.125)
        {
            GameObject a= Instantiate(tileArr[0], mother.transform);
            a.transform.localPosition = new Vector2(x - width/2f, y - height/2f);
        }
        else if (color.grayscale < 0.25)
        {
            GameObject a = Instantiate(tileArr[1], mother.transform);
            a.transform.localPosition = new Vector2(x - width / 2f, y - height / 2f);

        }
        else if (color.grayscale < 0.375)
        {
            GameObject a = Instantiate(tileArr[2], mother.transform);
            a.transform.localPosition = new Vector2(x - width / 2f, y - height / 2f);

        }
        else if (color.grayscale < 0.5)
        {
            GameObject a = Instantiate(tileArr[3], mother.transform);
            a.transform.localPosition = new Vector2(x - width / 2f, y - height / 2f);

        }
        else if (color.grayscale < 0.625)
        {
            GameObject a = Instantiate(tileArr[4], mother.transform);
            a.transform.localPosition = new Vector2(x - width / 2f, y - height / 2f);

        }
        else if (color.grayscale < 0.75)
        {
            GameObject a = Instantiate(tileArr[5], mother.transform);
            a.transform.localPosition = new Vector2(x - width / 2f, y - height / 2f);

        }
        else if (color.grayscale < 0.875)
        {
            GameObject a = Instantiate(tileArr[6], mother.transform);
            a.transform.localPosition = new Vector2(x - width / 2f, y - height / 2f);

        }
        else
        {
            GameObject a = Instantiate(tileArr[7], mother.transform);
            a.transform.localPosition = new Vector2(x - width / 2f, y - height / 2f);

        }

        return color;
    }    
}
