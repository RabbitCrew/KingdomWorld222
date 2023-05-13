using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIJobComment : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI comment;

    public void AppearComment(int jobNum)
    {
        if (jobNum == -100) { comment.text = ""; return; }

        switch(jobNum)
        {
            case 0:
                comment.text = "어떤일이든 할 준비가 되어있는 사람들입니다.";
                break;
            case 1:
                comment.text = "나무를 베어 나무꾼의 오두막으로 가져갑니다." + "\n" + "나무꾼의 오두막이 있어야 배정가능합니다.";
                break;
            case 2:
                comment.text = "건축 예정인 공간을 발견하면 건물을 짓습니다." + "\n" + "목공소가 있어야 배정가능합니다.";
                break;
            case 3:
                comment.text = "야생동물을 사냥하여 가죽을 수집합니다." + "\n" + "사냥꾼의 오두막이 있어야 배정가능합니다. 현재 미구현입니다.";
                break;
            case 4:
                comment.text = "밀을 수확하여 농장으로 가져갑니다." + "\n" + "밭이 있어야 농부는 수확을 합니다.";
                break;
            case 5:
                comment.text = "소와 양을 키워 고기, 우유, 양털을 얻습니다." + "\n" + "농장이 있어야 배정가능합니다.";
                break;
            case 6:
                comment.text = "자원이 가득찬 생산건물에서 자원을 빼와 창고에 넣습니다." + "\n" + "창고가 있어야 배정가능합니다.";
                break;
            case 7:
                comment.text = "광산에 가서 철을 캡니다. 캔 자원은 창고에 넣습니다.";
                break;
            case 8:
                comment.text = "돌을 캐서 광부의 오두막으로 가져갑니다." + "\n" + "광부의 집이 있어야 배정가능합니다.";
                break;
            case 9:
                comment.text = "고기로 햄을 만듭니다." + "\n" + "햄 가공소 있어야 배정가능합니다.";
                break;
            case 10:
                comment.text = "우유로 치즈를 만듭니다." + "\n" + "치즈 공방이 있어야 배정 가능합니다.";
                break;
            case 11:
                comment.text = "양털로 옷을 만듭니다." + "\n" + "옷감 공방이 있어야 배정 가능합니다.";
                break;
            case 12:
                comment.text = "철광석을 주조철로 만듭니다." + "\n" + "대장간이 있어야 배정 가능합니다..";
                break;
        }
    }

}
