using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NPCHpBar : MonoBehaviour
{
    int Hp;
    int MaxHP;

    [SerializeField] Image HPBar;
    [SerializeField] GameObject HpPanel;

    private void Awake()
    {
        HpPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        HpImageSet();
    }

    float distance = 50f;

    void HpImageSet()
    {
        if (!IsPointerOverUIObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray, distance);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.name == "Citizen1(Clone)")
                {
                    HpPanel.gameObject.SetActive(true);

                    Hp = hits[i].collider.gameObject.gameObject.GetComponent<NPC>().HP;

                    MaxHP = hits[i].collider.gameObject.gameObject.GetComponent<NPC>().Maxhp;

                    HPBar.fillAmount = Hp / MaxHP;
                }
                else
                {
                    HpPanel.gameObject.SetActive(false);
                }
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //RaycastResult : BaseRaycastModule������ ��Ʈ ���.
        List<RaycastResult> results = new List<RaycastResult>();
        //EventSystem.current�� �ֱٿ� �߻��� �̺�Ʈ �ý����� ��ȯ�Ѵ�.
        //ù��° ���ڰ� : ���� ������ ������.
        //�ι�° ���ڰ� : List of 'hits' to populate.
        //RaycastAll : ��� ������ BaseRaycaster�� ����� ���� �ش� �������� ���� ĳ����.
        // -> �����ִ� ������Ʈ���� �ִٸ� �����ִ� ���� results�� ī��Ʈ�� �ٲ�
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        //Debug.Log(results.Count);
        return results.Count > 0;
    }
}
