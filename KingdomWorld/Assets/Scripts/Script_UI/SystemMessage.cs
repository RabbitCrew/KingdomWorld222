using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemMessage : MonoBehaviour
{
    public GameObject Mtarget;
    public TextMeshProUGUI TMP;

    void MessageQ(string Message)// ������Ʈ���� ���۵� �޽����� �޾� �ؽ�Ʈ�� �ű�
    {
        Mtarget.SetActive(true);

        TMP.text = Message;
    }
}
