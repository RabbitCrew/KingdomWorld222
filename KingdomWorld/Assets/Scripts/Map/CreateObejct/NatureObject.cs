using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureObject : MonoBehaviour
{
    private CallSettingObjectToNatureObjectEventDriven callSettingObjectToTreeObjectEventDriven = new CallSettingObjectToNatureObjectEventDriven();

    public ulong objCode { get; set; }

    // Start is called before the first frame update
    void OnDisable()
    {
        callSettingObjectToTreeObjectEventDriven.RunRemoveObjectInfoToTileEvent(objCode, this.gameObject);
    }
}
