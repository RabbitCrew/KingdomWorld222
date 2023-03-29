using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniCtrl : MonoBehaviour
{
    public Animation ani;
    public AnimationClip Starts; //���۹�ư
    public AnimationClip SetB; //������ư
    public AnimationClip ExitB; //�����ư

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
