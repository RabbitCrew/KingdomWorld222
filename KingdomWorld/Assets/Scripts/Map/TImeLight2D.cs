using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.ParticleSystemJobs;

//시간대에 따라 Light2D의 색을 바꾸어 낮밤효과를 줌
public class TImeLight2D : MonoBehaviour
{
    [SerializeField] private RectTransform clockImageRect;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Light2D light2D;
    [SerializeField]private ParticleSystem rainParticle;

    public float colorLerp { get; private set; }    // 시간 파트 별로 진행도
    private ParticleSystem.EmissionModule emissionModule;
    private Color colorDawn;    // 새벽 시간
    private Color colorMorning; // 아침 시간
    private Color colorEvening; // 저녁 시간
    private Color colorNight;   // 밤 시간
    private float rotate;
    void Start()
    {
        colorLerp = 0;
        colorDawn = new Color(53 / 255f, 77 / 255f, 157 / 255f);    //새벽 조명 색깔
        colorMorning = new Color(255 / 255f, 249 / 255f, 219 / 255f);   // 아침 조명 색깔
        colorEvening = new Color(221 / 255f, 149 / 255f, 66 / 255f);    // 저녁 조명 색깔
        colorNight = new Color(7 / 255f, 11 / 255f, 22 / 255f); // 밤 조명 색깔
        light2D.color = colorDawn;  // 시간은 0부터 시작하므로 시작 조명은 새벽
        rotate = 45f;
        //isRain = false;
        emissionModule = rainParticle.emission;
        StartCoroutine(RainTerm());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(gameManager.dayNightRatio);

        // 새벽녘에서 아침(낮 시간대)
        if (gameManager.dayNightRatio >= 0 && gameManager.dayNightRatio < 0.2f)
        {
            // 새벽시간대가 얼마나 지났는지 퍼센트를 의미
            colorLerp = Mathf.InverseLerp(0f, 0.2f, gameManager.dayNightRatio);
            // 퍼센트에 따라 조명의 색을 서서히 바꿔줌.
            light2D.color = Color.Lerp(colorDawn, colorMorning, colorLerp);
        }
        // 아침에서 저녁(낮 시간대)
        else if (gameManager.dayNightRatio >= 0.2f && gameManager.dayNightRatio < 0.4f)
        {
            // 아침시간대가 얼마나 지났는지 퍼센트를 의미
            colorLerp = Mathf.InverseLerp(0.2f, 0.4f, gameManager.dayNightRatio);
            light2D.color = Color.Lerp(colorMorning, colorEvening, colorLerp);
        }
        // 저녁에서 밤(낮 시간대)
        else if (gameManager.dayNightRatio >= 0.4f && gameManager.dayNightRatio < 2f / 3f)
        {
            // 위와 마찬가지로 계산. 저녁시간대가 얼마나 지났는지 퍼센트를 의미
            colorLerp = Mathf.InverseLerp(0.4f, 2f / 3f, gameManager.dayNightRatio);
            light2D.color = Color.Lerp(colorEvening, colorNight, colorLerp);
        }
        // 밤에서 새벽(저녁 시간대)
        else if (gameManager.dayNightRatio >= 2f / 3f && gameManager.dayNightRatio <= 1f)
        {
            // 밤시간대가 얼마나 지났는지 퍼센트를 의미
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