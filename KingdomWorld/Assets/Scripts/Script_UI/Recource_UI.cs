using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Recource_UI : MonoBehaviour
{
    public GameObject ChangeWin;
    public GameObject MReciver;

    public TextMeshProUGUI[] RText = new TextMeshProUGUI[13];

    Color TextColor;
    Color defaultTextColor;

    //bool IsEmpty = false;

    private void Update()
    {
        ResourceSearch();
    }

    public void ChangeWinOn()
    {
        ChangeWin.SetActive(true);
    }

    public void ResourceSearch()//�ڿ� ���� �ؽ�Ʈ�� �����
    {
        TextColor = new Color(1, 0, 0, 1);

        for (int i = 0; i < RText.Length; i++)
        {
            switch (i)
            {
                case 0:
                    RText[i].text = Resource.Resource_Instance.Wheat.ToString();

                    if (Resource.Resource_Instance.Wheat <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                        ChangeWinOn();
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 1:
                    RText[i].text = Resource.Resource_Instance.Food.ToString();

                    if (Resource.Resource_Instance.Food <= 0)//�ķ��� 0���� ���� �� �ķ� ġȯ â ��� �ؽ�Ʈ ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 2:
                    RText[i].text = Resource.Resource_Instance.Wood.ToString();

                    if (Resource.Resource_Instance.Wood <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 3:
                    RText[i].text = Resource.Resource_Instance.Meat.ToString();

                    if (Resource.Resource_Instance.Meat <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 4:
                    RText[i].text = Resource.Resource_Instance.Leather.ToString();

                    if (Resource.Resource_Instance.Leather <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 5:
                    RText[i].text = Resource.Resource_Instance.Gold.ToString();

                    if (Resource.Resource_Instance.Gold <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 6:
                    RText[i].text = Resource.Resource_Instance.Itronstone.ToString();

                    if (Resource.Resource_Instance.Itronstone <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 7:
                    RText[i].text = Resource.Resource_Instance.CastIron.ToString();

                    if (Resource.Resource_Instance.CastIron <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 8:
                    RText[i].text = Resource.Resource_Instance.Cow.ToString();

                    if (Resource.Resource_Instance.Cow <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 9:
                    RText[i].text = Resource.Resource_Instance.Sheep.ToString();

                    if (Resource.Resource_Instance.Sheep <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 10:
                    RText[i].text = Resource.Resource_Instance.Cheese.ToString();

                    if (Resource.Resource_Instance.Cheese <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 11:
                    RText[i].text = Resource.Resource_Instance.Fleece.ToString();

                    if (Resource.Resource_Instance.Fleece <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 12:
                    RText[i].text = Resource.Resource_Instance.Cloth.ToString();

                    if (Resource.Resource_Instance.Cloth <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void FoodChange(int value)//�Ű������� ���� �޾Ƽ� ���� ���� ��, ���, ġ� �ķ����� �ٲ���. ���� ������ �� ���â ���
    {
        //MReciver.SendMessage("MessageQ", "You should change some food");
        switch (value)
        {
            case 0:
                if (Resource.Resource_Instance.Wheat >= 1)
                {
                    Resource.Resource_Instance.Food += 1;
                    Resource.Resource_Instance.Wheat--;
                }
                else
                {
                    MReciver.SendMessage("MessageQ", "���� �����մϴ�.");
                }
                break;
            case 1:
                if (Resource.Resource_Instance.Meat >= 1)
                {
                    Resource.Resource_Instance.Food += 10;
                    Resource.Resource_Instance.Meat--;
                }
                else
                {
                    MReciver.SendMessage("MessageQ", "��Ⱑ �����մϴ�.");
                }
                break;
            case 2:
                if (Resource.Resource_Instance.Cheese >= 1)
                {
                    Resource.Resource_Instance.Food += 5;
                    Resource.Resource_Instance.Cheese--;
                }
                else
                {
                    MReciver.SendMessage("MessageQ", "ġ� �����մϴ�.");
                }
                break;
        }
    }

    public void MeatChange(int value)//�Ű������� ���� �޾Ƽ� ���� ���� ��, ���� ���� �ٲ���. ���� ������ �� ���â ���
    {
        switch (value)
        {
            case 0:
                if (Resource.Resource_Instance.Cow >= 1)
                {
                    Resource.Resource_Instance.Meat++;
                    Resource.Resource_Instance.Cow--;
                }
                else
                {
                    MReciver.SendMessage("MessageQ", "�Ұ� �����մϴ�.");
                }
                break;
            case 1:
                if (Resource.Resource_Instance.Sheep >= 1)
                {
                    Resource.Resource_Instance.Meat++;
                    Resource.Resource_Instance.Sheep--;
                }
                else
                {
                    MReciver.SendMessage("MessageQ", "���� �����մϴ�.");
                }
                break;
        }
    }
}
