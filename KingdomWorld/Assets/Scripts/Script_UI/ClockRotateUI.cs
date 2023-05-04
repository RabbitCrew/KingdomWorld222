using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockRotateUI : MonoBehaviour
{
    [SerializeField] private RectTransform clockImageRect;
    [SerializeField] private TImeLight2D timeLight2D;

    private float rotate;
    // Start is called before the first frame update
    void Start()
    {
        rotate = 45f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GameStop) { return; }

        if (GameManager.instance.dayNightRatio >= 0f && GameManager.instance.dayNightRatio < 0.2f)
        {
            rotate = Mathf.Lerp(45, -45, timeLight2D.colorLerp);
        }
        else if (GameManager.instance.dayNightRatio >= 0.2f && GameManager.instance.dayNightRatio < 0.4f)
        {
            rotate = Mathf.Lerp(-45, -135, timeLight2D.colorLerp);
        }
        else if (GameManager.instance.dayNightRatio >= 0.4f && GameManager.instance.dayNightRatio < 2f / 3f)
        {
            rotate = Mathf.Lerp(-135, -225, timeLight2D.colorLerp);
        }
        else if (GameManager.instance.dayNightRatio >= 2f / 3f && GameManager.instance.dayNightRatio <= 1f)
        {
            rotate = Mathf.Lerp(-225, -315, timeLight2D.colorLerp);
        }
        //Debug.Log(rotate + " " + timeLight2D.colorLerp + " " + GameManager.instance.dayNightRatio);
        clockImageRect.localEulerAngles = new Vector3(0, 0, rotate);
    }
}
