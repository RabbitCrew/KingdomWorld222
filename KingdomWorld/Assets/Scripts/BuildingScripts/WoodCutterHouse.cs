using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCutterHouse : MonoBehaviour
{
    public GameObject npc;

    private BuildingSetting buildingSetting;

    private void Start()
    {
        buildingSetting = GetComponent<BuildingSetting>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "NPC")
        {
            buildingSetting.AddItem("wood", 1);
        }
    }
}
