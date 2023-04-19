using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CitizenCountText : MonoBehaviour
{
    TextMeshProUGUI CitizenText;

    private void Update()
    {
        CitizenText.text = "½Ã¹Î ¼ö : " + (GameManager.instance.AllHuman.Count + Inventory.instance.Children).ToString() + "¸í";
    }
}
