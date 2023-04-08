using System.Collections;
using System.Collections.Generic;
using UnityEngine;

delegate void VoidVoidEvent();
delegate void VoidUlongGameObjectEvent(ulong n, GameObject obj);
delegate void VoidGameObjectEvent(GameObject obj);

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

class CallSettingObjectToBuildingColiderEventDriven
{
	public static event VoidUlongGameObjectEvent getObjectCodeEvent;

	public void RunGetObjectCodeEvent(ulong objCode, GameObject obj)
	{
		if (getObjectCodeEvent != null)
		{
			getObjectCodeEvent(objCode, obj);
		}
	}
}

class CallBuildingAttachMouseToWaitingBuildingEventDriven
{
	public static event VoidGameObjectEvent getObjectEvent;

	public void RunGetObjectEvent(GameObject obj)
    {
		if (getObjectEvent != null)
        {
			getObjectEvent(obj);
        }
    }
}


