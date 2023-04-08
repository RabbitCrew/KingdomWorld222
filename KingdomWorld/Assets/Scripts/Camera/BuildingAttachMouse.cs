using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BuildingButton Ŭ�������� �����Ǵ� �������� �� ��ũ��Ʈ������ �Ű��.
// �������� �����ϰų�, BuildingColider���� ȣ��޾� ����� ��ġ�ϴ� �Լ��� �ߵ��ϱ⵵ �Ѵ�.
public class BuildingAttachMouse : MonoBehaviour
{
    // AddTilePoint2�Լ��� ȣ���ϱ� ���� SettingObject�� ����
    [SerializeField] private SettingObject settingObj;
    // ������ �����տ� �θ������Ʈ�� �����ϱ� ���� ���ӿ�����Ʈ
    [SerializeField] private GameObject motherBuildingObject;
    [SerializeField] private GameObject waitingClone;

    public static GameObject clone { get; private set; } // ������ ������Ʈ �ν��Ͻ�
    public static bool isClick { get; private set; }    // ��ư Ŭ�� ����

    public void Awake()
    {
        //�̺�Ʈ �帮��
        CallBuildingAttachMouseToWaitingBuildingEventDriven.getObjectEvent += CreateBuilding;
        CallBuildingButtonToBuildingColiderEventDriven.isClickFalseEvent += DetachWatingClone;
        RemoveEventDriven.isRemoveEvent += RemoveEvent;

        isClick = false;
        clone = null;
    }

