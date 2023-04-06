using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BuildingButton 클래스에서 생성되던 프리팹을 이 스크립트쪽으로 옮겼다.
// 프리팹을 생성하거나, BuildingColider에서 호출받아 빌딩을 배치하는 함수를 발동하기도 한다.
public class BuildingAttachMouse : MonoBehaviour
{
    // AddTilePoint2함수를 호출하기 위한 SettingObject형 변수
    [SerializeField] private SettingObject settingObj;
    // 생성한 프리팹에 부모오브젝트로 설정하기 위한 게임오브젝트
    [SerializeField] private GameObject motherBuildingObject;
    public static GameObject clone { get; private set; } // 생성된 오브젝트 인스턴스
    public static GameObject waitingClone { get; private set; }
    public static bool isClick { get; private set; }    // 버튼 클릭 여부

    public void Awake()
    {
        //이벤트 드리븐
        CallBuildingButtonToBuildingColiderEventDriven.isClickFalseEvent += DetachClone;
        RemoveEventDriven.isRemoveEvent += RemoveEvent;

        isClick = false;
        clone = null;
    }

    private void RemoveEvent()
    {
        CallBuildingButtonToBuildingColiderEventDriven.isClickFalseEvent -= DetachClone;
        RemoveEventDriven.isRemoveEvent -= RemoveEvent;
    }
    // 빌딩 생성버튼을 클릭시 BuildingButton에서 호출하는 함수이다.
    // 프리팹을 생성하고, 해당 프리팹의 BuildingColider의 isFollowMouse를 true로 바꿔준다.
    // isFollowMouse는 프리팹이 마우스 좌표를 따라다니는지 여부이다.
    public void CloneInst(GameObject obj)
	{
        // 프리팹이 null이면 프리팹을 생성해준다.
        if (clone == null)
        {
            clone = Instantiate(obj);
        }
        // 이미 생성된 프리팹이 존재한다면 그 프리팹을 파괴하고 새롭게 프리팹을 생성한다.
        else
        {
            Destroy(clone.gameObject);
            clone = Instantiate(obj);
        }
        // isFollowMouse를 true로 바꿔준다. 이는 프리팹이 마우스 좌표를 따라다니는지 여부이다.
        if (clone.GetComponent<BuildingColider>() != null)
        {
            clone.GetComponent<BuildingColider>().isFollowMouse = true;
        }
        // 버튼을 클릭했는지 여부이며, 이것이 true로 되어있으면 마우스 좌표를 프리팹이 따라다닌다.
        isClick = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClick)
        {
            //ScreenToWorldPoint는 카메라가 비추고 있는 화면 내의 좌표값을 사용할 수 있게 해준다.
            //마우스의 좌표를 인자값으로 넣어 이 월드좌표를 화면좌표로 변경해준다.
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float plusX = 0;
            float plusZ = 0;
            // 타일 단위로 생성된 프리팹을 이동하기 위한 추가로 x,z축에 더해줄 수치이다.
            if ((clone.transform.GetComponent<SpriteRenderer>().sprite.rect.width / 16) % 2 == 1) { plusX = 0f;}
            else { plusX = 0.5f;}

            if ((clone.transform.GetComponent<SpriteRenderer>().sprite.rect.height / 16) % 2 == 1) { plusZ = 0f;}
            else { plusZ = 0.5f;}

            //타일에 맞춰 한칸씩 마우스 이동
            clone.transform.position = new Vector3(Mathf.RoundToInt(mousePosition.x) + plusX, 0, Mathf.RoundToInt(mousePosition.z) + plusZ);
        }
        // 우클릭을 하면 마우스에 붙어있던 프리팹을 지워줌
        if (Input.GetMouseButtonDown(1))
        {
            if (clone != null)
            {
                Destroy(clone.gameObject);
            }
            isClick = false;
        }
    }
    // 생성한 프리팹을 마우스에서 떨어지도록 해준다.
    private void DetachClone()
    {
        if (clone.GetComponent<BuildingColider>() != null)
        {
            // 프리팹의 부모 오브젝트를 미리 지정해둔 오브젝트로 지정한다.
            clone.transform.parent = motherBuildingObject.transform;
            float x, z;
            // 타일 단위로 움직이기 위해 위에서 plusX, plusZ를 더해줬으므로 원래 포지션에서 그 차이값만큼을 다시 계산하고 빼준다.
            // 뺀 값은 아래 AddTilePoint2에 인자값으로 쓰기 위해 사용된다. 
            if (clone.transform.localPosition.x % 1 != 0) { x = -0.5f; }
            else { x = 0; }

            if (clone.transform.localPosition.z % 1 != 0) { z = -0.5f; }
            else { z = 0; }
            // AddTilePoint2함수를 통해 청크 좌표에 있는 타일별로 해당 프리팹의 정보를 담는다.
            settingObj.AddTilePoint2((int)(clone.transform.localPosition.x + x), (int)(clone.transform.localPosition.z + z), clone.GetComponent<BuildingColider>().GetObjTypeNum(), clone);
        }
        // 프리팹을 null로 초기화한다.
        clone = null;
        // 마우스 클릭 여부를 false로 해두었으니 이제 마우스에서 떨어진다.
        isClick = false;
    }
}
