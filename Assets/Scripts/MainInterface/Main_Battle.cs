using UnityEngine;
using System.Collections;

// 附着在主界面-》上侧功能栏-》战斗  上的脚本
public class Main_Battle : Main_Mode
{
    public string selected, unselected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 点击“战斗”按钮事件
    public void battleEvent()
    {
        /**
        Message_SelectFunction_Fight m = (Message_SelectFunction_Fight)
            MessageManager.getMessageInstance<Message_SelectFunction_Fight>(Message_SelectFunction_Fight.sample.GetType().ToString());

        MessageManager.boradcastMessage(Message_SelectFunction_Fight.sample.GetType().ToString(), m);
        **/
    }

    // 点击“战斗”按钮时调用
    public void battleButtonDown()
    {
        GameRoot_Main.getSingleton<MessageManager_Main>().sendMessage_BattleButton();
        //MessageManager_Main.getInstance().sendMessage_BattleButton();
    }

    //// 修改成(不)选中状态
    //public void setSelected(bool ifSelect)
    //{
    //    //Debug.Log("Set " + ifSelect);
    //    transform.GetChild(0).GetComponent<UISprite>().spriteName = ifSelect ? "Function_Battle2" : "Function_Battle1";
    //}

    // 获取选中状态图片
    public override string getSelectedSpriteName()
    {
        return "Function_Battle2";
    }

    // 获取未选中状态图片
    public override string getUnSelectedSpriteName()
    {
        return "Function_Battle1";
    }
}
