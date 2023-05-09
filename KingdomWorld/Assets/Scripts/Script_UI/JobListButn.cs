using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class JobListButn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textPro;

    public BuildingNPCSet BNSet;

    public int butnNum { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        butnNum = -1;
    }

    public void SetButn(int num)
	{
        butnNum = num;

        //BNSet.SetBNPC(num);
    }
    public void SetText(string str)
    {
        textPro.text = str;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
