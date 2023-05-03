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
            }else if(value <= 0)
            {
                Debug.Log("����");
                if(buildingNum != null)
                {
                    int index = GameManager.instance.RestHuman.FindIndex(a => a.Equals(this.gameObject));
                    if (index != -1) { GameManager.instance.RestHuman.RemoveAt(index); }
                    index = BuildingNum.GetComponent<BuildingSetting>().npcs.FindIndex(a => a.Equals(this.gameObject));
                    if (index != -1) { BuildingNum.GetComponent<BuildingSetting>().npcs.RemoveAt(index); }
                    index = GameManager.instance.AllHuman.FindIndex(a => a.Equals(this.gameObject));
                    if (index != -1) { GameManager.instance.AllHuman.RemoveAt(index); }

                }
                if (this.GetComponent<CitizenInfoPanel>() != null)
                {
                    GameManager.instance.jobCountDic[this.GetComponent<CitizenInfoPanel>().jobNumEnum]++;
                }

                Destroy(this.gameObject);
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
