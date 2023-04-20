using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPestState : MonoBehaviour
{
    bool InPest = false;

    [SerializeField] GameObject PestImage;

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
                this.gameObject.GetComponent<NPCParameter>().HP--;

                HPDropCool = DefaultHPDropCool;
            }

            Debug.Log(this.gameObject.GetComponent<NPCParameter>().HP);

            if (this.gameObject.GetComponent<NPCParameter>().HP <= 0)
            {
                Destroy(this.gameObject);

                GameManager.instance.AllHuman.Remove(this.gameObject);
            }
        }
        else
        {
            PestImage.SetActive(false);
        }
    }
}