using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public int ResourceN;

    public int ResourceStack = 50;

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "StorageNPC")
        {
            switch (ResourceN)
            {
                case 0:
                    Resource.Resource_Instance.Wheat += ResourceStack;
                    break;
                case 1:
                    Resource.Resource_Instance.Food += ResourceStack;
                    break;
                case 2:
                    Resource.Resource_Instance.Wood += ResourceStack;
                    break;
                case 3:
                    Resource.Resource_Instance.Meat += ResourceStack;
                    break;
                case 4:
                    Resource.Resource_Instance.Leather += ResourceStack;
                    break;
                case 5:
                    Resource.Resource_Instance.Gold += ResourceStack;
                    break;
                case 6:
                    Resource.Resource_Instance.Itronstone += ResourceStack;
                    break;
                case 7:
                    Resource.Resource_Instance.CastIron += ResourceStack;
                    break;
                case 8:
                    Resource.Resource_Instance.Cow += ResourceStack;
                    break;
                case 9:
                    Resource.Resource_Instance.Sheep += ResourceStack;
                    break;
                case 10:
                    Resource.Resource_Instance.Cheese += ResourceStack;
                    break;
                case 11:
                    Resource.Resource_Instance.Fleece += ResourceStack;
                    break;
                case 12:
                    Resource.Resource_Instance.Cloth += ResourceStack;
                    break;
                default:
                    break;  
            }
        }
    }
}