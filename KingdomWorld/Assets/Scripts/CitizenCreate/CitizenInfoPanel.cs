using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectNS;

public class CitizenInfoPanel : MonoBehaviour
{
    [SerializeField] private SpriteRenderer clothesSpr;
    public JobNum jobNumEnum;  // 시민의 직업 정보를 담고 있음.

    public void WareClothes(Sprite clothes, int jobCode)
    {
        if (clothes == null) { clothesSpr.sprite = null; Debug.Log("ab"); }
        else { clothesSpr.sprite = clothes; Debug.Log("abc"); }

        if (System.Enum.GetValues(typeof(JobNum)).Length <= jobCode)
        {
            jobNumEnum = JobNum.CITIZEN;
        }
        else
        {
            jobNumEnum = (JobNum)jobCode;
        }
        Debug.Log(jobNumEnum);
    }

}
