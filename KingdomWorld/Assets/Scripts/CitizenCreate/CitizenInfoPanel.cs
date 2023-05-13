using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectNS;

public class CitizenInfoPanel : MonoBehaviour
{
    [SerializeField] private SpriteRenderer clothesSpr;
    public JobNum jobNumEnum;  // �ù��� ���� ������ ��� ����.

    public void WareClothes(Sprite clothes, int jobCode)
    {
        if (clothes == null) { clothesSpr.sprite = null; }
        else { clothesSpr.sprite = clothes; }

        if (System.Enum.GetValues(typeof(JobNum)).Length <= jobCode || jobCode == -1 || jobCode == 0)
        {
            jobNumEnum = JobNum.CITIZEN;
            if (GameManager.instance.RestHuman.FindIndex(a => a.Equals(this.gameObject)) == -1)
                GameManager.instance.RestHuman.Add(this.gameObject);
        }
        else
        {
            jobNumEnum = (JobNum)jobCode;
            /*if (GameManager.instance.RestHuman.FindIndex(a => a.Equals(this.gameObject)) != -1)
			{
                GameManager.instance.RestHuman.Remove(gameObject);
			}*/
        }
        //Debug.Log(jobNumEnum);
    }

}
