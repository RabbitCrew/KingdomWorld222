using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHP : MonoBehaviour
{
    [SerializeField] GameObject DeadBodyMom;

    int MaxHP = 10;
    int Hp = 10;

    private void Awake()
    {
        SetHp();
    }

    private void Update()
    {
        DeadCheck();
        GetHP();
    }

    void SetHp()
    {
        this.gameObject.GetComponent<NPC>().Maxhp = MaxHP;
        this.gameObject.GetComponent<NPC>().HP = MaxHP;

        Hp = MaxHP;
    }

    void GetHP()
    {
        Hp = this.gameObject.GetComponent<NPC>().HP;
        MaxHP = this.gameObject.GetComponent<NPC>().Maxhp;

        if (this.gameObject.GetComponent<NPC>().Maxhp < this.gameObject.GetComponent<NPC>().HP)
        {
            this.gameObject.GetComponent<NPC>().HP = this.gameObject.GetComponent<NPC>().Maxhp;
        }
    }

    void DeadCheck()
    {
        if(Hp <= 0)
        {
            GameManager.instance.AllHuman.Remove(this.gameObject);

            this.transform.parent.SendMessage("DeadBodyCreate", this.transform.position);

            Destroy(this.gameObject);
        }
    }
}