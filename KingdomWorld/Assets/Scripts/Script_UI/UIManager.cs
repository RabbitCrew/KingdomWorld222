using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform jobChangeUI;

    public bool isOpenCitizenPanel { get; set; }

    private Vector3 openJobChangeUIVec;
    private Vector3 closeJobChangeUIVec;
    // Start is called before the first frame update
    void Awake()
    {
        isOpenCitizenPanel = false;

        openJobChangeUIVec = new Vector3(-760f, 100f, 0f);
        closeJobChangeUIVec = new Vector3(-1160f, 100f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpenCitizenPanel && openJobChangeUIVec.x != jobChangeUI.localPosition.x)
        {
            jobChangeUI.localPosition = Vector3.Lerp(jobChangeUI.localPosition, openJobChangeUIVec, Time.deltaTime * 5f);
            if (openJobChangeUIVec.x - jobChangeUI.localPosition.x < 1f)
            {
                jobChangeUI.localPosition = openJobChangeUIVec;
            }
        }
        else if (!isOpenCitizenPanel && closeJobChangeUIVec.x != jobChangeUI.localPosition.x)
        {
            jobChangeUI.localPosition = Vector3.Lerp(jobChangeUI.localPosition, closeJobChangeUIVec, Time.deltaTime * 5f);
            if (jobChangeUI.localPosition.x - closeJobChangeUIVec.x < 1f)
            {
                jobChangeUI.localPosition = closeJobChangeUIVec;
            }
        }
    }
}
