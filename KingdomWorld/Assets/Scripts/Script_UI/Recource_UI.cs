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

    public void ResourceSearch()//자원 수량 텍스트로 띄워줌
    {
        TextColor = new Color(1, 0, 0, 1);

        for (int i = 0; i < RText.Length; i++)
        {
            switch (i)
            {
                case 0:
                    RText[i].text = GameManager.instance.Wheat.ToString();

                    if (GameManager.instance.Wheat <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (GameManager.instance.Food <= 0)//식량이 0개가 됬을 때 식량 치환 창 띄움 텍스트 색상 변경
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

                    if (GameManager.instance.Wood <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (GameManager.instance.Meat <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (GameManager.instance.Leather <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (GameManager.instance.Gold <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (GameManager.instance.Itronstone <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (GameManager.instance.CastIron <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (GameManager.instance.Cow <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (GameManager.instance.Sheep <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (GameManager.instance.Cheese <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (GameManager.instance.Fleece <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (GameManager.instance.Cloth <= 0)//자원이 0개가 됬을 때 색상 변경
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

    public void FoodChange(int value)//매개변수로 값을 받아서 값에 따라 밀, 고기, 치즈를 식량으로 바꿔줌. 수가 부족할 시 경고창 띄움
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
                    MReciver.SendMessage("MessageQ", "밀이 부족합니다.");
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
                    MReciver.SendMessage("MessageQ", "고기가 부족합니다.");
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
                    MReciver.SendMessage("MessageQ", "치즈가 부족합니다.");
                }
                break;
        }
    }

    public void MeatChange(int value)//매개변수로 값을 받아서 값에 따라 소, 양을 고기로 바꿔줌. 수가 부족할 시 경고창 띄움
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
                    MReciver.SendMessage("MessageQ", "소가 부족합니다.");
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
                    MReciver.SendMessage("MessageQ", "양이 부족합니다.");
                }
                break;
        }
    }
}
