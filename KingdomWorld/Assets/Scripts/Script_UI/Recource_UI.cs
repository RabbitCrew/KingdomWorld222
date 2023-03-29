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

    public void ResourceSearch()//자원 수량 텍스트로 띄워줌
    {
        TextColor = new Color(1, 0, 0, 1);

        for (int i = 0; i < RText.Length; i++)
        {
            switch (i)
            {
                case 0:
                    RText[i].text = Resource.Resource_Instance.Wheat.ToString();

                    if (Resource.Resource_Instance.Wheat <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (Resource.Resource_Instance.Food <= 0)//식량이 0개가 됬을 때 식량 치환 창 띄움 텍스트 색상 변경
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

                    if (Resource.Resource_Instance.Wood <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (Resource.Resource_Instance.Meat <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (Resource.Resource_Instance.Leather <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (Resource.Resource_Instance.Gold <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (Resource.Resource_Instance.Itronstone <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (Resource.Resource_Instance.CastIron <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (Resource.Resource_Instance.Cow <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (Resource.Resource_Instance.Sheep <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (Resource.Resource_Instance.Cheese <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (Resource.Resource_Instance.Fleece <= 0)//자원이 0개가 됬을 때 색상 변경
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

                    if (Resource.Resource_Instance.Cloth <= 0)//자원이 0개가 됬을 때 색상 변경
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
                if (Resource.Resource_Instance.Wheat >= 1)
                {
                    Resource.Resource_Instance.Food += 1;
                    Resource.Resource_Instance.Wheat--;
                }
                else
                {
                    MReciver.SendMessage("MessageQ", "밀이 부족합니다.");
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
                    MReciver.SendMessage("MessageQ", "고기가 부족합니다.");
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
                if (Resource.Resource_Instance.Cow >= 1)
                {
                    Resource.Resource_Instance.Meat++;
                    Resource.Resource_Instance.Cow--;
                }
                else
                {
                    MReciver.SendMessage("MessageQ", "소가 부족합니다.");
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
                    MReciver.SendMessage("MessageQ", "양이 부족합니다.");
                }
                break;
        }
    }
}
