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
    int DefaultRainRate;
    List<float> ChildList = new List<float>();
    float ChildCool = 3f;
    float DefaultChildCool = 3f;
    float ChildRate;
    float DefaultChildRate;

    float HPWaterTime = 180f;
    float DefaultHPWaterTime = 180f;

    bool TeruTeruBous = false;

    [SerializeField] GameObject CitizenPrefab;
    [SerializeField] GameObject NPCMother;

    private void Update()
    {
        ArtifactEffect(ArtifactNum);

        GrowingChild();
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
                    if (Inventory.instance.HasArtifact[value] <= 2 && Inventory.instance.HasArtifact[value] > 0)
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
                    GameManager.instance.timeSpeed = 1;
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
                if (ArtifactActiveState.isOn == true)
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
                if (ArtifactActiveState.isOn == true)
                {
                    for (int i = 0; i < GameManager.instance.AllHuman.Count; i++)
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
                //비다 비!!
                if (ArtifactActiveState.isOn == true)
                {
                    DefaultRainRate = Inventory.instance.RainRate;

                    Inventory.instance.RainRate *= (5 + (Inventory.instance.HasArtifact[value] - 1) * 2) / 100;

                    GameManager.instance.Food -= 5 * Inventory.instance.HasArtifact[value];

                    TeruTeruBous = true;

                    ArtifactActiveState.isOn = false;

                    ArtifactActiveState.interactable = false;
                }

                if (GameManager.instance.isRain == true)
                {
                    if (TeruTeruBous == true)
                    {
                        Inventory.instance.RainRate = DefaultRainRate;

                        ArtifactActiveState.interactable = true;
                        TeruTeruBous = false;
                    }
                }
                break;
            case 6:
                //애새끼 함 만들어보자... 연성이다!!!!!!!
                if (ArtifactActiveState.isOn == true)
                {
                    ChildCool -= Time.deltaTime;

                    if (ChildCool <= 0)
                    {
                        ChildRate = Random.Range(DefaultChildRate, 1000);

                        ChildCool = DefaultChildCool;
                    }

                    if(ChildRate >= 0 && ChildRate <= 8 * Inventory.instance.HasArtifact[value])
                    {
                        ChildList[Inventory.instance.Children] = 180 * 30; //자라는 시간 설정

                        Inventory.instance.Children++;
                    }
                }
                else
                {
                    ChildCool = DefaultChildCool;
                }
                break;
            case 7:
                //애낳을 확률 증가
                if(ArtifactActiveState.isOn == true)
                {
                    DefaultChildRate += Inventory.instance.HasArtifact[value];
                }
                else
                {
                    DefaultChildRate = 0;
                }
                break;
            case 8:
                //시민 스폰 속도 증가. // 20개 넘기면 적용 안됨.
                if(ArtifactActiveState.isOn == true)
                {
                    Inventory.instance.SpawnTime += Inventory.instance.SpawnTime * ((5 * Inventory.instance.HasArtifact[value]) / 100);
                }
                else
                {
                    Inventory.instance.SpawnTime -= Inventory.instance.SpawnTime * ((5 * Inventory.instance.HasArtifact[value]) / 100);
                }
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
            case 22:
                break;
            case 23:
                break;
            case 24:
                break;
            case 25:
                break;
            case 26:
                break;
            case 27:
                break;
            case 28:
                break;
            case 29:
                break;
            case 30:
                break;
            case 31:
                break;
            case 32:
                break;
            default:
                break;
        }
    }

    int Count = 0;

    void GrowingChild()
    {
        GameManager.instance.Food -= Inventory.instance.Children * 80 / 100;

        for (int i = 0; i < ChildList.Count; i++)
        {
            ChildList[i] -= Time.deltaTime;

            if (ChildList[i] <= 0) // 애 다 성장하면.
            {
                ChildList.RemoveAt(i);

                Inventory.instance.Children--;

                GameObject CSpawn = Instantiate(CitizenPrefab);//시민 스폰시켜주기~
                CSpawn.transform.parent = NPCMother.transform;
                if (Inventory.instance.houseDic.ContainsKey(i))
                {
                    CSpawn.transform.position = Inventory.instance.houseDic[i].transform.position;
                    CSpawn.GetComponent<NPC>().HouseTr = Inventory.instance.houseDic[i].transform;
                }

                Count = RandomSprite();

                CSpawn.GetComponent<SpriteRenderer>().sprite = Inventory.instance.CtSpriteList[Count];

                GameManager.instance.AddAllHumanList(CSpawn);

                CSpawn.SendMessage("SetPAni", Count);

                GameManager.instance.RestHuman.Add(CSpawn);
            }
        }
    }

    public int RandomSprite() //일반 시민 스프라이트 랜덤 지정
    {
        Count = Random.Range(0, Inventory.instance.CtSpriteList.Length);
        //Debug.Log(Count);
        return Count;
    }
}