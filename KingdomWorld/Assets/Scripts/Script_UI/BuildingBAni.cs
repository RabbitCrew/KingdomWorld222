using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBAni : MonoBehaviour
{
    public Image[] BImage = new Image[13];

    bool AniStart = false;

    int AniCount = 0;

    public int Speed = 20;

    private void Update()
    {
        if (AniStart == true)//anistart 체크되어있을 시 애니매이션 시작
        {
            BAniStart();
        }
    }

    public void BAniC()//처음 호출 시 모든 이미지 fillamount비우기 
    {// anistart 체크해줌
        for (int i = 0; i < BImage.Length; i++)
        {
            BImage[i].fillAmount = 0;
        }

        AniStart = true;
    }

    void BAniStart()//호출 시 배열에 있는 이미지 순서대로 speed값에 따라 fillamount 채워가도록 함.
    {
        switch (AniCount)
        {
            case 0:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 1:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 2:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 3:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 4:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 5:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 6:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 7:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 8:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 9:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 10:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 11:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 12:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount = 0;
                    AniStart = false;
                }
                break;
            default:
                break;
        }
    }

    public void BAniEnd() //애니메이션 끝나게 하는 함수
    {
        AniCount = 0;
        AniStart = false;
    }
}
