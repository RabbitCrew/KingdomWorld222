using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smithy : MonoBehaviour
{
    // . . . store 제거 및 추가 문제 해결 X

    private BuildingSetting buildingSetting;

    public int itronstone = 0;
    public int castIron = 0;
    private int castIronMax = 0;

    private float increaseInterval = 1f;
    private float timer = 0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "MineWorker")
        {
            //buildingSetting.AddItem("itronstone", npc가 가지고있는 철광석 수);
        }
    }

    private void Start()
    {
        buildingSetting = GetComponent<BuildingSetting>();
    }

    private void Update()
    {
        castIronMax = buildingSetting.storeMax;

        timer += Time.deltaTime;
        ChangeCastIron();
    }

    public void ChangeCastIron()
    {
        if(itronstone > 0 && castIron <= castIronMax)
        {
            if(timer >= increaseInterval)
            {
                timer = 0;
                itronstone--;
                buildingSetting.store--;

                castIron++;
                buildingSetting.AddItem("castIron", 1);
            }
        }
    }
}
