using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingBuilding : MonoBehaviour
{
    //public int makeTime { get; set; }
    public GameObject building { get; private set; }
    public float time { get; set; }
    public float maxTime { get; set; }
    public int shield { get; set; }
    public int maxShield { get; set; }
    private CallBuildingAttachMouseToWaitingBuildingEventDriven callBuildingAttachMouseToWaitingBuildingEventDriven = new CallBuildingAttachMouseToWaitingBuildingEventDriven();
    private Material material;
    private float fade;

	private void Awake()
	{
        time = 0f;
        fade = 0f;
        shield = 0;
    }
	void Start()
    {

        //makeTime = Random.Range(1, 10);
        material = GetComponent<SpriteRenderer>().material;
        material.SetFloat("_Fade", fade + 0.2f);

    }

    public void SetBuilding(GameObject obj)
    {
        building = Instantiate(obj);
        building.transform.parent = this.transform.parent;

        if (building.GetComponent<BuildingSetting>() != null)
        {
            maxTime = building.GetComponent<BuildingSetting>().BuildingTime;
        }
        else
		{
            maxTime = 10f;
		}
        building.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;

        fade = Mathf.InverseLerp(0, maxTime, time);
        fade *= 0.6f;

        //Debug.Log(fade);

        if (time >= maxTime)
        {
            building.SetActive(true);
            building.GetComponent<BuildingColider>().isSettingComplete = true;
            callBuildingAttachMouseToWaitingBuildingEventDriven.RunGetObjectEvent(building);
            Destroy(this.gameObject);
        }

        material.SetFloat("_Fade", fade + 0.2f);
    }

    private void OnDisable()
    {
        if (!building.activeSelf)
        {
            Destroy(building.gameObject);
        }
    }
}
