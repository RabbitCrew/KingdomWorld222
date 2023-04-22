using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMineWorker : NPC
{
	private bool isWork;
    public void StoneMineWorkerMove()
	{
		//낮엔 일하고 밤엔 일 안하고
		if (GameManager.instance.isDaytime && !isWork) { isWork = true; }
		else if (!GameManager.instance.isDaytime && isWork) { isWork = false; }

		if (isWork)
		{
			//돌캐러 가야됨

		}
		else
		{
			//집으로 돌아가야됨
		}

	}
}
