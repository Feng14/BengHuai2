using UnityEngine;
using System.Collections;

// 战斗地图关卡对象（在主界面-》战斗模块)
public class BattleStageObj : MonoBehaviour
{
    // 关卡信息
    public BattleStage battleStage;

    // 关卡图标
    public UISprite image;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // (主界面-》战斗模式-》关卡）被点击---》显示战斗信息
    public void BattleReady()
    {
        GameRoot_Main.getSingleton<MessageManager_BattleMap>().sendMessage_ClickBattleStage(battleStage);
    }
}
