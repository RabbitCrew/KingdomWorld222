using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TImeLight2D : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Light2D light2D;

    private float colorLerp = 0;
    private Color colorDawn;
    private Color colorMorning;
    private Color colorEvening;
    private Color colorNight;
    // Start is called before the first frame update
    void Start()
    {
        colorDawn = new Color(53 / 255f, 77 / 255f, 157 / 255f);
        colorMorning = new Color(255 / 255f, 249 / 255f, 219 / 255f);
        colorEvening = new Color(221 / 255f, 149 / 255f, 66 / 255f);
        colorNight = new Color(7 / 255f, 11 / 255f, 22 / 255f);
        light2D.color = colorDawn;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(gameManager.dayNightRatio);

        // »õº®³è¿¡¼­ ¾ÆÄ§(³· ½Ã°£´ë)
        if (gameManager.dayNightRatio >= 0 && gameManager.dayNightRatio < 0.2f)
        {
            colorLerp = (gameManager.dayNightRatio * 10f - 0f) / (2f - 0f);
            light2D.color = Color.Lerp(colorDawn, colorMorning, colorLerp);
        }
        // ¾ÆÄ§¿¡¼­ Àú³á(³· ½Ã°£´ë)
        else if (gameManager.dayNightRatio >= 0.2f && gameManager.dayNightRatio < 0.4f)
        {
            colorLerp = (gameManager.dayNightRatio * 10f - 2f) / (4f -2f);
            light2D.color = Color.Lerp(colorMorning, colorEvening, colorLerp);
        }
        // Àú³á¿¡¼­ ¹ã(³· ½Ã°£´ë)
        else if (gameManager.dayNightRatio >= 0.4f && gameManager.dayNightRatio < 2f / 3f)
        {
            colorLerp = (gameManager.dayNightRatio * 10f - 4f) / (20f / 3f - 4f);
            light2D.color = Color.Lerp(colorEvening, colorNight, colorLerp);
        }
        // ¹ã¿¡¼­ »õº®(Àú³á ½Ã°£´ë)
        else if (gameManager.dayNightRatio >= 2f / 3f && gameManager.dayNightRatio <= 1f)
        {
            colorLerp = (gameManager.dayNightRatio * 10f - (20f / 3f)) / (10f - (20f / 3f));
            light2D.color = Color.Lerp(colorNight, colorDawn, colorLerp);

        }

        //Debug.Log(colorLerp);

    }
}
