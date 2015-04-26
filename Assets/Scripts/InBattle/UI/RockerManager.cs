using UnityEngine;
using System.Collections;

// 摇杆管理器
public class RockerManager : MonoBehaviour {
    private float centerX, centerY;
    public GameObject imageObject;
    public UISprite imageSprite;

    private float angle;

	// Use this for initialization
	void Start () {
        centerX = transform.localPosition.x + gameObject.GetComponent<UISprite>().width / 2;
        centerY = transform.localPosition.y + gameObject.GetComponent<UISprite>().height / 2;

        UIEventListener.Get(gameObject).onPress = OnPressRocker;
        UIEventListener.Get(gameObject).onDrag = OnDragRocker;

	}
	
	// Update is called once per frame
	void Update () {

	}

    public void OnPressRocker(GameObject button, bool isPressed)
    {
        //Debug.Log("Press");

        if (isPressed)
            MoveRocker();
        else
            ReleaseRocker();

    }

    public void OnDragRocker(GameObject button, Vector2 delta)
    {
        //Debug.Log(delta);
        MoveRocker();
    }

    // 移动摇杆
    private void MoveRocker()
    {
        setImageSelected(true);
        RotateImage(Input.mousePosition.x - centerX, Input.mousePosition.y - centerY);
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().SendMessage_RockedEvent(angle, true);
    }

    // 松开摇杆
    private void ReleaseRocker()
    {
        //Debug.Log("ReleaseRocker");
        setImageSelected(false);
        GameRoot_InBattle.getSingleton<MessageManager_InBattle>().SendMessage_RockedEvent(angle, false);
    }

    // 设置图片
    private void setImageSelected(bool selected)
    {
        //print("change Image");
        if (selected)
            imageSprite.spriteName = "Rocker_2";
        else
            imageSprite.spriteName = "Rocker_1";
    }

    // 变化图片方向
    private void RotateImage(float relateX, float relateY)
    {
        angle = Mathf.Atan2(relateX, relateY) / Mathf.PI * -180;
        //print(angle);

        imageObject.transform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
