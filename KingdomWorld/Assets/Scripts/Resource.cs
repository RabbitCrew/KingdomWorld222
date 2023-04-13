using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Setgrid
{
    //��, �ķ�, ����, ����, ����, ��ȭ, ö����, ����ö, ��, ��, ġ��, ����, ��

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
    private int cheese;
    private int fleece;
    private int cloth;
    public int MaxResource; //�ڿ� �ִ�ġ

    
    public int Wheat {
        get { return wheat; } 
        set 
        { 
            if(value >= MaxResource)
            {
                value = MaxResource;
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
                value = MaxResource;
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
