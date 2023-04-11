using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingBuildingInfo : MonoBehaviour
{
    public int makeTime { get; set; }
    public GameObject building { get; private set; }

    private CallBuildingAttachMouseToWaitingBuildingEventDriven callBuildingAttachMouseToWaitingBuildingEventDriven = new CallBuildingAttachMouseToWaitingBuildingEventDriven();
    private Material material;
    private float time = 0f;
    private float fade = 0f;
    void Start()
    {
        makeTime = Random.Range(1, 10);
        material = GetComponent<SpriteRenderer>().material;
        material.SetFloat("_Fade", fade + 0.2f);

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

        fade = Mathf.InverseLerp(0, makeTime, time);
        fade *= 0.6f;

        //Debug.Log(fade);

        if (time >= makeTime)
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
