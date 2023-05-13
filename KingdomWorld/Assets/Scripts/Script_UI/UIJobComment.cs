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
                comment.text = "������ ���� �������� ���θ����� �������ϴ�." + "\n" + "�������� ���θ��� �־�� ���������մϴ�.";
                break;
            case 2:
                comment.text = "���� ������ ������ �߰��ϸ� �ǹ��� �����ϴ�." + "\n" + "����Ұ� �־�� ���������մϴ�.";
                break;
            case 3:
                comment.text = "�߻������� ����Ͽ� ������ �����մϴ�." + "\n" + "��ɲ��� ���θ��� �־�� ���������մϴ�. ���� �̱����Դϴ�.";
                break;
            case 4:
                comment.text = "���� ��Ȯ�Ͽ� �������� �������ϴ�." + "\n" + "���� �־�� ��δ� ��Ȯ�� �մϴ�.";
                break;
            case 5:
                comment.text = "�ҿ� ���� Ű�� ���, ����, ������ ����ϴ�." + "\n" + "������ �־�� ���������մϴ�.";
                break;
            case 6:
                comment.text = "�ڿ��� ������ ����ǹ����� �ڿ��� ���� â�� �ֽ��ϴ�." + "\n" + "â�� �־�� ���������մϴ�.";
                break;
            case 7:
                comment.text = "���꿡 ���� ö�� ĸ�ϴ�. ĵ �ڿ��� â�� �ֽ��ϴ�.";
                break;
            case 8:
                comment.text = "���� ĳ�� ������ ���θ����� �������ϴ�." + "\n" + "������ ���� �־�� ���������մϴ�.";
                break;
            case 9:
                comment.text = "���� ���� ����ϴ�." + "\n" + "�� ������ �־�� ���������մϴ�.";
                break;
            case 10:
                comment.text = "������ ġ� ����ϴ�." + "\n" + "ġ�� ������ �־�� ���� �����մϴ�.";
                break;
            case 11:
                comment.text = "���з� ���� ����ϴ�." + "\n" + "�ʰ� ������ �־�� ���� �����մϴ�.";
                break;
            case 12:
                comment.text = "ö������ ����ö�� ����ϴ�." + "\n" + "���尣�� �־�� ���� �����մϴ�..";
                break;
        }
    }

}
