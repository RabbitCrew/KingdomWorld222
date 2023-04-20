using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPeopleCon : MonoBehaviour
{
    public Transform DeadBMother;

    [SerializeField] GameObject DeadBodyPrefab;

    public void DeadBodyCreate(Vector3 BodyPos)
    {
        GameObject BodyMother = Instantiate(DeadBodyPrefab);

        BodyMother.transform.position = BodyPos;

        BodyMother.transform.parent = DeadBMother;
    }
}