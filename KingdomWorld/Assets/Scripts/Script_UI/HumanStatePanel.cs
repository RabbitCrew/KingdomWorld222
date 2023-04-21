using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HumanStatePanel : JobStringArr
{
    [SerializeField] private TextMeshProUGUI jobText;
    private GameObject human;
    private MouseRay mouseRay;

    public void SetHuman(GameObject human)
    {
        this.human = human;
        UpdateText();
    }
    public void SetMouseRay(MouseRay mouseRay)
    {
        this.mouseRay = mouseRay;
    }
    public void SetTargetTrans()
    {
        if (mouseRay != null)
        {
            mouseRay.SetTargetTransform(human.transform);
        }
    }    

    private void UpdateText()
    {
        if (human != null)
        {
            jobText.text = jobArr[(int)human.GetComponent<CitizenInfoPanel>().jobNumEnum];
        }
        else
        {
            jobText.text = "X";
        }
    }
}
