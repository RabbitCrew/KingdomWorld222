using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Setgrid
{
    //밀, 식량, 나무, 육류, 가죽, 금화, 철광석, 주조철, 소, 양, 우유, 치즈, 햄, 양털, 옷감, 바위

    private int wheat;
    private int food;
    private int wood;
    private int meat;
    private int leather;
    private int gold;
    private int itronstone;
    private int castIron;
    private int cow;
    private int sheep;
    private int milk;
    private int cheese;
    private int ham;
    private int fleece;
    private int cloth;
    private int stone;
    public int MaxResource; //자원 최대치
    public List<GameObject> AllHuman = new List<GameObject>();

    public int Stone
    {
        get { return stone; }
        set { if(value >= MaxResource)
            {
                value = MaxResource;
            }
            stone = value;
        }
    }
    public int Milk
    {
        get { return milk; }
        set { if(value >= MaxResource)
            {
                value = MaxResource;
            }
            milk = value;
        }
    }
    public int Wheat {
        get { return wheat; } 
        set 
        { 
            if(Food < MaxResource)
            {
                Food += value;
                value = 0;
            }else if(Food >= MaxResource)
            {
                if (value >= MaxResource)
                {
                    value = MaxResource;
                }
            }
            wheat = value;
        }
    }
    public int Food
    {
        get { return food; }
        set
        {
            if (value >= MaxResource)
            {
                Debug.Log("Food 맥스리소스 초과");
                value = MaxResource;
            }
            else if(value < 0)//죽음의조건
            {
                while (value <= MaxResource)//food생산
                {
                    if(Wheat > 0)
                    {
                        Wheat -= 1;
                        value += 1;
                    }
                    else if (Meat > 0)
                    {
                        Meat -= 1;
                        value += 5;
                    }else if(Milk > 0)
                    {
                        Milk -= 1;
                        value += 2;
                    }
                    else if (Ham > 0)
                    {
                        Ham -= 1;
                        value += 20;
                    }
                    else if (Cheese > 0)
                    {
                        Cheese -= 1;
                        value += 20;
                    }
                    else if (Meat <= 0 && Ham <= 0 && Cheese <= 0 && Wheat <= 0 && Milk <= 0)
                    {
                        break;
                    }
                }
                if(value < 0)
                {
                    value *= -1;
                    for (int i = 0; i < value / 10; i++)
                    {
                        if (AllHuman.Count >= 1)
                        {
                            AllHuman[0].GetComponent<NPC>().HP = 0;
                            //AllHuman.RemoveAt(0);
                        }
                    }
                    value = 0;
                }
                else if (value >= 0 && value < 10)
                {
                    value = 10;
                    for (int i = 0; i < value / 10; i++)
                    {
                        if (AllHuman.Count >= 1)
                        {
                            AllHuman[0].GetComponent<NPC>().HP = 0;
                            //AllHuman.RemoveAt(0);
                        }
                    }
                    value = 0;
                }                
            }
            food = value;
        }
    }
    public int Wood
    {
        get { return wood; }
        set {
            if (value >= MaxResource)
            {
                value = MaxResource;
            }else if(value < 0)
            {
                value *= -1;
                if (value > 0 && value < 10)
                    value = 10;
                for (int i = 0; i < value / 10; i++)
                {
                    if (AllHuman.Count >= 1)
                    {
                        AllHuman[0].GetComponent<NPC>().HP = 0;
                        //AllHuman.RemoveAt(0);
                    }
                }
                value = 0;
            }
            wood = value;
        }
    }
    public int Meat
    {
        get { return meat; }
        set {
            if (value >= MaxResource)
            {
                value = MaxResource;
            }
            meat = value;
        }
    }
    public int Leather
    {
        get { return leather; }
        set {
            if (value >= MaxResource)
            {
                value = MaxResource;
            }
            leather = value;
        }
    }
    public int Gold
    {
        get { return gold; }
        set {
            if (value >= MaxResource)
            {
                value = MaxResource;
            }
            gold = value;
        }
    }
    public int Itronstone
    {
        get { return itronstone; }
        set {
            if (value >= MaxResource)
            {
                value = MaxResource;
            }
            itronstone = value;
        }
    }
    public int CastIron
    {
        get { return castIron; }
        set {
            if (value >= MaxResource)
            {
                value = MaxResource;
            }
            castIron = value;
        }
    }
    public int Cow
    {
        get { return cow; }
        set {
            if (value >= MaxResource)
            {
                value = MaxResource;
            }
            cow = value;
        }
    }
    public int Sheep
    {
        get { return sheep; }
        set {
            if (value >= MaxResource)
            {
                value = MaxResource;
            }
            sheep = value;
        }
    }
    public int Cheese
    {
        get { return cheese; }
        set {
            if (value >= MaxResource)
            {
                value = MaxResource;
            }
            cheese = value;
        }
    }
    public int Ham
    {
        get { return ham; }
        set
        {
            if (value >= MaxResource)
            {
                value = MaxResource;
                ham = value;
            }
        }
    }

    public int Fleece
    {
        get { return fleece; }
        set {
            if (value >= MaxResource)
            {
                value = MaxResource;
            }
            fleece = value;
        }
    }
    public int Cloth
    {
        get { return cloth; }
        set {
            if (value >= MaxResource)
            {
                value = MaxResource;
            }
            cloth = value;
        }
    }
}
