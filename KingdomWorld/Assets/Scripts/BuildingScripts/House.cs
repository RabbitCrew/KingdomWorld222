using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private float increaseInterval = 10f;
    private float timer = 0f;

    public BuildingSetting buildingsetting;

    public void AddCitizen()        // . . . ÀÏÁ¤½Ã°£¸¶´Ù ½Ã¹ÎÃÖ´ëÄ¡¸¦ ´Ã·ÁÁÜ
    {
        if (timer >= increaseInterval)
        {
            timer = 0f;
            buildingsetting.citizenCount++;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        AddCitizen();
    }
}
