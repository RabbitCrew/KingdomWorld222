using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCutterHouse : MonoBehaviour
{
    private BuildingSetting buildingSetting;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "NPC")
        {
            collision.tag = "WoodCutter";
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "WoodCutter")
        {
           // buildingSetting.AddItem("Wood", npc가 가지고있는 자원 수);
        }
    }
}
