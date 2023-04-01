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
                break;
            case 1:
                NPC.gameObject.tag = "CarpenterNPC";
                break;
            case 2:
                NPC.gameObject.tag = "CheeseNPC";
                break;
            case 3:
                NPC.gameObject.tag = "FabricNPC";
                break;
            case 4:
                NPC.gameObject.tag = "FarmNPC";
                break;
            case 5:
                NPC.gameObject.tag = "HamNPC";
                break;
            case 6:
                NPC.gameObject.tag = "NPC";
                break;
            case 7:
                NPC.gameObject.tag = "Hunter";
                break;
            case 8:
                NPC.gameObject.tag = "MineWorker";
                break;
            case 9:
                NPC.gameObject.tag = "WoodCutter";
                break;
            case 10:
                NPC.gameObject.tag = "Smith";
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
