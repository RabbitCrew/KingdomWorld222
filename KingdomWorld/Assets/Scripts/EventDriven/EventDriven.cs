using System.Collections;
using System.Collections.Generic;
using UnityEngine;

delegate void VoidVoidEvent();
delegate void VoidUlongGameObjectEvent(ulong n, GameObject obj);
delegate void VoidGameObjectEvent(GameObject obj);
delegate void VoidIntIntGameObjectIntIntEvent(int n, int n2, GameObject obj, int n3, int n4);
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

class CallBuildingAttachMouseToBuildingColiderEventDriven
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

class CallBuildingAttachMouseToSettingObjectEventDriven
{
	public static event VoidIntIntGameObjectIntIntEvent SetObjectAndPointEvent;

	public void RunSetObjectAndPointEvnet(int n, int n2, GameObject obj, int n3, int n4)
	{
		if (SetObjectAndPointEvent != null)
		{
			SetObjectAndPointEvent(n, n2, obj, n3, n4);
		}
	}
}

class CallSettingObjectToNatureObjectEventDriven
{
	public static event VoidUlongGameObjectEvent removeObjectInfoToTileEvent;

	public void RunRemoveObjectInfoToTileEvent(ulong code, GameObject obj)
    {
		if (removeObjectInfoToTileEvent != null)
        {
			removeObjectInfoToTileEvent(code, obj);
        }
    }
}


