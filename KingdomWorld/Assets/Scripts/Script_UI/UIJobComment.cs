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
                comment.text = "����̵� �� �غ� �Ǿ��ִ� ������Դϴ�.";
                break;
            case 1:
                comment.text = "������ ���� �������� ���θ����� �������ϴ�.";
                break;
            case 2:
                comment.text = "���� ������ ������ �߰��ϸ� �ǹ��� �����ϴ�.";
                break;
            case 3:
                comment.text = "�߻������� ����Ͽ� ������ �����մϴ�.";
                break;
            case 4:
                comment.text = "���� ��Ȯ�Ͽ� �������� �������ϴ�.";
                break;
            case 5:
                comment.text = "�ҿ� ���� Ű�� ���, ����, ������ ����ϴ�.";
                break;
            case 6:
                comment.text = "�ڿ��� ������ ����ǹ����� �ڿ��� ���� â�� �ֽ��ϴ�.";
                break;
            case 7:
                comment.text = "���꿡 ���� ö�� ĸ�ϴ�.";
                break;
            case 8:
                comment.text = "���� ĳ�� ������ ���θ����� �������ϴ�.";
                break;
            case 9:
                comment.text = "���� ���� ����ϴ�.";
                break;
            case 10:
                comment.text = "������ ġ� ����ϴ�.";
                break;
            case 11:
                comment.text = "���з� ���� ����ϴ�.";
                break;
            case 12:
                comment.text = "ö������ ����ö�� ����ϴ�.";
                break;
        }
    }

}
