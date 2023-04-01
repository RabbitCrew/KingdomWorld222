using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingLayout : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject citizenBDPrefab;
    private GameObject imageInstance;

    private Image currentImage;

    private Color opaqueColor = new Color(1f, 1f, 1f, 1f);
    private Color transparentColor = new Color(1f, 1f, 1f, 0f);
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (imageInstance != null)
        {
            Vector2 mousePosition = Input.mousePosition;
            imageInstance.transform.position = mousePosition;
        }
    }

    public void OnButtonClick()
    {
        if (citizenBDPrefab != null)
        {
            imageInstance = Instantiate(citizenBDPrefab, transform.parent);

            Color color = citizenBDPrefab.GetComponent<Image>().color;
            color.a = 0.5f;
            citizenBDPrefab.GetComponent<Image>().color = color;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnButtonClick();
        if (currentImage != null)
        {
            currentImage.transform.SetParent(transform);
            currentImage = null;
        }
        Debug.Log("point down");
    }
    public void OnPointerUp(PointerEventData eventData)         //이미지 위에 게이지바 / 시간 5초 / 불투명상태 / 5초후 건설완
    {
        imageInstance = null;
        Debug.Log("point up");

        Invoke("ActivateBuilding", 5f);
    }

    private void ActivateBuilding()
    {
        imageInstance.SetActive(true);
    }

    private Vector2 GetMousePosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 localMousePosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, mousePosition, null, out localMousePosition))
        {
            mousePosition = localMousePosition;
        }

        return mousePosition;
    }
}
