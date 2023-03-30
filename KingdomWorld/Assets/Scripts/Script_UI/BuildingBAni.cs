using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBAni : MonoBehaviour
{
    public Image[] BImage = new Image[13];

    bool AniStart = false;
    bool RBGo = false;
    bool LBGo = false;

    int AniCount = 0;

    public int Speed = 20;

    public GameObject RBTarget;
    public GameObject LButton;
    public GameObject RButton;

    Vector3 RBTargetSP;

    private void Update()
    {
        if (AniStart == true)//anistart üũ�Ǿ����� �� �ִϸ��̼� ����
        {
            BAniStart();
        }

        RBAniStart();
        LBAniStart();
    }

    public void BAniC()//ó�� ȣ�� �� ��� �̹��� fillamount���� 
    {// anistart üũ����
        for (int i = 0; i < BImage.Length; i++)
        {
            BImage[i].fillAmount = 0;
        }

        AniStart = true;

        RBTargetSP = RBTarget.transform.position;
    }

    void BAniStart()//ȣ�� �� �迭�� �ִ� �̹��� ������� speed���� ���� fillamount ä�������� ��.
    {
        switch (AniCount)
        {
            case 0:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 1:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 2:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 3:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 4:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 5:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 6:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 7:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 8:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 9:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 10:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 11:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount++;
                }
                break;
            case 12:
                BImage[AniCount].fillAmount += Time.deltaTime * Speed;
                if (BImage[AniCount].fillAmount == 1)
                {
                    AniCount = 0;
                    AniStart = false;
                }
                break;
            default:
                break;
        }
    }

    public void BAniEnd() //�ִϸ��̼� ������ �ϴ� �Լ�
    {
        AniCount = 0;
        AniStart = false;

        RBTarget.transform.position = RBTargetSP;
        RBGo = false;
        LBGo = false;
    }

    public void RightBAni()
    {
        RBGo = true;
    }

    public void LeftBAni()
    {
        LBGo = true;
    }

    void RBAniStart()
    {
        Vector3 TargetP;

        TargetP = new Vector3(RBTarget.transform.localPosition.x - 72f, RBTarget.transform.localPosition.y, RBTarget.transform.localPosition.z);

        if (RBGo == true)
        {
            RBTarget.transform.localPosition = TargetP;

            LButton.SetActive(true);

            RBGo = false;
        }

        if (RBTarget.transform.localPosition.x <= -180f)
        {
            RBTarget.transform.localPosition = new Vector3(-185, 132, 0);

            RButton.SetActive(false);
        }
    }

    void LBAniStart()
    {
        Vector3 TargetP;

        TargetP = new Vector3(RBTarget.transform.localPosition.x + 72f, RBTarget.transform.localPosition.y, RBTarget.transform.localPosition.z);

        if (LBGo == true)
        {
            RBTarget.transform.localPosition = TargetP;

            LBGo = false;
        }

        if (RBTarget.transform.localPosition.x >= 0)
        {
            RBTarget.transform.localPosition = new Vector3(0, 132, 0);

            LButton.SetActive(false);
            RButton.SetActive(true);
        }
    }
}
