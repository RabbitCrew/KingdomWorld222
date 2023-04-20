using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PestOn : MonoBehaviour
{
    [SerializeField] float PestDistance;

    float Cool = 10f;

    private void Update()
    {
        PeopleFind();

        DeadCool();
    }

    void PeopleFind()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, PestDistance, Vector3.up, 0);

        for (int i = 0; i < hits.Length; i++) // 레이로 클릭한 부분의 오브젝트 뒤져서
        {
            if (hits[i].collider.name == "Citizen1(Clone)")
            {
                //Debug.Log(hits[i].collider.name);

                hits[i].collider.SendMessage("IsPest");
            }
        }
    }

    void DeadCool()
    {
        if(Cool > 0)
        {
            Cool -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
