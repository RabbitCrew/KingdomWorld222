using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ǹ� ������ ������ ������ �Ұ����� ������ �ð������� �����ֱ� ���� Ÿ���� ���� �ٲٴ� Ŭ�����̴�.
public class TileColorChange : MonoBehaviour
{
    public void ChangeRedColor()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void ChangeGreenColor()
	{
        this.GetComponent<SpriteRenderer>().color = Color.green;
	}


    public void ChangeWhiteColor()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;

    }
}
