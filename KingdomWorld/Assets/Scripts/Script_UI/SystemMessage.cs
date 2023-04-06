using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemMessage : MonoBehaviour
{
    public GameObject Mtarget;
    public TextMeshProUGUI TMP;

    void MessageQ(string Message)// 오브젝트에서 전송된 메시지를 받아 텍스트로 옮김
    {
        Mtarget.SetActive(true);

        TMP.text = Message;
    }
}
