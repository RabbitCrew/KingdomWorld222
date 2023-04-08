using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterHouse : MonoBehaviour
{
    private BuildingSetting buildingSetting;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            collision.tag = "Hunter";
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Hunter")
        {
            // buildingSetting.AddItem("meat", npc가 가지고있는 자원 수);
        }
    }
}
