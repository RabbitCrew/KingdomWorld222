using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAni : MonoBehaviour
{
    public Animation TypingAni;
    public Animation ButtonDrop;

    private void Start()
    {
        TypingAni.Play();

        Invoke("IsDrop", TypingAni["TitleTyping"].length + 0.8f);
    }

    void IsDrop()
    {
        ButtonDrop.Play();
    }
}
