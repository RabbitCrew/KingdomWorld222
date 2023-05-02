using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Setgrid
{
    //¹Ð, ½Ä·®, ³ª¹«, À°·ù, °¡Á×, ±ÝÈ­, Ã¶±¤¼®, ÁÖÁ¶Ã¶, ¼Ò, ¾ç, ¿ìÀ¯, Ä¡Áî, ÇÜ, ¾çÅÐ, ¿Ê°¨, ¹ÙÀ§

    private int wheat;
    private int food = 1000;
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
    public int MaxResource; //ÀÚ¿ø ÃÖ´ëÄ¡
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
            else if(value <= 0)
            {
                while (value > 0)//food°¡ 0ÀÌÇÏ·Î ¶³¾îÁú½Ã ½Ã¹Î »ç¸Á
                {
                    if (Meat > 0)
                    {
                        Meat -= 1;
                        value += 10;
                    } else if (Ham > 0)
                    {
                        Ham -= 1;
                        value += 5;
                    } else if (Cheese > 0)
                    {
                        Cheese -= 1;
                        value += 5;
                    }
                }
                if (value < 10)
                    value = 10;
                for(int i=0; i < value / 10; i++)
                {
                    if(AllHuman.Count >= 1)
                    {
                        AllHuman[0].GetComponent<NPC>().HP = 0;
                        AllHuman.RemoveAt(0);
                    }
                }
                value = 0;
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
                if (value < 10)
                    value = 10;
                for (int i = 0; i < value / 10; i++)
                {
                    if (AllHuman.Count >= 1)
                    {
                        AllHuman[0].GetComponent<NPC>().HP = 0;
                        AllHuman.RemoveAt(0);
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
