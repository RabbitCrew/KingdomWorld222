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
    }

    private void RandomTimer()
    {
        if(timer >= increaseInterval)
        {
            timer = 0f;

            speedX = Random.Range(-0.5f, 0.5f);
            Debug.Log(speedX);
            speedY = Random.Range(-0.5f, 0.5f);
            Debug.Log(speedY);
        }
    }
}
