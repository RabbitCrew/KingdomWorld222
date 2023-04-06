using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPanelOpenUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttonObj = new List<GameObject>();
    [SerializeField] private GameObject buildingButnObj;
    
    private List<RectTransform> buttonTrans = new List<RectTransform>();
    private List<Image> buttonImage = new List<Image>();
    private List<BuildingButtonAlpha> buttonAlpha = new List<BuildingButtonAlpha>();
    private bool isOpen = false;
    private bool isMove = true;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttonObj.Count; i++)
        {
            buttonTrans.Add(buttonObj[i].GetComponent<RectTransform>());
            buttonImage.Add(buttonObj[i].GetComponent<Image>());
            buttonAlpha.Add(buttonObj[i].GetComponent<BuildingButtonAlpha>());
            buttonImage[i].color = new Color(1f, 1f, 1f, 0f);
        }
    }

    public void MoveButton()
    {
        if (!isOpen)
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }

        for (int i = 0; i < buttonAlpha.Count; i++)
        {
            buttonAlpha[i].isAlpha = true;
        }

        isMove = true;
    }

    public void Update()
    {
        if (isOpen && isMove)
        {
            for (int i = 0; i < buttonObj.Count; i++)
            {
                buttonImage[i].color = Color.Lerp(buttonImage[i].color, new Color(1f, 1f, 1f, 1f), Time.deltaTime * 5f);

                if ((i * 80 - 922) - buttonTrans[i].localPosition.x < 1)
                {
                    buttonTrans[i].localPosition = new Vector3(i * 80 - 922, -12, 0);
                    continue;
                }
                int ran = Random.Range(-19, 20);
                buttonTrans[i].localPosition = Vector3.Lerp(buttonTrans[i].localPosition, new Vector3(i * 80 - 922,-12 + ran,0), Time.deltaTime * 5f);
            }

            isMove = false;
            
            for (int i = 0; i < buttonObj.Count; i++)
            {
                if (buttonTrans[i].localPosition.x != i * 80 - 922)
                {
                    isMove = true;
                }
            }
        }

        if (!isOpen && isMove)
        {
            for (int i = 0; i < buttonObj.Count; i++)
            {
                buttonImage[i].color = Color.Lerp(buttonImage[i].color, new Color(1f, 1f, 1f, 0f), Time.deltaTime * 5f);

                if (buttonTrans[i].localPosition.x + 922 < 1)
                {
                    buttonTrans[i].localPosition = new Vector3(- 922, -12, 0);
                    continue;
                }

                int ran = Random.Range(-19, 20);
                buttonTrans[i].localPosition = Vector3.Lerp(buttonTrans[i].localPosition, new Vector3(- 922, -12 + ran, 0), Time.deltaTime * 5f);

            }

            isMove = false;

            for (int i = 0; i < buttonObj.Count; i++)
            {
                if (buttonTrans[i].localPosition.x != -922)
                {
                    isMove = true;
                }
            }

            if (!isMove)
            {
                buildingButnObj.SetActive(false);
            }
        }
    }
}
