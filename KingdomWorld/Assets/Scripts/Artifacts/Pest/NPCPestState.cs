using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPestState : MonoBehaviour
{
    public bool InPest { get; set; }

    [SerializeField] GameObject PestImage;

    private void Awake()
    {
        InPest = false;
    }

    private void Update()
    {
        PestImagePrint();
    }

    public void IsPest()
    {
        InPest = true;
    }

    float HPDropCool = 3f;
    float DefaultHPDropCool = 3f;

    void PestImagePrint()
    {
        if (InPest == true)
        {
            PestImage.SetActive(true);

            HPDropCool -= Time.deltaTime;

            if (HPDropCool <= 0)
            {
                this.gameObject.GetComponent<NPCParameter>().HP -= 1;

                HPDropCool = DefaultHPDropCool;
            }
        }
        else
        {
            PestImage.SetActive(false);
        }
    }
}