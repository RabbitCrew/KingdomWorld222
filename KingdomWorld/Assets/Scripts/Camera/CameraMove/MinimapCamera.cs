using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
	Transform trans;
	private void Awake()
	{
		trans = this.transform;
	}


	// Update is called once per frame
	void Update()
    {
		trans.position = Camera.main.transform.position;
    }
}
