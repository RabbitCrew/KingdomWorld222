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
    public int BuildingNum { get; set; }

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
        GameManager.instance.WaitingBuildingList.Add(this.gameObject);
        buildingRange.SetActive(true);
        material = GetComponent<SpriteRenderer>().material;
        material.SetFloat("_Fade", realFade);
        GameManager.instance.SlimInitializeGrid(10, 10, this.transform);

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
            BuildingNum = building.GetComponent<BuildingSetting>().BuildingNum;
        }
        else
        {
            maxTime = 10f;
        }
        building.SetActive(false);
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;

        fade = Mathf.InverseLerp(0, building.GetComponent<BuildingSetting>().MaxBuildingHp, building.GetComponent<BuildingSetting>().BuildingHp);
        maxTime = building.GetComponent<BuildingSetting>().MaxBuildingHp;
        time = building.GetComponent<BuildingSetting>().BuildingHp;
        shield = building.GetComponent<BuildingSetting>().buildingShield;
        maxShield = building.GetComponent<BuildingSetting>().maxBuildingShield;

        //fade = Mathf.InverseLerp(0, maxTime, time); // 윗줄 코드와 전환

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
        //if (time >= maxTime)    // 윗줄 코드와 전환
        {
            building.SetActive(true);
            building.GetComponent<BuildingColider>().isSettingComplete = true;
            callBuildingAttachMouseToWaitingBuildingEventDriven.RunGetObjectEvent(building);
            buildingRange.SetActive(false);
            Destroy(this.gameObject);
        }

        material.SetFloat("_Fade", realFade);
        //material.SetFloat("_Fade", fade + 0.2f); // 윗줄 코드와 전환
    }

    private void OnDisable()
    {
        if (!building.activeSelf)
        {
            Destroy(building.gameObject);
        }
    }
}