using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IVoidClickObject
{
	public void ClickObject();
}

interface IBuildingProperty
{
	public string buildingName { get; set; }

}
