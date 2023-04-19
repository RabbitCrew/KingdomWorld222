using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactWork : MonoBehaviour
{
    public Artifacts artifact;
    public AnExchange anExchange;

    public Toggle ArtifactActiveState;

    int ArtifactNum;

    float HPWaterTime = 180f;
    float DefaultHPWaterTime = 180f;

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
                    if (Inventory.instance.HasArtifact[value] <= 2)
                    {
                        GameManager.instance.timeSpeed = 2 * Inventory.instance.HasArtifact[value];
                    }
                    else if (Inventory.instance.HasArtifact[value] > 2)
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
                        GameManager.instance.DayTime *= (Inventory.instance.HasArtifact[value] * (5 / 100) + 1);
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
                // 네고 확률 증가
                if (anExchange.IsOpen == true)
                {
                    if (ArtifactActiveState.isOn == true)
                    {
                        Inventory.instance.MaxNegoText = 6 + Inventory.instance.HasArtifact[value];
                    }
                    else
                    {
                        Inventory.instance.MaxNegoText = 6;
                    }
                }
                break;
            case 3:
                // 환율 증가
                if(ArtifactActiveState.isOn == true)
                {
                    Inventory.instance.AddExchangeRate += (10 + (value - 1) * 5) / 100;
                }
                else
                {
                    Inventory.instance.AddExchangeRate = 0;
                }
                break;
            case 4:
                //hp 증가 유물 // 1회용 // 쿨타임 하루
                if(ArtifactActiveState.isOn == true)
                {
                    for(int i = 0; i < GameManager.instance.AllHuman.Count; i++)
                    {
                        GameManager.instance.AllHuman[i].GetComponent<NPC>().HP *= 150 / 100;
                    }

                    Inventory.instance.HasArtifact[value] -= 1;

                    HPWaterTime -= Time.deltaTime;

                    ArtifactActiveState.interactable = false;

                    if (HPWaterTime <= 0)
                    {
                        ArtifactActiveState.interactable = true;
                       
                        HPWaterTime = DefaultHPWaterTime;
                        ArtifactActiveState.isOn = false;
                    }
                }
                else
                {
                    for (int i = 0; i < GameManager.instance.AllHuman.Count; i++)
                    {
                        GameManager.instance.AllHuman[i].GetComponent<NPC>().HP *= 100 / 150;
                    }
                }
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
