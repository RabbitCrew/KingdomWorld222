using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactWork : MonoBehaviour
{
    public Artifacts artifact;
    public SVScrollBttm SVScrollBtn;
    public AnExchange anExchange;
    public Toggle ArtifactActiveState;

    int ArtifactNum;

    private void Update()
    {
        ArtifactEffect(ArtifactNum);
    }

    public void GetArtifactNum(int value)
    {
        ArtifactNum = value;
    }

    void ArtifactEffect(int value)
    {
        switch (value)
        {
            case 0:
                // 시계
                if (ArtifactActiveState.isOn == true)
                {
                    if (artifact.HasArtifact[value] <= 2)
                    {
                        GameManager.instance.timeSpeed = 2 * artifact.HasArtifact[value];
                    }
                    else if (artifact.HasArtifact[value] > 2)
                    {
                        GameManager.instance.timeSpeed = 4;
                    }
                }
                else
                {
                    GameManager.instance.timeSpeed = 0;
                }
                break;
            case 1:
                // 백야
                if (ArtifactActiveState.isOn == true)
                {
                    if (GameManager.instance.DayTime < 1f)
                    {
                        GameManager.instance.DayTime *= (artifact.HasArtifact[value] * (5 / 100) + 1);
                    }
                    else if (GameManager.instance.DayTime >= 1f)
                    {
                        GameManager.instance.DayTime = 1f;
                    }
                }
                else
                {
                    GameManager.instance.DayTime = 2 / 3;
                }
                break;
            case 2:
                //네고 확률 증가
                if (anExchange.IsOpen == true)
                {
                    if (ArtifactActiveState.isOn == true)
                    {
                        SVScrollBtn.MaxNegoText = 6 + artifact.HasArtifact[value];
                    }
                    else
                    {
                        SVScrollBtn.MaxNegoText = 6;
                    }
                }
                break;
            case 3:
                //환율 증가
                if(ArtifactActiveState.isOn == true)
                {

                }
                else
                {

                }
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;
            case 20:
                break;
            case 21:
                break;
            default:
                break;
        }
    }
}
