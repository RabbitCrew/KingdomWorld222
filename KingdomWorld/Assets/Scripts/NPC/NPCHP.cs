using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHP : MonoBehaviour
{
    [SerializeField] GameObject DeadBodyMom;

    int MaxHP = 10;
    int Hp = 10;

    private void Start()
    {
        this.gameObject.GetComponent<NPC>().HP = this.gameObject.GetComponent<NPC>().Maxhp;
    }

    void DeadCheck()
    {
        int RandomDead;

        RandomDead = Random.Range(0, 100);

        //if (RandomDead <= 5)
        //{
        //    this.transform.parent.SendMessage("DeadBodyCreate", this.transform.position);
        //}
    }

    private void OnDisable()
    {
        DeadCheck();
    }
}