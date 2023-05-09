using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.SlimInitializeGrid(10, 10, this.transform);
    }
}
