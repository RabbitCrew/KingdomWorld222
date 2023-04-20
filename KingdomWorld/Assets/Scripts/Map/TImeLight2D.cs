using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.ParticleSystemJobs;

//�ð��뿡 ���� Light2D�� ���� �ٲپ� ����ȿ���� ��
public class TImeLight2D : MonoBehaviour
{
    [SerializeField] private RectTransform clockImageRect;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Light2D light2D;
    [SerializeField]private ParticleSystem rainParticle;

    public float colorLerp { get; private set; }    // �ð� ��Ʈ ���� ���൵
    private ParticleSystem.EmissionModule emissionModule;
    private Color colorDawn;    // ���� �ð�
    private Color colorMorning; // ��ħ �ð�
    private Color colorEvening; // ���� �ð�
    private Color colorNight;   // �� �ð�
    private float rotate;
    void Start()
    {
        colorLerp = 0;
        colorDawn = new Color(53 / 255f, 77 / 255f, 157 / 255f);    //���� ���� ����
        colorMorning = new Color(255 / 255f, 249 / 255f, 219 / 255f);   // ��ħ ���� ����
        colorEvening = new Color(221 / 255f, 149 / 255f, 66 / 255f);    // ���� ���� ����
        colorNight = new Color(7 / 255f, 11 / 255f, 22 / 255f); // �� ���� ����
        light2D.color = colorDawn;  // �ð��� 0���� �����ϹǷ� ���� ������ ����
        rotate = 45f;
        //isRain = false;
        emissionModule = rainParticle.emission;
        StartCoroutine(RainTerm());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(gameManager.dayNightRatio);

        // �����迡�� ��ħ(�� �ð���)
        if (gameManager.dayNightRatio >= 0 && gameManager.dayNightRatio < 0.2f)
        {
            // �����ð��밡 �󸶳� �������� �ۼ�Ʈ�� �ǹ�
            colorLerp = Mathf.InverseLerp(0f, 0.2f, gameManager.dayNightRatio);
            // �ۼ�Ʈ�� ���� ������ ���� ������ �ٲ���.
            light2D.color = Color.Lerp(colorDawn, colorMorning, colorLerp);
        }
        // ��ħ���� ����(�� �ð���)
        else if (gameManager.dayNightRatio >= 0.2f && gameManager.dayNightRatio < 0.4f)
        {
            // ��ħ�ð��밡 �󸶳� �������� �ۼ�Ʈ�� �ǹ�
            colorLerp = Mathf.InverseLerp(0.2f, 0.4f, gameManager.dayNightRatio);
            light2D.color = Color.Lerp(colorMorning, colorEvening, colorLerp);
        }
        // ���ῡ�� ��(�� �ð���)
        else if (gameManager.dayNightRatio >= 0.4f && gameManager.dayNightRatio < 2f / 3f)
        {
            // ���� ���������� ���. ����ð��밡 �󸶳� �������� �ۼ�Ʈ�� �ǹ�
            colorLerp = Mathf.InverseLerp(0.4f, 2f / 3f, gameManager.dayNightRatio);
            light2D.color = Color.Lerp(colorEvening, colorNight, colorLerp);
        }
        // �㿡�� ����(���� �ð���)
        else if (gameManager.dayNightRatio >= 2f / 3f && gameManager.dayNightRatio <= 1f)
        {
            // ��ð��밡 �󸶳� �������� �ۼ�Ʈ�� �ǹ�
            colorLerp = Mathf.InverseLerp(2f / 3f, 1f, gameManager.dayNightRatio);
            light2D.color = Color.Lerp(colorNight, colorDawn, colorLerp);
        }

        //light2D.color = light2D.color - rainColorMinus;

        if (Time.timeScale != 0)
        {
            if (GameManager.instance.dayNightRatio >= 0f && GameManager.instance.dayNightRatio < 0.2f)
            {
                rotate = Mathf.Lerp(45, -45, colorLerp);
            }
            else if (GameManager.instance.dayNightRatio >= 0.2f && GameManager.instance.dayNightRatio < 0.4f)
            {
                rotate = Mathf.Lerp(-45, -135, colorLerp);
            }
            else if (GameManager.instance.dayNightRatio >= 0.4f && GameManager.instance.dayNightRatio < 2f / 3f)
            {
                rotate = Mathf.Lerp(-135, -225, colorLerp);
            }
            else if (GameManager.instance.dayNightRatio >= 2f / 3f && GameManager.instance.dayNightRatio <= 1f)
            {
                rotate = Mathf.Lerp(-225, -315, colorLerp);
            }
            clockImageRect.localEulerAngles = new Vector3(0, 0, rotate);
        }
    }

    IEnumerator RainTerm()
    {
        var one = new WaitForSeconds(1f);
        int ran = 0;
        int length = 0;
        Color oneColor = new Color(1 / 255f, 1 / 255f, 1 / 255f);
        while (true)
        {
            ran = Random.Range(1, Inventory.instance.RainRate);
            length = Random.Range(1, Inventory.instance.RainRate);
            Debug.Log(ran + " " + length);
            for (int i = 0; i < ran; i++)
            {
                yield return one;
            }
            GameManager.instance.isRain = true;

            rainParticle.Play();

            for (int i = 0; i < length; i++)
            {
                if (i < length / 2)
                {
                    if (i < 50)
                    {
                        light2D.intensity -= 0.01f;
                    }

                    if (emissionModule.rateOverTime.constant < 300f )
                    {
                        emissionModule.rateOverTime = emissionModule.rateOverTime.constant + 10f;
                    }
                }
                else
                {
                    if (light2D.intensity < 1)
                    {
                        light2D.intensity += 0.01f;
                    }


                    if (emissionModule.rateOverTime.constant > 10f)
                    {
                        emissionModule.rateOverTime = emissionModule.rateOverTime.constant - 10f;
                    }
                }
                yield return one;
            }
            light2D.intensity = 1f;
            GameManager.instance.isRain = false;
            rainParticle.Stop();
        }
    }
}