using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinterIsComing : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private TextMeshProUGUI textPro;
    [SerializeField] private GameObject WinterPanel;
    [SerializeField] private PerlinNoiseMapMaker perlin;

    private ParticleSystem.EmissionModule emissionModule;
    private Material textProMaterial;
    private Color textProColor;
    private float intensity;
    public int winterCount { get; set; }
    public bool isOneDay { get; set; }
    private bool isWinter;
    public bool isChangedSprite { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        textProColor = new Color(191 / 255f, 53 / 255f, 0 / 255f, 0 / 255f);
        emissionModule = particle.emission;
        initField();
    }

    public void initField()
	{
        isOneDay = false;
        isWinter = false;
        winterCount = 3;
        isChangedSprite = false;
        intensity = 0f;
        emissionModule.rateOverTime = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isDaytime && !isOneDay && winterCount > 0)
        {
            winterCount -= 1;
            isOneDay = true;
        }
        else if (!GameManager.instance.isDaytime)
        {
            isOneDay = false;
        }

        if (isWinter)
		{
            WinterPanel.SetActive(true);
            particle.Play();
            textProMaterial = textPro.material;
            //Invoke("ChangeSprite", 5f);
            isWinter = false;
		}


        if (winterCount <= 0 && winterCount > -10 && !isChangedSprite)
        {
            //Time.timeScale = 0;
            Debug.Log("겨울이 온다...");
            isWinter = true;
            if (emissionModule.rateOverTime.constant < 2200f)
            {
                emissionModule.rateOverTime = emissionModule.rateOverTime.constant + 5f;
            }
            if (textPro.color.a < 1f)
			{
                textPro.color += new Color(0, 0, 0, Time.unscaledDeltaTime * 0.05f);
			}
            if (textProMaterial != null)
			{
                if (intensity < 4.2f)
                {
                    intensity += Time.unscaledDeltaTime * 0.5f;
                }
                textProColor += new Color(0 / 255f, 0 / 255f, 0 / 255f, Time.unscaledDeltaTime * 0.1f / 255f);
                textProMaterial.SetColor("_Glow", textProColor * intensity);
			}

            if (intensity >= 4.2f) { winterCount = -10; ChangeSprite(); }
        }

        if (isChangedSprite)
		{
            Debug.Log("겨울이 왔다...");
            Time.timeScale = 1;
            if (emissionModule.rateOverTime.constant > 0f)
			{
                emissionModule.rateOverTime = emissionModule.rateOverTime.constant - 10f;
            }
            if (textProMaterial != null)
            {
                if (intensity > 0f)
                {
                    intensity -= Time.unscaledDeltaTime * 0.1f;
                }
                textProColor -= new Color(0 / 255f, 0 / 255f, 0 / 255f, Time.unscaledDeltaTime * 1f / 255f);
                textProMaterial.SetColor("_Glow", textProColor * intensity);
            }
            if (textPro.color.a > 0f)
			{
                textPro.color -= new Color(0, 0, 0, Time.unscaledDeltaTime * 0.5f);
			}
            else
			{
                WinterPanel.SetActive(false);
            }
            if (emissionModule.rateOverTime.constant <= 0f)
			{
                particle.Stop();
                isChangedSprite = false;
			}
        }
        
    }

    private void ChangeSprite()
	{
        perlin.SnowRefreshChunk(this);
	}
}
