using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCitizen : MonoBehaviour
{
    public GameObject Citizen;

    public GameObject SpawnPoint;

    private void Start()
    {
        InvokeRepeating("SpawnintgCitizen", 10f, 10f);
    }

    void SpawnintgCitizen()
    {
        SpawnPoint = Instantiate(Citizen);
    }
}
