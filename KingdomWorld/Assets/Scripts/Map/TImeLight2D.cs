using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

//시간대에 따라 Light2D의 색을 바꾸어 낮밤효과를 줌
public class TImeLight2D : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Light2D light2D;

    public float colorLerp { get; private set; }    // 시간 파트 별로 진행도
    private Color colorDawn;    // 새벽 시간
    private Color colorMorning; // 아침 시간
    private Color colorEvening; // 저녁 시간
    private Color colorNight;   // 밤 시간

    void Start()
    {
        colorLerp = 0;
        colorDawn = new Color(53 / 255f, 77 / 255f, 157 / 255f);    //새벽 조명 색깔
        colorMorning = new Color(255 / 255f, 249 / 255f, 219 / 255f);   // 아침 조명 색깔
        colorEvening = new Color(221 / 255f, 149 / 255f, 66 / 255f);    // 저녁 조명 색깔
        colorNight = new Color(7 / 255f, 11 / 255f, 22 / 255f); // 밤 조명 색깔
        light2D.color = colorDawn;  // 시간은 0부터 시작하므로 시작 조명은 새벽
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(gameManager.dayNightRatio);

        // 새벽녘에서 아침(낮 시간대)
        if (gameManager.dayNightRatio >= 0 && gameManager.dayNightRatio < 0.2f)
        {
            // 새벽시간대가 얼마나 지났는지 퍼센트를 의미(0f를 빼준건 계산식을 보기 편하게 하기 위함)
            // 0f를 빼주어 dayNightRatio의 범위를 0에서 0.2 미만으로 바꿈.
            // 새벽에서 아침 시간대 범위를 구하기 위해 0.2에 0을 빼줌. 보기좋게 각각의 값에 10 곱하고 dayNightRatio를 범위값으로 나눔
            colorLerp = (gameManager.dayNightRatio * 10f - 0f) / (2f - 0f);
            // 퍼센트에 따라 조명의 색을 서서히 바꿔줌.
            light2D.color = Color.Lerp(colorDawn, colorMorning, colorLerp);
        }
        // 아침에서 저녁(낮 시간대)
        else if (gameManager.dayNightRatio >= 0.2f && gameManager.dayNightRatio < 0.4f)
        {
            // 아침시간대가 얼마나 지났는지 퍼센트를 의미
            // 아침에서 저녁 시간대의 범위는 0.2 즉, 0.4 - 0.2. 보기 좋게 각각 10씩 곱해서 4 - 2로 계산
            colorLerp = (gameManager.dayNightRatio * 10f - 2f) / (4f -2f);
            light2D.color = Color.Lerp(colorMorning, colorEvening, colorLerp);
        }
        // 저녁에서 밤(낮 시간대)
        else if (gameManager.dayNightRatio >= 0.4f && gameManager.dayNightRatio < 2f / 3f)
        {
            // 위와 마찬가지로 계산. 저녁시간대가 얼마나 지났는지 퍼센트를 의미
            colorLerp = (gameManager.dayNightRatio * 10f - 4f) / (20f / 3f - 4f);
            light2D.color = Color.Lerp(colorEvening, colorNight, colorLerp);
        }
        // 밤에서 새벽(저녁 시간대)
        else if (gameManager.dayNightRatio >= 2f / 3f && gameManager.dayNightRatio <= 1f)
        {
            // 밤시간대가 얼마나 지났는지 퍼센트를 의미
            colorLerp = (gameManager.dayNightRatio * 10f - (20f / 3f)) / (10f - (20f / 3f));
            light2D.color = Color.Lerp(colorNight, colorDawn, colorLerp);

        }

    }
}
