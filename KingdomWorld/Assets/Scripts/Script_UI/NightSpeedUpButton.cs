using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NightSpeedUpButton : UIComment
{
	[SerializeField] private TextMeshProUGUI nightSpeedUpText;
    private bool isOnNightSpeedUp;

    // Start is called before the first frame update
    void Awake()
    {
        isOnNightSpeedUp = true;
		commentNum = 7;

	}

	private void Update()
	{
		if (isOnNightSpeedUp && !GameManager.instance.isDaytime)
		{
			GameManager.instance.timeSpeed = 15f;
		}
		else
		{
			GameManager.instance.timeSpeed = 1f;
		}
	}

	public void SetIsOnNightSpeedUp()
	{
        if (isOnNightSpeedUp) { isOnNightSpeedUp = false; nightSpeedUpText.text = "广 积帆Off"; }
        else { isOnNightSpeedUp = true; nightSpeedUpText.text = "广 积帆On"; }
	}
}
