using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class JobListButn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI textPro;
    [SerializeField] private UIJobComment uiJobComment;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiJobComment.AppearComment(butnNum);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uiJobComment.AppearComment(-100);
    }
}
