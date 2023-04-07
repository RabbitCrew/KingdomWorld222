using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingBuilding : MonoBehaviour
{
    private CallBuildingAttachMouseToWaitingBuildingEventDriven callBuildingAttachMouseToWaitingBuildingEventDriven = new CallBuildingAttachMouseToWaitingBuildingEventDriven();

    public int makeTime { get; set; }
    public GameObject building { get; private set; }

    private float time = 0f;

    void Start()
    {
        makeTime = Random.Range(1, 10);
    }

    public void SetBuilding(GameObject obj)
    {
        building = Instantiate(obj);
        building.transform.parent = this.transform.parent;
        building.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time > makeTime)
        {
            building.SetActive(true);
            building.GetComponent<BuildingColider>().isSettingComplete = true;
            callBuildingAttachMouseToWaitingBuildingEventDriven.RunGetObjectEvent(building);
            Destroy(this.gameObject);
        }
    }

    private void OnDisable()
    {
        if (!building.activeSelf)
        {
            Destroy(building.gameObject);
        }
    }
}
