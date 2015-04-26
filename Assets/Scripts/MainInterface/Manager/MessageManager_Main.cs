using UnityEngine;
using System.Collections;

// 管理主页面事件
public class MessageManager_Main : Singleton<MessageManager_Main>
{

    // 主界面功能按钮
    public delegate void MessageDelegate_MainInterfaceMode();
    // 主界面的按钮
    public event MessageDelegate_MainInterfaceMode mainInterface_Mode;
    // 主界面的战斗按钮
    public event MessageDelegate_MainInterfaceMode mainInterface_Mode_Battle;
    // 主界面的装备按钮
    public event MessageDelegate_MainInterfaceMode mainInterface_Mode_Equipment;
    // 返回按钮
    public event MessageDelegate_MainInterfaceMode mainInterface_TurnBack;

    private GameObject gameRoot;

    public override void Init() { }


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    // 发送点击“战斗”按钮事件
    public void sendMessage_BattleButton()
    {
        if (mainInterface_Mode != null)
            mainInterface_Mode();
        if (mainInterface_Mode_Battle != null)
            mainInterface_Mode_Battle();
    }
    
    // 发送点击“装备”按钮事件
    public void sendMessage_EquipmentButton()
    {
        if (mainInterface_Mode != null)
            mainInterface_Mode();
        if (mainInterface_Mode_Equipment != null)
            mainInterface_Mode_Equipment();
    }

    // 发送点击“扭蛋”按钮事件
    public void sendMessage_PrayButton()
    {
        //if (mainInterface_Mode_Battle != null)
        //    mainInterface_Mode_Battle();
    }

    // 发送点击“基友”按钮事件
    public void sendMessage_FriendButton()
    {
        //if (mainInterface_Mode_Battle != null)
        //    mainInterface_Mode_Battle();
    }

    // 发送点击“其他”按钮事件
    public void sendMessage_OtherButton()
    {
        //if (mainInterface_Mode_Battle != null)
        //    mainInterface_Mode_Battle();
    }

    // 任意界面点击“返回”按钮
    public void sendMessage_TurnBack()
    {
        if (mainInterface_TurnBack != null)
            mainInterface_TurnBack();
    }
}
