using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AClock : MonoBehaviour
{
    [SerializeField] private GameObject[] Clocks;

    int ClockCount = 2;

    private void Start()
    {
        Clocks = new GameObject[ClockCount];
    }

    private void Update()
    {

    }

    void Clock()
    {
        Time.timeScale = 2;
    }
}
