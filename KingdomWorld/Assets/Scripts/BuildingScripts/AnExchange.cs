using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class AnExchange : MonoBehaviour
{
    public GameObject AnExchangeUI;
    public Transform AnExchangeBP;
    public GameObject AnExchangeB;
    public GameObject CellBtns;
    public GameObject ExitBOn;
    public GameObject CellThings;
    public GameObject BuyThings;
    public GameObject NegoBtns;
    public GameObject NegoSder;
    public GameObject SMassage;
    public GameObject IsOpenImage;
    public GameObject ChatViewer;

    private float distance = 50f;

    public int ResourceCount;
    public int ResourceCount_Buy;
    public int RandomOpen;

    int Value = 1;
    int CValue;
    int BValue;
    int IsNegoNum;

    bool IsOpen;
    bool isLerp = false;
    bool IsApear = false;
    bool isNego = false;

    Vector3 CellBtnPosition;
    Vector3 ExitBtnPosition;
    Vector3 NegoBtnPosition;
    Vector3 NegoSderPositon;
    Vector3 ChatViewerPosition;

    public TextMeshProUGUI SelectedRToCell;
    public TextMeshProUGUI SelectedRToBuy;
    public TMP_InputField BuyRNum;
    public TMP_InputField CellRNum;

    public List<Sprite> ResourceSprite = new List<Sprite>();

    public Image RSpriteToCell;
    public Image RSpriteToBuy;

    float ExchangeRate = 100f;

    public Slider MySlider;
    public Slider CellerSlider;

    public Button NegoBtn;

    private void Awake()
    {
        IsOpenImage.SetActive(false);

        PositionSet();

        // �ӽ� �׽�Ʈ��. �׽�Ʈ ������ �����ߵ�!!
        AnExchangeUI.SetActive(true);
        isLerp = true;
    }

    private void Update()
    {
        BtnLerp();

        CellThingsLerp();

        NegoGageCtrl();
    }

    private void FixedUpdate()
    {
        BuildFound();

        RIsOpen();

        if (AnExchangeB != null)
        {
            OpenState();
            ClickCheck();
        }
        else
        {
            return;
        }
    }

    void NegoGageCtrl()
    {
        if (isNego == true)
        {
            MySlider.value = MySlider.value + (float)IsNegoNum / 10f;
            CellerSlider.value = CellerSlider.value - ((float)IsNegoNum - 1f) / 10f;

            if (MySlider.value >= 1)
            {
                NegoBtn.interactable = false;
            }
            else
            {
                NegoBtn.interactable = true;
            }

            isNego = false;
        }
    }

    void PositionSet()
    {
        CellBtnPosition = CellBtns.GetComponent<RectTransform>().anchoredPosition3D;
        ExitBtnPosition = ExitBOn.GetComponent<RectTransform>().anchoredPosition3D;
        NegoBtnPosition = NegoBtns.GetComponent<RectTransform>().anchoredPosition3D;
        NegoSderPositon = NegoSder.GetComponent<RectTransform>().anchoredPosition3D;
        ChatViewerPosition = ChatViewer.GetComponent<RectTransform>().anchoredPosition3D;
    }

    public void CellResourcesGet(int RNum)
    {
        RSpriteToCell.sprite = ResourceSprite[RNum];

        Value = RNum + 1;
        CValue = RNum;

        switch (RNum)
        {
            //��, �ķ�, ����, ����, ����, ��ȭ, ö����, ����ö, ��, ��, ġ��, ����, ��
            case 0:
                SelectedRToCell.text = "��";
                break;
            case 1:
                SelectedRToCell.text = "�ķ�";
                break;
            case 2:
                SelectedRToCell.text = "����";
                break;
            case 3:
                SelectedRToCell.text = "����";
                break;
            case 4:
                SelectedRToCell.text = "����";
                break;
            case 5:
                SelectedRToCell.text = "��ȭ";
                break;
            case 6:
                SelectedRToCell.text = "ö����";
                break;
            case 7:
                SelectedRToCell.text = "����ö";
                break;
            case 8:
                SelectedRToCell.text = "��";
                break;
            case 9:
                SelectedRToCell.text = "��";
                break;
            case 10:
                SelectedRToCell.text = "ġ��";
                break;
            case 11:
                SelectedRToCell.text = "����";
                break;
            case 12:
                SelectedRToCell.text = "��";
                break;
            default:
                break;
        }
    }

    public void ButResourcesGet(int RNum)
    {
        RSpriteToBuy.sprite = ResourceSprite[RNum];

        Value = RNum + 1;
        BValue = RNum;

        switch (RNum)
        {
            //��, �ķ�, ����, ����, ����, ��ȭ, ö����, ����ö, ��, ��, ġ��, ����, ��
            case 0:
                SelectedRToBuy.text = "��";
                break;
            case 1:
                SelectedRToBuy.text = "�ķ�";
                break;
            case 2:
                SelectedRToBuy.text = "����";
                break;
            case 3:
                SelectedRToBuy.text = "����";
                break;
            case 4:
                SelectedRToBuy.text = "����";
                break;
            case 5:
                SelectedRToBuy.text = "��ȭ";
                break;
            case 6:
                SelectedRToBuy.text = "ö����";
                break;
            case 7:
                SelectedRToBuy.text = "����ö";
                break;
            case 8:
                SelectedRToBuy.text = "��";
                break;
            case 9:
                SelectedRToBuy.text = "��";
                break;
            case 10:
                SelectedRToBuy.text = "ġ��";
                break;
            case 11:
                SelectedRToBuy.text = "����";
                break;
            case 12:
                SelectedRToBuy.text = "��";
                break;
            default:
                break;
        }
    }

    void BtnLerp()
    {
        if (isLerp == true)
        {
            CellBtns.GetComponent<RectTransform>().anchoredPosition3D =
            Vector3.Lerp(CellBtns.GetComponent<RectTransform>().anchoredPosition3D,
            new Vector3(-520, 0, 0), Time.deltaTime * 4f);

            ExitBOn.GetComponent<RectTransform>().anchoredPosition3D =
                 Vector3.Lerp(ExitBOn.GetComponent<RectTransform>().anchoredPosition3D,
                 new Vector3(520, 0, 0), Time.deltaTime * 4f);
        }
        else
        {
            CellBtns.GetComponent<RectTransform>().anchoredPosition3D =
           Vector3.Lerp(CellBtns.GetComponent<RectTransform>().anchoredPosition3D,
           CellBtnPosition, Time.deltaTime * 4f);

            ExitBOn.GetComponent<RectTransform>().anchoredPosition3D =
                 Vector3.Lerp(ExitBOn.GetComponent<RectTransform>().anchoredPosition3D,
                 CellBtnPosition, Time.deltaTime * 4f);
        }
    }

    public void ExitBtnOn()
    {
        IsApear = false;

        isLerp = false;

        AnExchangeUI.SetActive(false);
    }

    public void CellBClicked()
    {
        if (IsApear == true)
        {
            IsApear = false;
        }
        else
        {
            IsApear = true;
        }
    }

    void CellThingsLerp()
    {
        if (IsApear == true)
        {
            CellThings.GetComponent<RectTransform>().anchoredPosition3D =
           Vector3.Lerp(CellThings.GetComponent<RectTransform>().anchoredPosition3D,
           new Vector3(-420, 0, 0), Time.deltaTime * 4f);

            BuyThings.GetComponent<RectTransform>().anchoredPosition3D =
                   Vector3.Lerp(BuyThings.GetComponent<RectTransform>().anchoredPosition3D,
                   new Vector3(420, 0, 0), Time.deltaTime * 4f);

            NegoBtns.GetComponent<RectTransform>().anchoredPosition3D =
                  Vector3.Lerp(NegoBtns.GetComponent<RectTransform>().anchoredPosition3D,
                  new Vector3(0, -270, 0), Time.deltaTime * 4f);

            NegoSder.GetComponent<RectTransform>().anchoredPosition3D =
                 Vector3.Lerp(NegoSder.GetComponent<RectTransform>().anchoredPosition3D,
                 new Vector3(0, 0, 0), Time.deltaTime * 4f);

            ChatViewer.GetComponent<RectTransform>().anchoredPosition3D =
                 Vector3.Lerp(ChatViewer.GetComponent<RectTransform>().anchoredPosition3D,
                 new Vector3(0, 0, 0), Time.deltaTime * 4f);
        }
        else
        {
            CellThings.GetComponent<RectTransform>().anchoredPosition3D =
            Vector3.Lerp(CellThings.GetComponent<RectTransform>().anchoredPosition3D,
            CellBtnPosition, Time.deltaTime * 4f);

            BuyThings.GetComponent<RectTransform>().anchoredPosition3D =
                   Vector3.Lerp(BuyThings.GetComponent<RectTransform>().anchoredPosition3D,
                   ExitBtnPosition, Time.deltaTime * 4f);

            NegoBtns.GetComponent<RectTransform>().anchoredPosition3D =
                  Vector3.Lerp(NegoBtns.GetComponent<RectTransform>().anchoredPosition3D,
                  NegoBtnPosition, Time.deltaTime * 4f);

            NegoSder.GetComponent<RectTransform>().anchoredPosition3D =
                 Vector3.Lerp(NegoSder.GetComponent<RectTransform>().anchoredPosition3D,
                 NegoSderPositon, Time.deltaTime * 4f);

            ChatViewer.GetComponent<RectTransform>().anchoredPosition3D =
                 Vector3.Lerp(ChatViewer.GetComponent<RectTransform>().anchoredPosition3D,
                 ChatViewerPosition, Time.deltaTime * 4f);
        }
    }

    void ClickCheck()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray, distance);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.GetComponent<BuildingColider>() != null)
                {
                    if (hits[i].collider.gameObject.tag.Equals("AnExchange") && hits[i].collider.GetComponent<BuildingColider>().isSettingComplete)
                    {
                        if (IsOpen == true)
                        {
                            AnExchangeUI.SetActive(true);

                            isLerp = true;
                        }
                        else
                        {
                            AnExchangeUI.SetActive(false);

                            isLerp = false;
                        }
                    }
                }
            }
        }
    }

    void BuildFound()
    {
        for (int i = 0; i < AnExchangeBP.transform.childCount; i++)
        {
            if (AnExchangeBP.transform.GetChild(i).tag.Equals("AnExchange"))
            {
                AnExchangeB = AnExchangeBP.transform.GetChild(i).gameObject;
            }
            else
            {
                return;
            }
        }
    }

    void RIsOpen()
    {
        if (GameManager.instance.dayNightRatio == 1f || GameManager.instance.dayNightRatio == 0f)
        {
            RandomOpen = Random.Range(0, 2);
        }
    }

    void OpenState()
    {
        if (GameManager.instance.isDaytime == true)
        {
            if (RandomOpen == 0)
            {
                IsOpen = true;

                RandomExchangeRate();

                IsOpenImage.gameObject.SetActive(true);

                IsOpenImage.transform.position = new Vector3(AnExchangeB.transform.position.x + 1.5f, 
                    AnExchangeB.transform.position.y, AnExchangeB.transform.position.z + 1.5f);

                MySlider.value = 0;
                CellerSlider.value = 0;
            }
            else
            {
                IsOpen = false;

                IsOpenImage.gameObject.SetActive(false);
            }
        }
        else
        {
            IsOpen = false;

            IsOpenImage.gameObject.SetActive(false);
        }
    }

    public void GetQuantity(string value)
    {
        ResourceCount = int.Parse (value);

        Debug.Log(ResourceCount);

        BuyRNum.text = (ResourceCount * (ExchangeRate / 100) * Value).ToString();

        ResourceCount = (int)(ResourceCount * (100 / ExchangeRate) * Value);
    }

    public void GetQuantityBuy(string value)
    {
        ResourceCount_Buy = int.Parse(value);

        Debug.Log(ResourceCount_Buy);

        CellRNum.text = (ResourceCount_Buy * (ExchangeRate / 100) * Value).ToString();

        ResourceCount_Buy = (int)(ResourceCount_Buy * (ExchangeRate / 100) * Value);
    }

    public void CellResources()
    {
        bool CellCheck = false;

        switch (CValue)
        {
            //��, �ķ�, ����, ����, ����, ��ȭ, ö����, ����ö, ��, ��, ġ��, ����, ��
            case 0:
                if (ResourceCount >= GameManager.instance.Wheat)
                {
                    GameManager.instance.Wheat -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            case 1:
                if (ResourceCount >= GameManager.instance.Food)
                {
                    GameManager.instance.Food -= ResourceCount;
                    CellCheck = true;
                }
                break;
            case 2:
                if (ResourceCount >= GameManager.instance.Wood)
                {
                    GameManager.instance.Wood -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            case 3:
                if (ResourceCount >= GameManager.instance.Meat)
                {
                    GameManager.instance.Meat -= ResourceCount;
                    CellCheck = true;
                }
                break;
            case 4:
                if (ResourceCount >= GameManager.instance.Leather)
                {
                    GameManager.instance.Leather -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            case 5:
                if (ResourceCount >= GameManager.instance.Gold)
                {
                    GameManager.instance.Gold -= ResourceCount;
                    CellCheck = true;
                }
                break;
            case 6:
                if (ResourceCount >= GameManager.instance.Itronstone)
                {
                    GameManager.instance.Itronstone -= ResourceCount;
                    CellCheck = true;
                }
                break;
            case 7:
                if (ResourceCount >= GameManager.instance.CastIron)
                {
                    GameManager.instance.CastIron -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            case 8:
                if (ResourceCount >= GameManager.instance.Cow)
                {
                    GameManager.instance.Cow -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            case 9:
                if (ResourceCount >= GameManager.instance.Sheep)
                {
                    GameManager.instance.Sheep -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            case 10:
                if (ResourceCount >= GameManager.instance.Cheese)
                {
                    GameManager.instance.Cheese -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            case 11:
                if (ResourceCount >= GameManager.instance.Fleece)
                {
                    GameManager.instance.Fleece -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            case 12:
                if (ResourceCount >= GameManager.instance.Cloth)
                {
                    GameManager.instance.Cloth -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            default:
                CellCheck = false;
                break;
        }

        if (CellCheck == true)
        {
            switch (BValue)
            {
                //��, �ķ�, ����, ����, ����, ��ȭ, ö����, ����ö, ��, ��, ġ��, ����, ��
                case 0:
                    GameManager.instance.Wheat += ResourceCount_Buy;
                    break;
                case 1:
                    GameManager.instance.Food += ResourceCount_Buy;
                    break;
                case 2:
                    GameManager.instance.Wood += ResourceCount_Buy;
                    break;
                case 3:
                    GameManager.instance.Meat += ResourceCount_Buy;
                    break;
                case 4:
                    GameManager.instance.Leather += ResourceCount_Buy;
                    break;
                case 5:
                    GameManager.instance.Gold += ResourceCount_Buy;
                    break;
                case 6:
                    GameManager.instance.Itronstone += ResourceCount_Buy;
                    break;
                case 7:
                    GameManager.instance.CastIron += ResourceCount_Buy;
                    break;
                case 8:
                    GameManager.instance.Cow += ResourceCount_Buy;
                    break;
                case 9:
                    GameManager.instance.Sheep += ResourceCount_Buy;
                    break;
                case 10:
                    GameManager.instance.Cheese += ResourceCount_Buy;
                    break;
                case 11:
                    GameManager.instance.Fleece += ResourceCount_Buy;
                    break;
                case 12:
                    GameManager.instance.Cloth += ResourceCount_Buy;
                    break;
                default:
                    break;
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //RaycastResult : BaseRaycastModule������ ��Ʈ ���.
        List<RaycastResult> results = new List<RaycastResult>();
        //EventSystem.current�� �ֱٿ� �߻��� �̺�Ʈ �ý����� ��ȯ�Ѵ�.
        //ù��° ���ڰ� : ���� ������ ������.
        //�ι�° ���ڰ� : List of 'hits' to populate.
        //RaycastAll : ��� ������ BaseRaycaster�� ����� ���� �ش� �������� ���� ĳ����.
        // -> �����ִ� ������Ʈ���� �ִٸ� �����ִ� ���� results�� ī��Ʈ�� �ٲ�
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        //Debug.Log(results.Count);
        return results.Count > 0;
    }

    float RandomExchangeRate()
    {
        ExchangeRate = 100;

        Random.Range(0, 200);

        return ExchangeRate;
    }

    int IsNegoOn(int Num)
    {
        isNego = true;
        IsNegoNum = Num;

        if(Num == 0)
        {
            ExchangeRate *= 1.1f;
        }
        else
        {
            ExchangeRate *= 0.9f;
        }

        return IsNegoNum;
    }
}