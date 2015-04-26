using UnityEngine;
using System.Collections;

// 事件转发器（代为发送消息
public class MessageManager_BattleMap : Singleton<MessageManager_BattleMap>
{
    // 战斗模式地图接受消息？
    public bool battleMapReceiveMessage = true;

    // 主机面点击“战斗”按钮
    public delegate void BattleMapDelegate();
    public event BattleMapDelegate showBattleMap;

    // 关卡地图翻页事件
    public delegate void ModeBattleMessageDelegate();
    public event ModeBattleMessageDelegate pageLeftEvent;
    public event ModeBattleMessageDelegate pageRightEvent;

    // 地图移动结束事件
    public delegate void MapMoveMessageDelegate(GameObject map);
    public event MapMoveMessageDelegate mapMoveInOver;
    public event MapMoveMessageDelegate mapMoveOutOver;

    // 战斗关卡事件
    public delegate void BattleStageMessageDelegate(BattleStage stage);
    public event BattleStageMessageDelegate clickStage;
    public event BattleStageMessageDelegate changeToBreakMode;
    public event BattleStageMessageDelegate changeToLiveMode;
    public event BattleStageMessageDelegate startBattle;

	// Use this for initialization
    public override void Init() { }
	
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 通知显示一张关卡地图
    public void sendMessage_ShowMap()
    {
        battleMapReceiveMessage = true;
        if (showBattleMap != null)
            showBattleMap();
    }

    // 发送左翻页事件
    public void sendMessage_PageLeft()
    {
        if (!battleMapReceiveMessage)
            return;

        if (pageLeftEvent != null)
            pageLeftEvent();
    }

    // 发送右翻页事件
    public void sendMessage_PageRight()
    {
        if (!battleMapReceiveMessage)
            return;

        if (pageRightEvent != null)
            pageRightEvent();
    }

    // 发送地图移动结束事件
    public void sendMessage_MapMoveInOver(GameObject map)
    {
        if (mapMoveInOver != null)
            mapMoveInOver(map);
    }
    public void sendMessage_MapMoveOutOver(GameObject map)
    {
        if (mapMoveOutOver != null)
            mapMoveOutOver(map);
    }

    // 发送点击战斗关卡事件
    public void sendMessage_ClickBattleStage(BattleStage battleStage)
    {
        //Debug.Log("sendMessage_ClickBattleStage ");

        if (!battleMapReceiveMessage)
            return;

        // 停止“战斗关卡地图”发送消息
        battleMapReceiveMessage = false;

        //Debug.Log("BattleStage " + (battleStage.GetComponent<BattleStage>() == null ? "==" : "!=") + " null");
        if (clickStage != null)
            clickStage(battleStage);
    }

    // 修改为闯关模式
    public void sendMessage_ChangeToBreakMode(BattleStage battleStage)
    {
        if (changeToBreakMode != null)
            changeToBreakMode(battleStage);
    }

    // 修改为生存模式
    public void sendMessage_ChangeToLiveMode(BattleStage battleStage)
    {
        if (changeToLiveMode != null)
            changeToLiveMode(battleStage);
    }

    // 发送开始战斗关卡事件
    public void sendMessage_StartBattleStage(BattleStage battleStage)
    {
        //Debug.Log("Send Message : Start Game");
        if (startBattle != null)
            startBattle(battleStage);
    }
}
