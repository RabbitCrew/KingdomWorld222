using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniCtrl : MonoBehaviour
{
    public Animation ani;
    public AnimationClip Starts; //시작버튼
    public AnimationClip SetB; //설정버튼
    public AnimationClip ExitB; //종료버튼

    public void AniStart()
    {
        ani.clip = Starts;
        ani.Play();
    }

    public void AniSetB()
    {
        ani.clip = SetB;
        ani.Play();
    }

    public void AniExitB()
    {
        ani.clip = ExitB;
        ani.Play();
    }
}