    private void RemoveEvent()
    {
        CallBuildingAttachMouseToWaitingBuildingEventDriven.getObjectEvent -= CreateBuilding;
        CallBuildingButtonToBuildingColiderEventDriven.isClickFalseEvent -= DetachWatingClone;
        RemoveEventDriven.isRemoveEvent -= RemoveEvent;
    }
    // ��� ������ư�� Ŭ���� BuildingButton���� ȣ���ϴ� �Լ��̴�.
    // �������� �����ϰ�, �ش� �������� BuildingColider�� isFollowMouse�� true�� �ٲ��ش�.
    // isFollowMouse�� �������� ���콺 ��ǥ�� ����ٴϴ��� �����̴�.
    public void CloneInst(GameObject obj)
	{
        // �������� null�̸� �������� �������ش�.
        if (clone == null)
        {
            clone = Instantiate(obj);
        }
        // �̹� ������ �������� �����Ѵٸ� �� �������� �ı��ϰ� ���Ӱ� �������� �����Ѵ�.
        else
        {
            Destroy(clone.gameObject);
            clone = Instantiate(obj);
        }
        // isFollowMouse�� true�� �ٲ��ش�. �̴� �������� ���콺 ��ǥ�� ����ٴϴ��� �����̴�.
        if (clone.GetComponent<BuildingColider>() != null)
        {
            clone.GetComponent<BuildingColider>().isFollowMouse = true;
        }
        // ��ư�� Ŭ���ߴ��� �����̸�, �̰��� true�� �Ǿ������� ���콺 ��ǥ�� �������� ����ٴѴ�.
        isClick = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClick)
        {
            //ScreenToWorldPoint�� ī�޶� ���߰� �ִ� ȭ�� ���� ��ǥ���� ����� �� �ְ� ���ش�.
            //���콺�� ��ǥ�� ���ڰ����� �־� �� ������ǥ�� ȭ����ǥ�� �������ش�.
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float plusX = 0;
            float plusZ = 0;
            // Ÿ�� ������ ������ �������� �̵��ϱ� ���� �߰��� x,z�࿡ ������ ��ġ�̴�.
            if ((clone.transform.GetComponent<SpriteRenderer>().sprite.rect.width / 16) % 2 == 1) { plusX = 0f;}
            else { plusX = 0.5f;}

            if ((clone.transform.GetComponent<SpriteRenderer>().sprite.rect.height / 16) % 2 == 1) { plusZ = 0f;}
            else { plusZ = 0.5f;}

            //Ÿ�Ͽ� ���� ��ĭ�� ���콺 �̵�
            clone.transform.position = new Vector3(Mathf.RoundToInt(mousePosition.x) + plusX, 0, Mathf.RoundToInt(mousePosition.z) + plusZ);
        }
        // ��Ŭ���� �ϸ� ���콺�� �پ��ִ� �������� ������
        if (Input.GetMouseButtonDown(1))
        {
            if (clone != null)
            {
                Destroy(clone.gameObject);
            }
            isClick = false;
        }
    }
    // ������ �������� ���콺���� ���������� ���ش�.
    private void DetachWatingClone()
    {
        if (clone.GetComponent<BuildingColider>() != null)
        {
            float x, z;
            // �ǹ�(������)�� �ϳ� �����´�.
            GameObject waitC = Instantiate(waitingClone);
            waitC.transform.parent = motherBuildingObject.transform;
            waitC.GetComponent<SpriteRenderer>().sprite = clone.GetComponent<SpriteRenderer>().sprite;
            waitC.GetComponent<WaitingBuilding>().SetBuilding(clone);
            waitC.GetComponent<BuildingColider>().isSettingComplete = true;
            waitC.GetComponent<BoxCollider>().size
                = new Vector3(waitC.GetComponent<SpriteRenderer>().sprite.rect.width/ 16 - 0.2f, waitC.GetComponent<SpriteRenderer>().sprite.rect.height/ 16 - 0.2f, 0.2f);
            waitC.transform.localPosition = clone.transform.localPosition;

            // �������� �θ� ������Ʈ�� �̸� �����ص� ������Ʈ�� �����Ѵ�.
            clone.transform.parent = motherBuildingObject.transform;
            // Ÿ�� ������ �����̱� ���� ������ plusX, plusZ�� ���������Ƿ� ���� �����ǿ��� �� ���̰���ŭ�� �ٽ� ����ϰ� ���ش�.
            // �� ���� �Ʒ� AddTilePoint2�� ���ڰ����� ���� ���� ���ȴ�. 
            if (clone.transform.localPosition.x % 1 != 0) { x = -0.5f; }
            else { x = 0; }

            if (clone.transform.localPosition.z % 1 != 0) { z = -0.5f; }
            else { z = 0; }

            // AddTilePoint2�Լ��� ���� ûũ ��ǥ�� �ִ� Ÿ�Ϻ��� �ǹ�(������)�� ������ ��´�.
            settingObj.AddTilePoint2((int)(clone.transform.localPosition.x + x), (int)(clone.transform.localPosition.z + z), clone.GetComponent<BuildingColider>().GetObjTypeNum(), waitC);
            //settingObj.AddTilePoint2((int)(clone.transform.localPosition.x + x), (int)(clone.transform.localPosition.z + z), clone.GetComponent<BuildingColider>().GetObjTypeNum(), clone);
        }
        Destroy(clone.gameObject);

        // �������� null�� �ʱ�ȭ�Ѵ�.
        //clone = null;
        // ���콺 Ŭ�� ���θ� false�� �صξ����� ���� ���콺���� ��������.
        isClick = false;
    }

    private void CreateBuilding(GameObject building)
    {
        float x, z;

        // Ÿ�� ������ �����̱� ���� ������ plusX, plusZ�� ���������Ƿ� ���� �����ǿ��� �� ���̰���ŭ�� �ٽ� ����ϰ� ���ش�.
        // �� ���� �Ʒ� AddTilePoint2�� ���ڰ����� ���� ���� ���ȴ�. 
        if (building.transform.localPosition.x % 1 != 0) { x = -0.5f; }
        else { x = 0; }

        if (building.transform.localPosition.z % 1 != 0) { z = -0.5f; }
        else { z = 0; }
        // AddTilePoint2�Լ��� ���� ûũ ��ǥ�� �ִ� Ÿ�Ϻ��� �ش� �������� ������ ��´�.
        settingObj.AddTilePoint2((int)(building.transform.localPosition.x + x), (int)(building.transform.localPosition.z + z), building.GetComponent<BuildingColider>().GetObjTypeNum(), building);

    }
}
