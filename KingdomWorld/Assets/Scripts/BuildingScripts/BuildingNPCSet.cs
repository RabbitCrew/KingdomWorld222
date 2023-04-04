using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingNPCSet : MonoBehaviour
{
    public GameObject NPC;

    public GameObject NPCInfo;

    public TMP_Dropdown NpcJobSelect;

    public TextMeshProUGUI NPCName;
    public TextMeshProUGUI NPCJob;

    public string NameSave;
    public string JobSave;

    private void OnMouseDown()
    {
        NPCInfo.SetActive(true);

        NPCName.text = NameSave;
        NPCJob.text = JobSave;
    }

    public void SetBNPC(int BuildNum)
    {
        NPCJob.text = NpcJobSelect.options[BuildNum].text;

        JobSave = NpcJobSelect.options[BuildNum].text;

        switch (BuildNum)
        {
            case 0:
                NPC.gameObject.tag = "StorageNPC";

                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                
                break;
            case 1:
                NPC.gameObject.tag = "CarpenterNPC";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 2:
                NPC.gameObject.tag = "CheeseNPC";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 3:
                NPC.gameObject.tag = "FabricNPC";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 4:
                NPC.gameObject.tag = "FarmNPC";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 5:
                NPC.gameObject.tag = "HamNPC";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 6:
                NPC.gameObject.tag = "NPC";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 7:
                NPC.gameObject.tag = "Hunter";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 8:
                NPC.gameObject.tag = "MineWorker";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 9:
                NPC.gameObject.tag = "WoodCutter";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            case 10:
                NPC.gameObject.tag = "Smith";
                NPC.gameObject.GetComponent<NPC>().NPCBUildTrigger = true;
                break;
            default:
                break;
        }
    }

    public void NPCNameSet(string Name)
    {
        NPCName.text = Name;
        NameSave = Name;
    }
}
