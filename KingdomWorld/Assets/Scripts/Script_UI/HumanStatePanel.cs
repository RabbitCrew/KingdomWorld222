using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HumanStatePanel : JobStringArr
{
    [SerializeField] private TextMeshProUGUI jobText;
    private GameObject human;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetHuman(GameObject human)
    {
        this.human = human;
        UpdateText();
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
