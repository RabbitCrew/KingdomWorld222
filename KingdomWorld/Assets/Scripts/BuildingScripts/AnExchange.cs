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
    public GameObject TChatViewer;
    public GameObject TNegoSder;
    public GameObject TCellBtns;

    private float distance = 50f;
    float ExchangeRate = 100f;

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
    bool IsTApear = false;
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

    public Slider MySlider;
    public Slider CellerSlider;

    public Button NegoBtn;

    private void Awake()
    {
       // IsOpenImage.SetActive(false);//문열었는지 표시해주는 이미지. 시작할때 꺼줌

        PositionSet();
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

        if (AnExchangeB != null) // 거래소가 설치된 후 거래소를 찾았을 시
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
        if (isNego == true)//네고를 진행했을 때
        {
            MySlider.value = MySlider.value + 0.1f;//성공하면 그대로 두고 실패 시 게이지가 올라감
            CellerSlider.value = CellerSlider.value + 0.1f;//동문이하

            if (MySlider.value >= 1)//게이지가 꽉차면 네고 못하게 하고
            {
                NegoBtn.interactable = false;
            }
            else//아니면 계속
            {
                NegoBtn.interactable = true;
            }

            isNego = false;
        }
    }

    void PositionSet()// Lerp 전 위치 저장
    {
        CellBtnPosition = CellBtns.GetComponent<RectTransform>().anchoredPosition3D;
        ExitBtnPosition = ExitBOn.GetComponent<RectTransform>().anchoredPosition3D;
        NegoBtnPosition = NegoBtns.GetComponent<RectTransform>().anchoredPosition3D;
        NegoSderPositon = NegoSder.GetComponent<RectTransform>().anchoredPosition3D;
        ChatViewerPosition = ChatViewer.GetComponent<RectTransform>().anchoredPosition3D;
    }

    public void CellResourcesGet(int RNum)// 매개변수로 인덱스 값 받아서 팔 자원 지정
    {
        RSpriteToCell.sprite = ResourceSprite[RNum];//받은 인덱스값에 따라 스프라이트 지정

        Value = RNum + 1;//임시로 배율 지정함
        CValue = RNum;//인덱스값 저장

        switch (RNum)//받은 인덱스값에 따라 텍스트 지정
        {
            //밀, 식량, 나무, 육류, 가죽, 금화, 철광석, 주조철, 소, 양, 치즈, 양털, 옷
            case 0:
                SelectedRToCell.text = "밀";
                break;
            case 1:
                SelectedRToCell.text = "식량";
                break;
            case 2:
                SelectedRToCell.text = "나무";
                break;
            case 3:
                SelectedRToCell.text = "육류";
                break;
            case 4:
                SelectedRToCell.text = "가죽";
                break;
            case 5:
                SelectedRToCell.text = "금화";
                break;
            case 6:
                SelectedRToCell.text = "철광석";
                break;
            case 7:
                SelectedRToCell.text = "주조철";
                break;
            case 8:
                SelectedRToCell.text = "소";
                break;
            case 9:
                SelectedRToCell.text = "양";
                break;
            case 10:
                SelectedRToCell.text = "치즈";
                break;
            case 11:
                SelectedRToCell.text = "양털";
                break;
            case 12:
                SelectedRToCell.text = "옷";
                break;
            default:
                break;
        }
    }

    public void ButResourcesGet(int RNum)// 매개변수로 인덱스 값 받아서 살 자원 지정
    {
        RSpriteToBuy.sprite = ResourceSprite[RNum];

        Value = RNum + 1;//임시로 배율 지정
        BValue = RNum;//인덱스값 저장

        switch (RNum)
        {
            //밀, 식량, 나무, 육류, 가죽, 금화, 철광석, 주조철, 소, 양, 치즈, 양털, 옷
            case 0:
                SelectedRToBuy.text = "밀";
                break;
            case 1:
                SelectedRToBuy.text = "식량";
                break;
            case 2:
                SelectedRToBuy.text = "나무";
                break;
            case 3:
                SelectedRToBuy.text = "육류";
                break;
            case 4:
                SelectedRToBuy.text = "가죽";
                break;
            case 5:
                SelectedRToBuy.text = "금화";
                break;
            case 6:
                SelectedRToBuy.text = "철광석";
                break;
            case 7:
                SelectedRToBuy.text = "주조철";
                break;
            case 8:
                SelectedRToBuy.text = "소";
                break;
            case 9:
                SelectedRToBuy.text = "양";
                break;
            case 10:
                SelectedRToBuy.text = "치즈";
                break;
            case 11:
                SelectedRToBuy.text = "양털";
                break;
            case 12:
                SelectedRToBuy.text = "옷";
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
            new Vector3(-520, 0, 0), Time.deltaTime * 4f);//선형보간으로 숨겨뒀던 UI를 목표위치까지 서서히 이동해서 나타나게 함

            ExitBOn.GetComponent<RectTransform>().anchoredPosition3D =
                 Vector3.Lerp(ExitBOn.GetComponent<RectTransform>().anchoredPosition3D,
                 new Vector3(520, 0, 0), Time.deltaTime * 4f);
        }
        else
        {
            CellBtns.GetComponent<RectTransform>().anchoredPosition3D =
           Vector3.Lerp(CellBtns.GetComponent<RectTransform>().anchoredPosition3D,
           CellBtnPosition, Time.deltaTime * 4f);//저장해뒀던 위치로 다시 복구

            ExitBOn.GetComponent<RectTransform>().anchoredPosition3D =
                 Vector3.Lerp(ExitBOn.GetComponent<RectTransform>().anchoredPosition3D,
                 ExitBtnPosition, Time.deltaTime * 4f);
        }
    }

    public void ExitBtnOn()// 나가기 버튼으로 유아이 전부 꺼줌
    {
        ChatViewer.SendMessage("ListReset");
        //TChatViewer.SendMessage("ListReset");

        IsApear = false;

        isLerp = false;

        RandomOpen = 5;

        AnExchangeUI.SetActive(false);
    }

    public void CellBClicked()// 거래 버튼 누를 시 해당되는 유아이를 나오게 하기 위해 bool형으로 체크해줌 //나와 있으면 들어가고 들어가 있으면 나오게.
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

    public void TBClicked()// 유물거래 UI
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
        if (IsApear == true)//거래 관련 UI 선형보간으로 나오게
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
          new Vector3(0,1000,0), Time.deltaTime * 4f);

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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);// ray로 마우스 눌렀을 때 마우스 위치 받아옴

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray, distance);

            for (int i = 0; i < hits.Length; i++) // 레이로 클릭한 부분의 오브젝트 뒤져서
            {
                if (hits[i].collider.GetComponent<BuildingColider>() != null) // 건물에 할당한 콜라이더가 있을 시
                {
                    if (hits[i].collider.gameObject.tag.Equals("AnExchange") && hits[i].collider.GetComponent<BuildingColider>().isSettingComplete) // 태그로 AnExchange가 달려있으면
                    {
                        if (IsOpen == true) // 거래소가 열리면
                        {
                            AnExchangeUI.SetActive(true); // UI켜주고

                            isLerp = true; // 선형보간 (버튼)

                            MySlider.value = 0;
                            CellerSlider.value = 0;

                            RandomExchangeRate();
                        }
                        else
                        {
                            AnExchangeUI.SetActive(false); //아닐시 꺼주기

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
            if (AnExchangeBP.transform.GetChild(i).tag.Equals("AnExchange")) // 건물 생성 오브젝트 하위에 있는 오브젝트 중 태그가 AnExchange인 건물이 있으면 
            {
                AnExchangeB = AnExchangeBP.transform.GetChild(i).gameObject; // 게임오브젝트에 연결
            }
            else
            {
                return; // 없을 시 리턴
            }
        }
    }

    void RIsOpen()
    {
        if (GameManager.instance.dayNightRatio == 1f || GameManager.instance.dayNightRatio == 0f)
        {
            RandomOpen = Random.Range(0, 8);// 7분의 1 확률 가챠!! 열릴수도 있고~ 아닐수도 있고~
        }
    }

    void OpenState()
    {
        if (GameManager.instance.isDaytime == true)//낮이면
        {
            if (RandomOpen == 0)//랜덤으로 열리는 값이 들어오면
            {
                IsOpen = true; //문열기

                IsOpenImage.gameObject.SetActive(true);//알림 이미지 키고

                IsOpenImage.transform.position = new Vector3(AnExchangeB.transform.position.x + 1.5f, 
                    AnExchangeB.transform.position.y, AnExchangeB.transform.position.z + 1.5f);// 거래소 위치로 이동
            }
            else//0이 아니면 무조건 닫기
            {
                IsOpen = false;

                IsOpenImage.gameObject.SetActive(false);//알림 이미지 끔
            }
        }
        else//밤이면 무조건 닫기
        {
            IsOpen = false;

            IsOpenImage.gameObject.SetActive(false);
        }
    }

    public void GetQuantity(string value) // 팔 물건 갯수 받아서 환률 적용해서 살 물건 갯수 계산하고 표기.
    {
        ResourceCount = int.Parse (value);

        ResourceCount = (int)(ResourceCount * (100 / ExchangeRate) * Value);

        BuyRNum.text = ResourceCount.ToString();
    }

    public void GetQuantityBuy(string value)//살 물건 갯수 받아서 환률 적용해서 파ㅓㄹ 물건 갯수 계산하고 표기
    {
        ResourceCount_Buy = int.Parse(value);

        ResourceCount_Buy = (int)(ResourceCount_Buy * (ExchangeRate / 100) * Value);

        CellRNum.text = ResourceCount_Buy.ToString();
    }

    public void CellResources()
    {
        bool CellCheck = false;

        // 팔 물건 수가 가진 것보다 적으면 창고에서 소모, bool형 체크해줘서
        switch (CValue)
        {
            //밀, 식량, 나무, 육류, 가죽, 금화, 철광석, 주조철, 소, 양, 치즈, 양털, 옷
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

        //bool형 체크 되있으면 받아올 물건 받아오기
        if (CellCheck == true)
        {
            switch (BValue)
            {
                //밀, 식량, 나무, 육류, 가죽, 금화, 철광석, 주조철, 소, 양, 치즈, 양털, 옷
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
        else
        {
            SMassage.SendMessage("MessageQ", "자원이 부족합니다.");
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //RaycastResult : BaseRaycastModule에서의 히트 결과.
        List<RaycastResult> results = new List<RaycastResult>();
        //EventSystem.current은 최근에 발생한 이벤트 시스템을 반환한다.
        //첫번째 인자값 : 현재 포인터 데이터.
        //두번째 인자값 : List of 'hits' to populate.
        //RaycastAll : 모두 설정된 BaseRaycaster를 사용을 통한 해당 씬으로의 레이 캐스팅.
        // -> 겹쳐있는 오브젝트들이 있다면 겹쳐있는 수로 results의 카운트가 바뀜
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        //Debug.Log(results.Count);
        return results.Count > 0;
    }

    float RandomExchangeRate()//랜덤환률
    {
        ExchangeRate = 100;

        Random.Range(0, 201);

        return ExchangeRate;
    }

    int IsNegoOn(int Num)// 네고요청 한 후 네고 받으면 환률 증가 아니면 감소
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