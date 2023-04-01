using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasSetting : MonoBehaviour
{
    public GameObject image;
    private Image imageInstance;
    public RectTransform canvas;

    public BuildingLayout buildingLayout;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(imageInstance != null)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(canvas, Input.mousePosition))
            {
                image.transform.SetParent(canvas);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
