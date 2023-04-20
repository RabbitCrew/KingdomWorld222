using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PestOn : MonoBehaviour
{
    [SerializeField] float PestDistance;

    private void Update()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, PestDistance, Vector3.up, 0);

        for (int i = 0; i < hits.Length; i++) // 레이로 클릭한 부분의 오브젝트 뒤져서
        {
            if(hits[i].collider.name == "Citizen1(Clone)")
            {
                Debug.Log(hits[i].collider.name);
            }
        }
    }
}
