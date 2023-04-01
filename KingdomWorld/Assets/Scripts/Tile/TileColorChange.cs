using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 건물 생성시 가능한 영역과 불가능한 영역을 시각적으로 보여주기 위해 타일의 색을 바꾸는 클래스이다.
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
