using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonAlpha : MonoBehaviour
{
    public bool isAlpha { get; set; }
    private Image childImg;
    private Image img;
    private void Awake()
    {
        childImg = this.transform.GetChild(0).GetComponent<Image>();
        img = this.transform.GetComponent<Image>();
        isAlpha = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isAlpha)
        {
            childImg.color = img.color;
        }
    }
}
