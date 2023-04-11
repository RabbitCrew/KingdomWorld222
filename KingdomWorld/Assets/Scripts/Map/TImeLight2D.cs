using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

//�ð��뿡 ���� Light2D�� ���� �ٲپ� ����ȿ���� ��
public class TImeLight2D : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Light2D light2D;

    public float colorLerp { get; private set; }    // �ð� ��Ʈ ���� ���൵
    private Color colorDawn;    // ���� �ð�
    private Color colorMorning; // ��ħ �ð�
    private Color colorEvening; // ���� �ð�
    private Color colorNight;   // �� �ð�

    void Start()
    {
        colorLerp = 0;
        colorDawn = new Color(53 / 255f, 77 / 255f, 157 / 255f);    //���� ���� ����
        colorMorning = new Color(255 / 255f, 249 / 255f, 219 / 255f);   // ��ħ ���� ����
        colorEvening = new Color(221 / 255f, 149 / 255f, 66 / 255f);    // ���� ���� ����
        colorNight = new Color(7 / 255f, 11 / 255f, 22 / 255f); // �� ���� ����
        light2D.color = colorDawn;  // �ð��� 0���� �����ϹǷ� ���� ������ ����
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(gameManager.dayNightRatio);

        // �����迡�� ��ħ(�� �ð���)
        if (gameManager.dayNightRatio >= 0 && gameManager.dayNightRatio < 0.2f)
        {
            // �����ð��밡 �󸶳� �������� �ۼ�Ʈ�� �ǹ�(0f�� ���ذ� ������ ���� ���ϰ� �ϱ� ����)
            // 0f�� ���־� dayNightRatio�� ������ 0���� 0.2 �̸����� �ٲ�.
            // �������� ��ħ �ð��� ������ ���ϱ� ���� 0.2�� 0�� ����. �������� ������ ���� 10 ���ϰ� dayNightRatio�� ���������� ����
            colorLerp = (gameManager.dayNightRatio * 10f - 0f) / (2f - 0f);
            // �ۼ�Ʈ�� ���� ������ ���� ������ �ٲ���.
            light2D.color = Color.Lerp(colorDawn, colorMorning, colorLerp);
        }
        // ��ħ���� ����(�� �ð���)
        else if (gameManager.dayNightRatio >= 0.2f && gameManager.dayNightRatio < 0.4f)
        {
            // ��ħ�ð��밡 �󸶳� �������� �ۼ�Ʈ�� �ǹ�
            // ��ħ���� ���� �ð����� ������ 0.2 ��, 0.4 - 0.2. ���� ���� ���� 10�� ���ؼ� 4 - 2�� ���
            colorLerp = (gameManager.dayNightRatio * 10f - 2f) / (4f -2f);
            light2D.color = Color.Lerp(colorMorning, colorEvening, colorLerp);
        }
        // ���ῡ�� ��(�� �ð���)
        else if (gameManager.dayNightRatio >= 0.4f && gameManager.dayNightRatio < 2f / 3f)
        {
            // ���� ���������� ���. ����ð��밡 �󸶳� �������� �ۼ�Ʈ�� �ǹ�
            colorLerp = (gameManager.dayNightRatio * 10f - 4f) / (20f / 3f - 4f);
            light2D.color = Color.Lerp(colorEvening, colorNight, colorLerp);
        }
        // �㿡�� ����(���� �ð���)
        else if (gameManager.dayNightRatio >= 2f / 3f && gameManager.dayNightRatio <= 1f)
        {
            // ��ð��밡 �󸶳� �������� �ۼ�Ʈ�� �ǹ�
            colorLerp = (gameManager.dayNightRatio * 10f - (20f / 3f)) / (10f - (20f / 3f));
            light2D.color = Color.Lerp(colorNight, colorDawn, colorLerp);

        }

    }
}
