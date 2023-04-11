using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cornfield : MonoBehaviour
{
    // . . . NPC ��Ȯ �� ��Ȯ�ϸ� clone���� �ؾߵ�

    public GameObject wheatPrefab;
    private GameObject clone;
    public int wheat = 0;
    private bool cultureCheck = false;

    private float increaseInterval = 5f;
    private float timer = 0f;

    private float decreaseProbability = 0.2f;
    private float decreaseRatio = 0.1f;
    private float increaseProbability = 0.2f;
    private float increaseRatio = 0.2f;


    BuildingSetting buildingSetting;

    private void Start()
    {
        buildingSetting = GetComponent<BuildingSetting>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (cultureCheck == false)
        {
            WheatProduction();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       /*if(collision.tag == "NPC")
        {
            // . . . NPC�� ��Ȯ�ϸ� wheatPrefab ����
        }*/
    }

    public void WheatProduction()
    {
        if(timer >= increaseInterval)
        {
            timer = 0f;
            wheat = 10;
            RandomEvent();
            cultureCheck = true;
            Debug.Log("�л���");
            AddPrefab();
        }
    }

    public void RandomEvent()
    {
        float randomValue = Random.value;

        if(randomValue < decreaseProbability)
        {
            wheat = (int)(wheat * (1 - decreaseRatio));
            Debug.Log("20% �϶�");
        }
        else if(randomValue < decreaseProbability + increaseProbability)
        {
            wheat = (int)(wheat * (1 + increaseRatio));
            Debug.Log("20% ����");
        }
        else
        {
            return;
        }
    }

    public void AddPrefab()
    {
        Vector3 fieldPosition = new Vector3(this.transform.position.x, 0, this.transform.position.z + 0.4f);
        clone = wheatPrefab;
        clone.transform.position = fieldPosition;
        clone = Instantiate(wheatPrefab);
        clone.transform.SetParent(this.transform);
    }
}
