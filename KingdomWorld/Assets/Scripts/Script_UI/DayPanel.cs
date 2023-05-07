using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayPanel : MonoBehaviour
{
    private int[] dayArr = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    private int temMonth;
    // Start is called before the first frame update
    void Awake()
    {
        temMonth = dayArr[3];
    }

	private void Update()
	{
		if (GameManager.instance.GameStop) { return; }


	}
}
