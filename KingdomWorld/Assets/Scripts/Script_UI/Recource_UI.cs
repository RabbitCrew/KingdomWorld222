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
    Color defaultTextColor = new Color(1, 1, 1, 1);

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
                    RText[i].text = GameManager.instance.Wheat.ToString();

                    if (GameManager.instance.Wheat <= 0)//�ڿ��� 0���� ���� �� ���� ����
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
                    RText[i].text = GameManager.instance.Food.ToString();

                    if (GameManager.instance.Food <= 0)//�ķ��� 0���� ���� �� �ķ� ġȯ â ��� �ؽ�Ʈ ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 2:
                    RText[i].text = GameManager.instance.Wood.ToString();

                    if (GameManager.instance.Wood <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 3:
                    RText[i].text = GameManager.instance.Meat.ToString();

                    if (GameManager.instance.Meat <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 4:
                    RText[i].text = GameManager.instance.Leather.ToString();

                    if (GameManager.instance.Leather <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 5:
                    RText[i].text = GameManager.instance.Gold.ToString();

                    if (GameManager.instance.Gold <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 6:
                    RText[i].text = GameManager.instance.Itronstone.ToString();

                    if (GameManager.instance.Itronstone <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 7:
                    RText[i].text = GameManager.instance.CastIron.ToString();

                    if (GameManager.instance.CastIron <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 8:
                    RText[i].text = GameManager.instance.Cow.ToString();

                    if (GameManager.instance.Cow <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 9:
                    RText[i].text = GameManager.instance.Sheep.ToString();

                    if (GameManager.instance.Sheep <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 10:
                    RText[i].text = GameManager.instance.Cheese.ToString();

                    if (GameManager.instance.Cheese <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 11:
                    RText[i].text = GameManager.instance.Fleece.ToString();

                    if (GameManager.instance.Fleece <= 0)//�ڿ��� 0���� ���� �� ���� ����
                    {
                        RText[i].color = TextColor;
                    }
                    else
                    {
                        RText[i].color = defaultTextColor;
                    }
                    break;
                case 12:
                    RText[i].text = GameManager.instance.Cloth.ToString();

                    if (GameManager.instance.Cloth <= 0)//�ڿ��� 0���� ���� �� ���� ����
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
                if (GameManager.instance.Wheat >= 1)
                {
                    GameManager.instance.Food += 1;
                    GameManager.instance.Wheat--;
                }
                else
                {
                    MReciver.SendMessage("MessageQ", "���� �����մϴ�.");
                }
                break;
            case 1:
                if (GameManager.instance.Meat >= 1)
                {
                    GameManager.instance.Food += 10;
                    GameManager.instance.Meat--;
                }
                else
                {
                    MReciver.SendMessage("MessageQ", "��Ⱑ �����մϴ�.");
                }
                break;
            case 2:
                if (GameManager.instance.Cheese >= 1)
                {
                    GameManager.instance.Food += 5;
                    GameManager.instance.Cheese--;
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
                if (GameManager.instance.Cow >= 1)
                {
                    GameManager.instance.Meat++;
                    GameManager.instance.Cow--;
                }
                else
                {
                    MReciver.SendMessage("MessageQ", "�Ұ� �����մϴ�.");
                }
                break;
            case 1:
                if (GameManager.instance.Sheep >= 1)
                {
                    GameManager.instance.Meat++;
                    GameManager.instance.Sheep--;
                }
                else
                {
                    MReciver.SendMessage("MessageQ", "���� �����մϴ�.");
                }
                break;
        }
    }
}
