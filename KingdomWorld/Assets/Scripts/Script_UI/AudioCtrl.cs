using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioCtrl : MonoBehaviour
{
    // ����� ����

    public static AudioCtrl instance;

    public AudioMixer masterVolume;
    public Slider VolumeM;
    public Slider VolumeB;
    public Slider VolumeE;

    public float MSound = 0f;
    public float BGMSound = 0f;
    public float ESound = 0f;

    private void Awake()
    {
        instance = this;
    }

    public void VolumeCtrl_Master()// ��ü����
    {
        float sound = MSound;
        sound = Mathf.Log10(VolumeM.value) * 20; // �����̴��� ���� ���� ����

        if(sound <= -40f)
        {
            masterVolume.SetFloat("Master", -80f); //-40���ʹ� �� �鸮�� ����
        }
        else
        {
            masterVolume.SetFloat("Master", sound);
        }

        MSound = sound;
    }

    public void VolumeCtrl_BGM()
    {
        float sound = BGMSound;
        sound = Mathf.Log10(VolumeB.value) * 20; // �����̴��� ���� ���� ����

        if (sound <= -40f)
        {
            masterVolume.SetFloat("BGM", -80f); //-40���ʹ� �� �鸮�� ����
        }
        else
        {
            masterVolume.SetFloat("BGM", sound);
        }

        BGMSound = sound;
    }

    public void VolumeCtrl_Effect()
    {
        float sound = ESound;
        sound = Mathf.Log10(VolumeE.value) * 20; // �����̴��� ���� ���� ����

        if (sound <= -40f)
        {
            masterVolume.SetFloat("EffectSound", -80f); //-40���ʹ� �� �鸮�� ����
        }
        else
        {
            masterVolume.SetFloat("EffectSound", sound);
        }

        ESound = sound;
    }
}
