using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMineWorker : NPC
{
	private bool isWork;
    public void StoneMineWorkerMove()
	{
		//���� ���ϰ� �㿣 �� ���ϰ�
		if (GameManager.instance.isDaytime && !isWork) { isWork = true; }
		else if (!GameManager.instance.isDaytime && isWork) { isWork = false; }

		if (isWork)
		{
			//��ĳ�� ���ߵ�

		}
		else
		{
			//������ ���ư��ߵ�
		}

	}
}
