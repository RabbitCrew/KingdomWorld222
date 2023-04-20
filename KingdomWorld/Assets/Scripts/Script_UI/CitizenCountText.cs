using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CitizenCountText : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI CitizenText;

    private void Update()
    {
        CitizenText.text = "�ù� �� : " + (GameManager.instance.AllHuman.Count + Inventory.instance.Children).ToString() + "��";
    }
}
