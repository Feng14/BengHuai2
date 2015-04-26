using UnityEngine;
using System.Collections;


// 附着在主界面-》上侧功能栏-》装备  上的脚本
public class Main_Equipment : Main_Mode {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    // 点击“装备”按钮时调用
    public void euqipmentButtonDown()
    {
        //Debug.Log(GameRoot_Main.getSingleton<MessageManager_Main>() == null);
        GameRoot_Main.getSingleton<MessageManager_Main>().sendMessage_EquipmentButton();
        //MessageManager_Main.getInstance().sendMessage_EquipmentButton();
    }

    //// 修改成(不)选中状态
    //public void setSelected(bool ifSelect)
    //{
    //    //Debug.Log("Set " + ifSelect);
    //    transform.GetChild(0).GetComponent<UISprite>().spriteName = ifSelect ? "Function_Equipment2" : "Function_Equipment1";
    //}

    // 获取选中状态图片
    public override string getSelectedSpriteName()
    {
        return "Function_Equipment2";
    }

    // 获取未选中状态图片
    public override string getUnSelectedSpriteName()
    {
        return "Function_Equipment1";
    }
}
