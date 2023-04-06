using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SVScrollBttm : MonoBehaviour
{
    public GameObject NegoTextMama;
    public GameObject ExchangeUI;

    GameObject NegoT;

    public TextMeshProUGUI NegoText;

    public string[] NegoMent;
    public string[] Thanks;
    public string[] NegoAnswer;

    public List<GameObject> NegoTexts = new List<GameObject>();
    public List<Vector3> vecList = new List<Vector3>();
    int Rancount;

    public Button NegoButton;

    bool IsLerp;

    public ScrollRect ChatText = null;

    private void Update()
    {
        AutoScroll();
    }

    void AutoScroll()
    {
        if (IsLerp == true)
        {
            ChatText.verticalNormalizedPosition = 0.0f;
        }
    }

    public void TryNego()
    {
        NegoButton.interactable = false;

        NegoT = Instantiate(NegoText.gameObject, NegoTextMama.transform.position, Quaternion.identity, NegoTextMama.transform);
        NegoT.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        NegoT.gameObject.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -210, 0);

        NegoT.GetComponent<TextMeshProUGUI>().text = NegoMent[RandomNum(NegoMent.Length)];
        NegoT.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;

        NegoTexts.Add(NegoT);

        NegoMentPrint();

        IsLerp = true;
    }

    void NegoMentPrint()
    {
        GameObject NegoT = Instantiate(NegoText.gameObject, NegoTextMama.transform.position, Quaternion.identity, NegoTextMama.transform);
        NegoT.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        NegoT.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -210, 0);

        NegoT.GetComponent<TextMeshProUGUI>().text = NegoAnswer[RandomNum(NegoAnswer.Length)];
        NegoT.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;

        NegoTexts.Add(NegoT);

        NegoAnswerPrint();

        IsLerp = true;
    }

    void NegoAnswerPrint()
    {
        GameObject NegoT = Instantiate(NegoText.gameObject, NegoTextMama.transform.position, Quaternion.identity, NegoTextMama.transform);
        NegoT.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        NegoT.gameObject.transform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -210, 0);

        NegoTexts.Add(NegoT);

        ExchangeUI.SendMessage("IsNegoOn", Rancount % 2);

        if(Rancount % 2 == 0)
        {
            NegoT.GetComponent<TextMeshProUGUI>().text = Thanks[RandomEvenNum(Thanks.Length)];
        }
        else
        {
            NegoT.GetComponent<TextMeshProUGUI>().text = Thanks[RandomOddNum(Thanks.Length)];
        }

        NegoT.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;

        NegoButton.interactable = true;

        IsLerp = false;
    }

    int RandomNum(int Num)
    {
        Rancount = Random.Range(0, Num);

        return Rancount;
    }

    int RandomEvenNum(int Num)
    {
        Rancount = Random.Range(0, Num / 2);

        return Rancount * 2;
    }

    int RandomOddNum(int Num)
    {
        Rancount = Random.Range(1, Num);

        if (Rancount % 2 == 0)
        {
            return RandomOddNum(Num);
        }
        else
        {
            return Rancount;
        }
    }
}