using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCParameter : MonoBehaviour
{
    private int hp;
    private int maxHp;
    [SerializeField]
    private GameObject buildingNum;
    private Transform houseTr;
    private float speed;
    private float buildingSpeed;
    public int HavedWheat;
    public int HavedWood;
    public int HavedAnimal;
    protected Dictionary<string, int> cargoclasshaveitem = new Dictionary<string, int>();

    public int Maxhp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }

    public int HP
    {
        get { return hp; }
        set { 
            if (value > maxHp)
            {
                value = maxHp;
            }
            hp = value;
        }
    }

    public GameObject BuildingNum
    {
        get { return buildingNum; }
        set { buildingNum = value; }
    }

    public Transform HouseTr
    {
        get { return houseTr; }
        set { houseTr = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public float BuildingSpeed
    {
        get { return buildingSpeed; }
        set { buildingSpeed = value; }
    }
}
