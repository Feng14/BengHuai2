using UnityEngine;
using System.Collections;

// Unuse
// 装备界面-》仓库按钮 事件管理器
public class Equipment_Button_Sotrehouse : Equipment_Button {

    void OnStart()
    {
        //GameRoot_Main.getSingleton<MessageManager_Equipment>().storehouseLoadOver
        //    += new MessageManager_Equipment.EquipmentDelegate(EventOver);
    }

    public void OnStorehouseButtonClick()
    {
        GameRoot_Main.getSingleton<MessageManager_Equipment>().sendMessage_Storehouse();
    }

    public override void OnClick()
    {
        //changeButtonImage(true);
        //Debug.Log(GameRoot_Main.getSingleton<MessageManager_Equipment>() == null);
    }

    //事件处理结束，弹起按钮
    //private void EventOver()
    //{
    //    changeButtonImage(false);
    //}
}
