using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public int ResourceN;

    public int ResourceStack = 50;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "StorageNPC")
        {
            switch (ResourceN)
            {
                case 0:
                    GameManager.instance.Wheat += ResourceStack;
                    break;
                case 1:
                    GameManager.instance.Food += ResourceStack;
                    break;
                case 2:
                    GameManager.instance.Wood += ResourceStack;
                    break;
                case 3:
                    GameManager.instance.Meat += ResourceStack;
                    break;
                case 4:
                    GameManager.instance.Leather += ResourceStack;
                    break;
                case 5:
                    GameManager.instance.Gold += ResourceStack;
                    break;
                case 6:
                    GameManager.instance.Itronstone += ResourceStack;
                    break;
                case 7:
                    GameManager.instance.CastIron += ResourceStack;
                    break;
                case 8:
                    GameManager.instance.Cow += ResourceStack;
                    break;
                case 9:
                    GameManager.instance.Sheep += ResourceStack;
                    break;
                case 10:
                    GameManager.instance.Cheese += ResourceStack;
                    break;
                case 11:
                    GameManager.instance.Fleece += ResourceStack;
                    break;
                case 12:
                    GameManager.instance.Cloth += ResourceStack;
                    break;
                default:
                    break;  
            }
        }
    }
}