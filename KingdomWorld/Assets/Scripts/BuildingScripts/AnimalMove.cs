using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove : MonoBehaviour
{
    private float speedX = 0f;
    private float speedY = 0f;
    public Transform trs;

    private bool outsideCheck = false;

    private float timer = 0f;
    private float increaseInterval = 2f;

    private void Update()
    {
        timer += Time.deltaTime;

        RandomTimer();
        trs.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime, 0);
        if(trs.localPosition.x > 3)
        {
            trs.localPosition = new Vector3(3, trs.localPosition.y);
        }else if(trs.localPosition.x < -3)
        {
            trs.localPosition = new Vector3(-3, trs.localPosition.y);
        }else if(trs.localPosition.z > 3)
        {
            trs.localPosition = new Vector3(trs.localPosition.x, trs.localPosition.y, 3);
        }else if(trs.localPosition.z < -3)
        {
            trs.localPosition = new Vector3(trs.localPosition.x, trs.localPosition.y, -3);
        }
    }

    private void RandomTimer()
    {
        if(timer >= increaseInterval)
        {
            timer = 0f;

            speedX = Random.Range(-0.5f, 0.5f);
            //Debug.Log(speedX);
            speedY = Random.Range(-0.5f, 0.5f);
            //Debug.Log(speedY);
        }
    }
}
