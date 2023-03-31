using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingColider : MonoBehaviour
{
    private List<Collider> colList = new List<Collider>();

    private void OnTriggerStay(Collider col)
    {
        colList.Add(col);
        if (col.gameObject.GetComponent<TileColorChange>() != null)
        {
            col.gameObject.GetComponent<TileColorChange>().ChangeRedColor();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<TileColorChange>() != null)
        {
            col.gameObject.GetComponent<TileColorChange>().ChangeWhiteColor();
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < colList.Count; i++)
        {
            if (colList[i].gameObject.GetComponent<TileColorChange>() != null)
            {
                colList[i].gameObject.GetComponent<TileColorChange>().ChangeWhiteColor();
            }
        }
        colList.Clear();
    }
}


