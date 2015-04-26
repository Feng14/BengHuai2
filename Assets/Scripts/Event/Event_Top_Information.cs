using UnityEngine;
using System.Collections;

using System.Threading;
using System.Reflection;

//  主页面顶上功能栏事件处理器
public class Event_Top_Information : MonoBehaviour
{


    public GameObject mode_Fight, mode_Equipment, mode_Pray, mode_Friend, mode_Other;

	// Use this for initialization
	void Start () {
        // 注册“功能按键被点选”事件
        GameRoot_Main.getSingleton<MessageManager_Main>().mainInterface_Mode
            += new MessageManager_Main.MessageDelegate_MainInterfaceMode(unSelectedAllItem);

        // 注册“战斗按键被点选”事件
        GameRoot_Main.getSingleton<MessageManager_Main>().mainInterface_Mode_Battle
            += new MessageManager_Main.MessageDelegate_MainInterfaceMode(deal_BattleItemEvent);

        // 注册“装备按键被点选”事件
        GameRoot_Main.getSingleton<MessageManager_Main>().mainInterface_Mode_Equipment
            += new MessageManager_Main.MessageDelegate_MainInterfaceMode(deal_EquipmentItemEvent);
	}
	
	// Update is called once per frame
	void Update () {
	
	}




    // 接收“战斗”按键事件消息
    public void deal_BattleItemEvent()
    {
        //Debug.Log("Set true");
        mode_Fight.GetComponent<Main_Battle>().setSelected(true);
    }

    // 接收“装备”按键事件消息
    public void deal_EquipmentItemEvent()
    {
        mode_Equipment.GetComponent<Main_Equipment>().setSelected(true);
    }

    // 接收“扭蛋”按键事件消息
    public void deal_PrayItemEvent()
    {
        unSelectedAllItem();
    }

    // 接收“基友”按键事件消息
    public void deal_FriendItemEvent()
    {
        unSelectedAllItem();
    }

    // 接收“其他”按键事件消息
    public void deal_OtherItemEvent()
    {
        unSelectedAllItem();
    }

    // 取消所有案件的选中状态
    private void unSelectedAllItem()
    {
        //Debug.Log("Fucking ");
        mode_Fight.GetComponent<Main_Battle>().setSelected(false);
        mode_Equipment.GetComponent<Main_Equipment>().setSelected(false);
    }
}
