using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PestOn : MonoBehaviour
{
    [SerializeField] float PestDistance;

    float DestroyCool = 12f;
    float CorruptionCool = 3f;

    private void Update()
    {
        PeopleFind();

        DeadBodyCool();
    }

    void PeopleFind()
    {
        if (CorruptionCool > 0)
        {
            CorruptionCool -= Time.deltaTime;
        }
        else if (CorruptionCool <= 0)
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, PestDistance, Vector3.up, 0);

            for (int i = 0; i < hits.Length; i++) // 레이로 클릭한 부분의 오브젝트 뒤져서
            {
                if (hits[i].collider.name == "Citizen1(Clone)")
                {
                    hits[i].collider.SendMessage("IsPest");
                }
            }
        }
    }

    void DeadBodyCool()
    {
        if(DestroyCool > 0)
        {
            DestroyCool -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}