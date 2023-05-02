using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class AnExchange : MonoBehaviour
{
    public GameObject AnExchangeUI;
    public GameObject UIImage;
    public Transform AnExchangeBP;
    public GameObject AnExchangeB;
    public GameObject CellBtns;
    public GameObject ExitBOn;
    public GameObject CellThings;
    public GameObject BuyThings;
    public GameObject NegoBtns;
    public GameObject NegoSder;
    public GameObject SMassage;
    public GameObject IsOpenImageObj;
    public GameObject ChatViewer;
    public GameObject TChatViewer;
    public GameObject TNegoSder;
    public GameObject TCellBtns;

    private float distance = 50f;
    public float ExchangeRate = 100f;

    public int ResourceCount;
    public int ResourceCount_Buy;
    public int RandomOpen;

    int Value = 1;
    int CValue;
    int BValue;
    int IsNegoNum;

    public bool IsOpen;
    bool isLerp = false;
    bool IsApear = false;
    bool IsTApear = false;
    bool isNego = false;
    bool isClickOpenMark = false;

    Vector3 CellBtnPosition;
    Vector3 ExitBtnPosition;
    Vector3 NegoBtnPosition;
    Vector3 NegoSderPositon;
    Vector3 ChatViewerPosition;
    Vector3 AnExchangeUIPos;

    public TextMeshProUGUI SelectedRToCell;
    public TextMeshProUGUI SelectedRToBuy;
    public TMP_InputField BuyRNum;
    public TMP_InputField CellRNum;

    public List<Sprite> ResourceSprite = new List<Sprite>();
    public Sprite[] isOpenSprite;

    public Image RSpriteToCell;
    public Image RSpriteToBuy;
    public Image isOpenImage;
    
    public Slider MySlider;
    public Slider TMySlider;
    public Slider CellerSlider;
    public Slider TCellerSlider;

    public Button NegoBtn;
    public Button TNegoBtn;

    private void Awake()
    {
        IsOpenImageObj.SetActive(false);//���������� ǥ�����ִ� �̹���. �����Ҷ� ����
                                        // IsOpenImage.SetActive(false);//���������� ǥ�����ִ� �̹���. �����Ҷ� ����

        AnExchangeUIPos = UIImage.GetComponent<RectTransform>().anchoredPosition3D;
    }

    private void Start()
    {
        PositionSet(); //���ü� �� ����

        //isLerp = true;
    }

    private void Update()
    {
        BtnLerp();

        CellThingsLerp();

        NegoGageCtrl();

        if (AnExchangeB != null) // �ŷ��Ұ� ��ġ�� �� �ŷ��Ҹ� ã���� ��
        {
            OpenState();
            ClickCheck();
        }
        else
        {
            return;
        }

        if (isClickOpenMark)
		{
            MoveTOAnExchange();
        }
    }

    private void FixedUpdate()
    {
        BuildFound();

        RIsOpen();
    }

    void NegoGageCtrl()
    {
        if (isNego == true)//�װ� �������� ��
        {
            MySlider.value = MySlider.value + 0.1f;//�����ϸ� �״�� �ΰ� ���� �� �������� �ö�
            TMySlider.value = TMySlider.value + 0.1f;
            CellerSlider.value = CellerSlider.value + 0.1f;
            TCellerSlider.value = TCellerSlider.value + 0.1f;

            if (MySlider.value >= 1)//�������� ������ �װ� ���ϰ� �ϰ�
            {
                NegoBtn.interactable = false;
                TNegoBtn.interactable = false;
            }
            else//�ƴϸ� ���
            {
                NegoBtn.interactable = true;
                TNegoBtn.interactable = true;
            }

            isNego = false;
        }
    }

    void PositionSet()// Lerp �� ��ġ ����
    {
        CellBtnPosition = CellBtns.GetComponent<RectTransform>().anchoredPosition3D;
        ExitBtnPosition = ExitBOn.GetComponent<RectTransform>().anchoredPosition3D;
        NegoBtnPosition = NegoBtns.GetComponent<RectTransform>().anchoredPosition3D;
        NegoSderPositon = NegoSder.GetComponent<RectTransform>().anchoredPosition3D;
        ChatViewerPosition = ChatViewer.GetComponent<RectTransform>().anchoredPosition3D;
    }

    public void CellResourcesGet(int RNum)// �Ű������� �ε��� �� �޾Ƽ� �� �ڿ� ����
    {
        RSpriteToCell.sprite = ResourceSprite[RNum];//���� �ε������� ���� ��������Ʈ ����

        Value = RNum + 1;//�ӽ÷� ���� ������
        CValue = RNum;//�ε����� ����

        switch (RNum)//���� �ε������� ���� �ؽ�Ʈ ����
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

    public void ButResourcesGet(int RNum)// �Ű������� �ε��� �� �޾Ƽ� �� �ڿ� ����
    {
        RSpriteToBuy.sprite = ResourceSprite[RNum];

        Value = RNum + 1;//�ӽ÷� ���� ����
        BValue = RNum;//�ε����� ����

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
        float DefaultLerpTime = 4f;

        if (isLerp == true)
        {
            UIImage.GetComponent<RectTransform>().anchoredPosition3D =
           Vector3.Lerp(UIImage.GetComponent<RectTransform>().anchoredPosition3D,
           new Vector3(0, 0, 0), Time.deltaTime * DefaultLerpTime);//������������ ���ܵ״� UI�� ��ǥ��ġ���� ������ �̵��ؼ� ��Ÿ���� ��

            if (AnExchangeUI.GetComponent<RectTransform>().anchoredPosition3D == new Vector3(0, 0, 0))
            {
                CellBtns.GetComponent<RectTransform>().anchoredPosition3D =
                Vector3.Lerp(CellBtns.GetComponent<RectTransform>().anchoredPosition3D,
                new Vector3(-520, 0, 0), Time.deltaTime * DefaultLerpTime);//������������ ���ܵ״� UI�� ��ǥ��ġ���� ������ �̵��ؼ� ��Ÿ���� ��

                ExitBOn.GetComponent<RectTransform>().anchoredPosition3D =
                     Vector3.Lerp(ExitBOn.GetComponent<RectTransform>().anchoredPosition3D,
                     new Vector3(520, 0, 0), Time.deltaTime * DefaultLerpTime);
            }
        }
        else
        {
            CellBtns.GetComponent<RectTransform>().anchoredPosition3D =
           Vector3.Lerp(CellBtns.GetComponent<RectTransform>().anchoredPosition3D,
           CellBtnPosition, Time.deltaTime * DefaultLerpTime);//�����ص״� ��ġ�� �ٽ� ����

            ExitBOn.GetComponent<RectTransform>().anchoredPosition3D =
                 Vector3.Lerp(ExitBOn.GetComponent<RectTransform>().anchoredPosition3D,
                 ExitBtnPosition, Time.deltaTime * DefaultLerpTime);

            UIImage.GetComponent<RectTransform>().anchoredPosition3D =
          Vector3.Lerp(UIImage.GetComponent<RectTransform>().anchoredPosition3D,
         AnExchangeUIPos, Time.deltaTime * DefaultLerpTime);

            if (UIImage.GetComponent<RectTransform>().anchoredPosition3D == AnExchangeUIPos)
            {
                AnExchangeUI.SetActive(false);
            }
        }
    }

    public void ExitBtnOn()// ������ ��ư���� ������ ���� ����
    {
        ChatViewer.SendMessage("ListReset");

        RandomOpen = 5;

        IsApear = false;
        IsTApear = false;

        isLerp = false;
    }

    public void CellBClicked()// �ŷ� ��ư ���� �� �ش�Ǵ� �����̸� ������ �ϱ� ���� bool������ üũ���� //���� ������ ���� �� ������ ������.
    {
        if (IsApear == true)
        {
            IsApear = false;
        }
        else
        {
            IsApear = true;
            IsTApear = false;
        }
    }

    public void TBClicked()// �����ŷ� UI
    {
        if (IsTApear == true)
        {
            IsTApear = false;
        }
        else
        {
            IsTApear = true;
            IsApear = false;
        }
    }

    void CellThingsLerp()
    {
        if (IsApear == true)//�ŷ� ���� UI ������������ ������
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
                 new Vector3(0, 80, 0), Time.deltaTime * 4f);
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

        if (IsTApear == true)
        {
            TChatViewer.GetComponent<RectTransform>().anchoredPosition3D =
           Vector3.Lerp(TChatViewer.GetComponent<RectTransform>().anchoredPosition3D,
           new Vector3(0, 350, 0), Time.deltaTime * 4f);

            TNegoSder.GetComponent<RectTransform>().anchoredPosition3D =
                   Vector3.Lerp(TNegoSder.GetComponent<RectTransform>().anchoredPosition3D,
                   new Vector3(0, 570, 0), Time.deltaTime * 4f);

            TCellBtns.GetComponent<RectTransform>().anchoredPosition3D =
                  Vector3.Lerp(TCellBtns.GetComponent<RectTransform>().anchoredPosition3D,
                  new Vector3(0, 0, 0), Time.deltaTime * 4f);
        }
        else
        {
            TChatViewer.GetComponent<RectTransform>().anchoredPosition3D =
          Vector3.Lerp(TChatViewer.GetComponent<RectTransform>().anchoredPosition3D,
          new Vector3(0, 1000, 0), Time.deltaTime * 4f);

            TNegoSder.GetComponent<RectTransform>().anchoredPosition3D =
                   Vector3.Lerp(TNegoSder.GetComponent<RectTransform>().anchoredPosition3D,
                   new Vector3(0, 690, 0), Time.deltaTime * 4f);

            TCellBtns.GetComponent<RectTransform>().anchoredPosition3D =
                  Vector3.Lerp(TCellBtns.GetComponent<RectTransform>().anchoredPosition3D,
                  NegoBtnPosition, Time.deltaTime * 4f);
        }
    }

    void ClickCheck()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);// ray�� ���콺 ������ �� ���콺 ��ġ �޾ƿ�

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray, distance);

            for (int i = 0; i < hits.Length; i++) // ���̷� Ŭ���� �κ��� ������Ʈ ������
            {
                if (hits[i].collider.GetComponent<BuildingColider>() != null) // �ǹ��� �Ҵ��� �ݶ��̴��� ���� ��
                {
                    if (hits[i].collider.gameObject.tag.Equals("AnExchange") && hits[i].collider.GetComponent<BuildingColider>().isSettingComplete) // �±׷� AnExchange�� �޷�������
                    {
                        if (IsOpen == true) // �ŷ��Ұ� ������
                        {
                            AnExchangeUI.SetActive(true); // UI���ְ�

                            isLerp = true; // �������� (��ư)

                            MySlider.value = 0;
                            CellerSlider.value = 0;

                            RandomExchangeRate();
                        }
                        else
                        {
                            AnExchangeUI.SetActive(false); //�ƴҽ� ���ֱ�

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
            Debug.Log(AnExchangeBP.transform.GetChild(i).tag);
            if (AnExchangeBP.transform.GetChild(i).tag.Equals("AnExchange")) // �ǹ� ���� ������Ʈ ������ �ִ� ������Ʈ �� �±װ� AnExchange�� �ǹ��� ������ 
            {
                AnExchangeB = AnExchangeBP.transform.GetChild(i).gameObject; // ���ӿ�����Ʈ�� ����
                return;
                //Debug.Log("��ã�� ����?");
            }
            else
            {
                //Debug.Log("���ϵǴ� ����?");
                continue; // ���� �� ����
            }
        }
    }

    void RIsOpen()
    {
        if (GameManager.instance.dayNightRatio == 1f || GameManager.instance.dayNightRatio == 0f)
        {
            RandomOpen = Random.Range(0, 1);// 7���� 1 Ȯ�� ��í!! �������� �ְ�~ �ƴҼ��� �ְ�~
        }
    }

    void OpenState()
    {
        if (GameManager.instance.isDaytime == true)//���̸�
        {
            if (RandomOpen == 0)//�������� ������ ���� ������
            {
                IsOpen = true; //������

                if (AnExchangeB != null && AnExchangeB.activeSelf)
                {
                    IsOpenImageObj.gameObject.SetActive(true);//�˸� �̹��� Ű��
                }
                float x = GameManager.instance.uiSizeX / Camera.main.scaledPixelWidth;
                float y = GameManager.instance.uiSizeY / Camera.main.scaledPixelHeight;

                Vector3 vec = Camera.main.WorldToScreenPoint(AnExchangeB.transform.position + new Vector3(1.5f,0f,1.6f));

                float vecX = 0f;
                float vecY = 0f;

                bool isX = false;
                bool isY = false;

                if (vec.x * x < 330f) { vecX = 330f; isX = false; }
                else if (vec.x * x > 1610f) { vecX = 1610f; isX = false; }
                else { vecX = vec.x * x; isX = true; }

                if (vec.y * y < 180f) { vecY = 180f; isY = false; }
                else if (vec.y * y > 900f) { vecY = 900f; isY = false; }
                else { vecY = vec.y * y; isY = true; }

                if (isX && isY) { isOpenImage.sprite = isOpenSprite[0]; }
                else { isOpenImage.sprite = isOpenSprite[1]; }
                IsOpenImageObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(vecX, vecY, vec.z);

                MySlider.value = 0;
                CellerSlider.value = 0;
            }
            else//0�� �ƴϸ� ������ �ݱ�
            {
                IsOpen = false;

                IsOpenImageObj.gameObject.SetActive(false);//�˸� �̹��� ��
            }
        }
        else//���̸� ������ �ݱ�
        {
            IsOpen = false;

            IsOpenImageObj.gameObject.SetActive(false);
        }
    }

    public void ClickOpenMark()
	{
        isClickOpenMark = true;
	}

    private void MoveTOAnExchange()
    {
        if (Vector3.SqrMagnitude(Camera.main.transform.position - (AnExchangeB.transform.position + new Vector3(0f, 40f, 0f))) > 3f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, AnExchangeB.transform.position + new Vector3(0f,40f,0f), Time.deltaTime * 5f);
        }
        else
		{
            Camera.main.transform.position = AnExchangeB.transform.position + new Vector3(0f, 40f, 0f);
            isClickOpenMark = false;
        }
    }

    public void GetQuantity(string value) // �� ���� ���� �޾Ƽ� ȯ�� �����ؼ� �� ���� ���� ����ϰ� ǥ��.
    {
        ResourceCount = int.Parse (value);

        ResourceCount = (int)(ResourceCount * (100 / ExchangeRate) * Value);

        BuyRNum.text = ResourceCount.ToString();
    }

    public void GetQuantityBuy(string value)//�� ���� ���� �޾Ƽ� ȯ�� �����ؼ� �� ���� ���� ����ϰ� ǥ��
    {
        ResourceCount_Buy = int.Parse(value);

        ResourceCount_Buy = (int)(ResourceCount_Buy * (ExchangeRate / 100) * Value);

        CellRNum.text = ResourceCount_Buy.ToString();
    }

    public void CellResources()
    {
        bool CellCheck = false;

        // �� ���� ���� ���� �ͺ��� ������ â���� �Ҹ�, bool�� üũ���༭
        switch (CValue)
        {
            //��, �ķ�, ����, ����, ����, ��ȭ, ö����, ����ö, ��, ��, ġ��, ����, ��
            case 0:
                if (ResourceCount <= GameManager.instance.Wheat)
                {
                    GameManager.instance.Wheat -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            case 1:
                if (ResourceCount <= GameManager.instance.Food)
                {
                    GameManager.instance.Food -= ResourceCount;
                    CellCheck = true;
                }
                break;
            case 2:
                if (ResourceCount <= GameManager.instance.Wood)
                {
                    GameManager.instance.Wood -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            case 3:
                if (ResourceCount <= GameManager.instance.Meat)
                {
                    GameManager.instance.Meat -= ResourceCount;
                    CellCheck = true;
                }
                break;
            case 4:
                if (ResourceCount <= GameManager.instance.Leather)
                {
                    GameManager.instance.Leather -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            case 5:
                if (ResourceCount <= GameManager.instance.Gold)
                {
                    GameManager.instance.Gold -= ResourceCount;
                    CellCheck = true;
                }
                break;
            case 6:
                if (ResourceCount <= GameManager.instance.Itronstone)
                {
                    GameManager.instance.Itronstone -= ResourceCount;
                    CellCheck = true;
                }
                break;
            case 7:
                if (ResourceCount <= GameManager.instance.CastIron)
                {
                    GameManager.instance.CastIron -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            case 8:
                if (ResourceCount <= GameManager.instance.Cow)
                {
                    GameManager.instance.Cow -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            case 9:
                if (ResourceCount <= GameManager.instance.Sheep)
                {
                    GameManager.instance.Sheep -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            case 10:
                if (ResourceCount <= GameManager.instance.Cheese)
                {
                    GameManager.instance.Cheese -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            case 11:
                if (ResourceCount <= GameManager.instance.Fleece)
                {
                    GameManager.instance.Fleece -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            case 12:
                if (ResourceCount <= GameManager.instance.Cloth)
                {
                    GameManager.instance.Cloth -= ResourceCount;
                    CellCheck = true;
                }
                    break;
            default:
                CellCheck = false;
                break;
        }

        //bool�� üũ �������� �޾ƿ� ���� �޾ƿ���
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

            SMassage.SendMessage("MessageQ", "�������ּż� �����մϴ�!");
        }
        else
        {
            SMassage.SendMessage("MessageQ", "�ڿ��� �����մϴ�.");
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

    float RandomExchangeRate()//����ȯ��
    {
        ExchangeRate = Random.Range(0, 201);

        ExchangeRate += Inventory.instance.AddExchangeRate;

        return ExchangeRate;
    }

    int IsNegoOn(int Num)// �װ��û �� �� �װ� ������ ȯ�� ���� �ƴϸ� ����
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