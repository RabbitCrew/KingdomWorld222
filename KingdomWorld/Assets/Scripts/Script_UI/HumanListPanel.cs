using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanListPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] humanStatePanelObjArr;
    [SerializeField] private MouseRay mouseRay;

    private HumanStatePanel[] humanStatePanelArr;
    private int nowPage;

    private void Awake()
    {
        nowPage = 1;
        humanStatePanelArr = new HumanStatePanel[humanStatePanelObjArr.Length];
        for (int i = 0; i < humanStatePanelObjArr.Length; i++)
        {
            humanStatePanelArr[i] = humanStatePanelObjArr[i].GetComponent<HumanStatePanel>();
            humanStatePanelArr[i].SetMouseRay(mouseRay);
        }
    }

    private void Start()
    {
    }

    public void ChangePage(int i)
    {
        if (i > 0 && nowPage < (GameManager.instance.AllHuman.Count / 10) + 1)
        {
            nowPage++;
        }
        else if (i < 0 && nowPage > 1)
        {
            nowPage--;
        }
        UpdateHumanList();
    }

    public void ChangeMaxLeftPage()
    {
        nowPage = 1;
    }

    public void ChangeMaxRightPage()
    {
        nowPage = (GameManager.instance.AllHuman.Count / 10) + 1;
    }

    void OnEnable()
    {
        StartCoroutine(UpdateHumanInfo());
    }

    public void UpdateHumanList()
    {
        for (int i = (nowPage-1) * 10; i < nowPage * 10; i++)
        {
            if (i > GameManager.instance.AllHuman.Count - 1)
            {
                humanStatePanelArr[i].SetHuman(null);
            }
            else
            {
                humanStatePanelArr[i].SetHuman(GameManager.instance.AllHuman[i]);
            }
        }
    }

    IEnumerator UpdateHumanInfo()
    {
        var one = new WaitForSeconds(1f);

        while(true)
        {
            UpdateHumanList();
            yield return one;
        }
    }
}
