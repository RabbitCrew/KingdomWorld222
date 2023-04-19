using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public float AddExchangeRate { get; set; }
    float DefaultLerpTime = 5f;
    public int MaxNegoText { get; set; }
    public int[] HasArtifact = new int[30];
    public int Children;
    public int RainRate;

    public Sprite[] ArtifactImage;

    [SerializeField] GameObject inven;

    private Vector3 InvenPos;
    private bool IsLerp = false;

    public static Inventory instance;

    private void Awake()
    {
        AddExchangeRate = 0;
        MaxNegoText = 6;

        InvenPos = inven.GetComponent<RectTransform>().anchoredPosition3D;

        RainRate = 361;
    }

    private void Update()
    {
        InventoryOn();

        ArtifactEffect();
    }

    public void InventoryButton()
    {
        if (IsLerp == false)
        {
            IsLerp = true;
        }
        else if(IsLerp == true)
        {
            IsLerp = false;
        }
    }

    void ArtifactEffect()// ���� ������ �κ��丮�� ǥ��
    {
        int count = 0;

        for (int i = 0; i < HasArtifact.Length; i++)
        {
            if (HasArtifact[i] >= 1)//�ش� ������ �Ѱ� �̻� ������ �ִٴ� �� Ȯ�εǸ�
            {
                inven.transform.GetChild(count).gameObject.SetActive(true);//�κ��丮 ������ �ִ� ������Ʈ ���ֱ�.

                inven.transform.GetChild(count).gameObject.
                    GetComponent<SpriteRenderer>().sprite = ArtifactImage[i];//�̹��� �´°� �־��ְ�

                inven.transform.GetChild(count).gameObject.SendMessage("ArtifactEffect", i);//��� �����ϵ��� ���.

                count++; //�ε��� �ܻ�
            }
        }
    }

    public void InventoryOn() 
    {
        if (IsLerp == true)
        {
            inven.GetComponent<RectTransform>().anchoredPosition3D =
           Vector3.Lerp(inven.GetComponent<RectTransform>().anchoredPosition3D, new Vector3(760, 100, 0), Time.deltaTime * DefaultLerpTime);
        }
        else if(IsLerp == false)
        {
            inven.GetComponent<RectTransform>().anchoredPosition3D =
         Vector3.Lerp(inven.GetComponent<RectTransform>().anchoredPosition3D, InvenPos, Time.deltaTime * DefaultLerpTime);
        }
    }
}
