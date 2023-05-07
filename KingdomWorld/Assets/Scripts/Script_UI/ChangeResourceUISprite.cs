using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeResourceUISprite : MonoBehaviour
{
    [SerializeField] private Image resourceUI;
    [SerializeField] private Sprite shortResourceUISpr;
    [SerializeField] private Sprite longResourceUISpr;
	[SerializeField] private RectTransform resourceUIRectTrans;
	private RectTransform rectTrans;
	private Vector2 shortVec;
	private Vector2 longVec;
	private Vector2 shortResourceVec;
	private Vector2 longResourceVec;
    private bool isLong;

	private void Awake()
	{
		rectTrans = this.GetComponent<RectTransform>();
		shortVec = new Vector2(760f, 300f);
		longVec = new Vector2(760f, 500f);
		shortResourceVec = new Vector2(0f, 240f);
		longResourceVec = new Vector2(0f, 40f);
		isLong = false;

	}

	public void ChangeSprite()
	{
		if (isLong)
		{
			isLong = false;
			resourceUI.sprite = shortResourceUISpr;
			resourceUIRectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 80f);
			rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 80f);
			rectTrans.localPosition = longVec;
			resourceUIRectTrans.localPosition = longResourceVec;

		}
		else
		{
			isLong = true;
			resourceUI.sprite = longResourceUISpr;
			resourceUIRectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 480f);
			rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 480f);
			rectTrans.localPosition = shortVec;
			resourceUIRectTrans.localPosition = shortResourceVec;

		}
	}
}
