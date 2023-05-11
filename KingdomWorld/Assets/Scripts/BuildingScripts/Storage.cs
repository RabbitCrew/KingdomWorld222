using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public int ResourceN;

    public int ResourceStack = 50;

    private void Awake()
    {
        //GameManager.instance.StorageList.Add(this.gameObject);
    }

    private void Start()
    {
        GameManager.instance.MaxResource += ResourceStack;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "FarmNPC")
        {
            if(ResourceN == 0)
            GameManager.instance.Wheat += ResourceStack;
        }
    }

    public void AddStorageList()
	{
        GameManager.instance.StorageList.Add(this.gameObject);
    }

	private void OnDisable()
	{
        GameManager.instance.StorageList.Remove(this.gameObject);
	}
}