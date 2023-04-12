using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingBuilding : MonoBehaviour
{
    [SerializeField] private GameObject buildingRange;
    public GameObject building { get; private set; }
    public float time { get; set; }
    public float maxTime { get; set; }
    public int shield { get; set; }
    public int maxShield { get; set; }
    private CallBuildingAttachMouseToWaitingBuildingEventDriven callBuildingAttachMouseToWaitingBuildingEventDriven = new CallBuildingAttachMouseToWaitingBuildingEventDriven();
    private Material material;
    private float fade;
    private float realFade;
	private void Awake()
	{
        time = 0f;
        fade = 0f;
        realFade = 0f;
        shield = 0;
    }
	void Start()
    {
        buildingRange.SetActive(true);
        material = GetComponent<SpriteRenderer>().material;
        material.SetFloat("_Fade", realFade);

    }

    public void SetBuilding(GameObject obj)
    {
        building = Instantiate(obj);
        building.transform.parent = this.transform.parent;

        buildingRange.transform.localScale 
            = new Vector3(building.GetComponent<SpriteRenderer>().sprite.rect.width / 16, building.GetComponent<SpriteRenderer>().sprite.rect.height / 16, 1);

        if (building.GetComponent<BuildingSetting>() != null)
        {
            maxTime = building.GetComponent<BuildingSetting>().BuildingTime;
            building.GetComponent<BuildingSetting>().BuildingHp = 1;
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

        fade = Mathf.InverseLerp(0, building.GetComponent<BuildingSetting>().MaxBuildingHp, building.GetComponent<BuildingSetting>().BuildingHp);
        //fade = Mathf.InverseLerp(0, maxTime, time);

        fade *= 0.6f;

        if (building.GetComponent<BuildingSetting>().BuildingHp == 1)
        {
            realFade = 0f;
        }
        else
        {
            realFade = fade + 0.2f;
        }

        if (building.GetComponent<BuildingSetting>().BuildingHp >= building.GetComponent<BuildingSetting>().MaxBuildingHp)
        //if (time >= maxTime)
        {
            building.SetActive(true);
            building.GetComponent<BuildingColider>().isSettingComplete = true;
            callBuildingAttachMouseToWaitingBuildingEventDriven.RunGetObjectEvent(building);
            buildingRange.SetActive(false);
            Destroy(this.gameObject);
        }

        material.SetFloat("_Fade", realFade);
    }

    private void OnDisable()
    {
        if (!building.activeSelf)
        {
            Destroy(building.gameObject);
        }
    }
}
