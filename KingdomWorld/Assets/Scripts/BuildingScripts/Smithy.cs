using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smithy : MonoBehaviour
{
    private BuildingSetting buildingSetting;

    public int itronstone = 0;
    public int castIron = 0;
    private int castIronMax = 0;

    private float increaseInterval = 10f;
    private float timer = 0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "MineWorker")
        {
            //buildingSetting.AddItem("itronstone", npc°¡ °¡Áö°íÀÖ´Â Ã¶±¤¼® ¼ö);
        }
    }

    private void Update()
    {
        itronstone = buildingSetting.store;
        castIronMax = buildingSetting.storeMax;

        timer += Time.deltaTime;
        ChangeCastIron();
    }

    public void ChangeCastIron()
    {
        if(itronstone > 0 && castIron < castIronMax)
        {
            if(timer >= increaseInterval)
            {
                timer = 0;
                buildingSetting.store--;
                castIron++;
            }
        }
    }
}
