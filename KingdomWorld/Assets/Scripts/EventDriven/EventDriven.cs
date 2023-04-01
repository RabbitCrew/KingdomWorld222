using System.Collections;
using System.Collections.Generic;
using UnityEngine;

delegate void VoidVoidEvent();

class RemoveEventDriven
{
	public static event VoidVoidEvent isRemoveEvent;

	public void RunIsRemoveEvent()
	{
		if (isRemoveEvent != null)
		{
			isRemoveEvent();
		}
	}
}

class CallBuildingButtonToBuildingColiderEventDriven
{
	public static event VoidVoidEvent isClickFalseEvent;

	public void RunIsClickFalseEvent()
	{
		if (isClickFalseEvent != null)
		{
			isClickFalseEvent();
		}
	}
}
