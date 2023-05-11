using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DayPanel : UIComment
{
    [SerializeField] private TextMeshProUGUI dayText;
    private int[] dayArr = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    private int temMonth;
    private int index;
    private int time;
    private int Index {
		get 
        {
            return index;
        } 
        set
		{
            if (value >= 12)
			{
                index = 0;
			}
            else
			{
                index = value;
			}
		}
    
    }
    // Start is called before the first frame update
    void Awake()
    {
        Index = 2;
        commentNum = 2;
        temMonth = 0;
        time = 10;
        CountDay();
    }


    public void CountDay()
    {

        temMonth++;
        
        //dayText.text = (Index + 1).ToString() + "/" + temMonth.ToString();
        if (!GameManager.instance.isWinterComing)
        { time--; dayText.text = "D-" + time; }
        else
        { time++; dayText.text = "Day" + time; }

  //      if (temMonth == dayArr[Index])
		//{
  //          temMonth = 0;
  //          Index++;
		//}
	}
}
