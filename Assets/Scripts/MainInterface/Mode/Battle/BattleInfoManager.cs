using UnityEngine;
using System.Collections;

// 附着在主界面-》战斗-》战斗信息（准备）上的管理器
public class BattleInfoManager : MonoBehaviour {
    public BattleStage battleStage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 点击开始按钮
    public void ClickStart()
    {
        GameRoot_Main.getSingleton<MessageManager_BattleMap>().sendMessage_StartBattleStage(battleStage);
    }
}
