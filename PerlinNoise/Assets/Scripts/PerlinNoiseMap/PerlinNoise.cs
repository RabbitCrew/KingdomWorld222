using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public float[,] GenerateMap(int width, int height, float scale, float octaves, float persistance, float lacunarity, float xOrg, float yOrg)
    {
        float[,] noiseMap = new float[width, height];
        scale = Mathf.Max(0.0001f, scale);
        float maxNoiseHeight = float.MinValue;  // �ִ� ���� ��� ���� ����
        float minNoiseHeight = float.MaxValue;  // �ּ� ���� ��� ���� ����
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float amplitude = 1;    // ����. �������� ���� ���õ� ��.
                float frequency = 1;    // ���Ľ�. �������� ���ݰ� ���õ� ��. ���ļ��� Ŀ������ ����� ��������
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)   // ��Ÿ�ΰ� �����Ҽ��� ���� ���ļ��� ���� ������ ����� ��ø��
                {
                    float xCoord = xOrg + x / scale * frequency;
                    float yCoord = yOrg + y / scale * frequency;
                    // 0~1 ������ ���� ��ȯ�ϴ� �Լ�. 2�� ���ϰ� 1�� ���� -1 ~ 1 ������ ������ ��ȯ
                    float perlinValue = Mathf.PerlinNoise(xCoord, yCoord) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;
                    amplitude *= persistance;
                    frequency *= lacunarity;
                    //Debug.Log(x + " " + scale);

                }
                if (noiseHeight > maxNoiseHeight) maxNoiseHeight = noiseHeight;
                else if (noiseHeight < minNoiseHeight) minNoiseHeight = noiseHeight;
                noiseMap[x, y] = noiseHeight;
                //Debug.Log(noiseHeight);

            }
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // lerp�� ���Լ��� �ּڰ��� �ִ��� ���հ��� 3��° ���ڷ� ������ 0~1������ ���� ��ȯ
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
                //Debug.Log(maxNoiseHeight);
            }
        }
        return noiseMap;
    }
}
