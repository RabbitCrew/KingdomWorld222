using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRay : MonoBehaviour
{
    private float distance = 50f;
    private RaycastHit[] hits;

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward * distance, Color.blue, 0.3f);
        //Debug.Log((Physics.Raycast(transform.position, transform.forward, out hit, distance)));
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(1);
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward);
            hits = Physics.RaycastAll(ray, distance);
            for (int i = 0; i < hits.Length; i++)
            {
                
                if (hits[i].transform.GetComponent<BuildingColider>() != null)
                {
                    //Debug.Log(hits[i].transform.name);
                    hits[i].transform.GetComponent<BuildingColider>().ClickObject();
                }
            }
        }
    }
}
